using System.Diagnostics;
using AutoRace.Core.Automation;
using AutoRace.Core.Domain.Models;

namespace AutoRace.Core.Tests;

public sealed class AutomationEngineTests
{
    private static Profile CreateProfile(string name = "Test")
        => new(name, new ProfileSettings("Game Window"));

    [Fact]
    public async Task Start_Then_Stop_Emits_Started_And_Stopped()
    {
        using var engine = new AutomationEngine(ct => Task.Delay(10, ct));

        await engine.StartAsync(CreateProfile());

        var started = await ReadNextAsync(engine, TimeSpan.FromSeconds(1));
        Assert.Equal(AutomationEventKind.Started, started.Kind);

        await engine.StopAsync();

        // There may be progress events in between; scan until Stopped.
        var stopped = await ReadUntilAsync(engine, AutomationEventKind.Stopped, TimeSpan.FromSeconds(1));
        Assert.Equal(AutomationEventKind.Stopped, stopped.Kind);
    }

    [Fact]
    public async Task Stop_Completes_Within_500ms()
    {
        using var engine = new AutomationEngine(ct => Task.Delay(50, ct));
        await engine.StartAsync(CreateProfile());

        // consume Started so the loop is definitely running
        _ = await ReadNextAsync(engine, TimeSpan.FromSeconds(1));

        var sw = Stopwatch.StartNew();
        await engine.StopAsync();
        sw.Stop();

        Assert.True(sw.Elapsed < TimeSpan.FromMilliseconds(500), $"Stop took {sw.Elapsed.TotalMilliseconds:0}ms");
    }

    [Fact]
    public async Task Faulted_Event_Is_Emitted_On_Exception()
    {
        var thrown = new InvalidOperationException("boom");
        using var engine = new AutomationEngine(_ => throw thrown);

        await engine.StartAsync(CreateProfile());

        // Started should still be emitted before fault.
        var started = await ReadNextAsync(engine, TimeSpan.FromSeconds(1));
        Assert.Equal(AutomationEventKind.Started, started.Kind);

        var faulted = await ReadUntilAsync(engine, AutomationEventKind.Faulted, TimeSpan.FromSeconds(1));
        Assert.Equal(AutomationEventKind.Faulted, faulted.Kind);
        Assert.IsType<InvalidOperationException>(faulted.Exception);

        var stopped = await ReadUntilAsync(engine, AutomationEventKind.Stopped, TimeSpan.FromSeconds(1));
        Assert.Equal(AutomationEventKind.Stopped, stopped.Kind);
    }

    private static async Task<AutomationEvent> ReadNextAsync(IAutomationEngine engine, TimeSpan timeout)
    {
        using var cts = new CancellationTokenSource(timeout);
        return await engine.Events.ReadAsync(cts.Token);
    }

    private static async Task<AutomationEvent> ReadUntilAsync(IAutomationEngine engine, AutomationEventKind kind, TimeSpan timeout)
    {
        var deadline = DateTime.UtcNow + timeout;

        while (DateTime.UtcNow < deadline)
        {
            var remaining = deadline - DateTime.UtcNow;
            if (remaining <= TimeSpan.Zero) break;

            var next = await ReadNextAsync(engine, remaining);
            if (next.Kind == kind)
            {
                return next;
            }
        }

        throw new TimeoutException($"Did not observe event kind '{kind}' within {timeout}.");
    }
}
