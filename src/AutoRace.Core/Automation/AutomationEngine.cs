using System.Threading.Channels;
using AutoRace.Core.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace AutoRace.Core.Automation;

public sealed class AutomationEngine : IAutomationEngine, IDisposable
{
    private readonly ILogger<AutomationEngine> _logger;
    private readonly Func<CancellationToken, Task> _step;
    private readonly Channel<AutomationEvent> _events;
    private readonly object _gate = new();

    private CancellationTokenSource? _cts;
    private Task? _runTask;
    private int _progress;

    public ChannelReader<AutomationEvent> Events => _events.Reader;

    public AutomationEngine(
        Func<CancellationToken, Task>? step = null,
        ILogger<AutomationEngine>? logger = null)
    {
        _logger = logger ?? NullLogger<AutomationEngine>.Instance;
        _step = step ?? (ct => Task.Delay(TimeSpan.FromMilliseconds(50), ct));
        _events = Channel.CreateUnbounded<AutomationEvent>(new UnboundedChannelOptions
        {
            SingleReader = false,
            SingleWriter = true,
            AllowSynchronousContinuations = true,
        });
    }

    public Task StartAsync(Profile profile, CancellationToken cancellationToken = default)
    {
        if (profile is null) throw new ArgumentNullException(nameof(profile));

        lock (_gate)
        {
            if (_runTask is not null && !_runTask.IsCompleted)
            {
                throw new InvalidOperationException("Automation engine is already running.");
            }

            _progress = 0;
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            _runTask = RunAsync(profile, _cts.Token);
        }

        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken = default)
    {
        Task? runTask;
        CancellationTokenSource? cts;

        lock (_gate)
        {
            runTask = _runTask;
            cts = _cts;
        }

        if (runTask is null)
        {
            return;
        }

        try
        {
            cts?.Cancel();
        }
        catch (ObjectDisposedException)
        {
            // ignore
        }

        // Await completion but keep it bounded by external cancellation if provided.
        await runTask.WaitAsync(cancellationToken);
    }

    private async Task RunAsync(Profile profile, CancellationToken cancellationToken)
    {
        try
        {
            await PublishAsync(new AutomationEvent(AutomationEventKind.Started, DateTimeOffset.UtcNow, Message: profile.Name), cancellationToken);

            while (!cancellationToken.IsCancellationRequested)
            {
                await _step(cancellationToken);
                _progress++;
                await PublishAsync(new AutomationEvent(AutomationEventKind.Progress, DateTimeOffset.UtcNow, Progress: _progress), cancellationToken);
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            // expected
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Automation loop faulted");
            await PublishAsync(new AutomationEvent(AutomationEventKind.Faulted, DateTimeOffset.UtcNow, Exception: ex), CancellationToken.None);
        }
        finally
        {
            await PublishAsync(new AutomationEvent(AutomationEventKind.Stopped, DateTimeOffset.UtcNow), CancellationToken.None);
            _events.Writer.TryComplete();

            lock (_gate)
            {
                _cts?.Dispose();
                _cts = null;
            }
        }
    }

    private ValueTask PublishAsync(AutomationEvent evt, CancellationToken cancellationToken)
        => _events.Writer.WriteAsync(evt, cancellationToken);

    public void Dispose()
    {
        lock (_gate)
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
    }
}
