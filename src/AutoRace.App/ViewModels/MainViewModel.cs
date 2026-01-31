using System;
using System.Windows.Input;
using System.Windows.Threading;
using AutoRace.App.Mvvm;

namespace AutoRace.App.ViewModels;

public sealed class MainViewModel : ObservableObject
{
    private readonly DispatcherTimer _timer;

    private string _statusText = "Ready";
    private bool _isRunning;
    private DateTimeOffset? _startedAt;
    private TimeSpan _elapsed;
    private string _elapsedText = "00:00:00";
    private int _rounds;
    private decimal _revenue;

    public string StatusText
    {
        get => _statusText;
        set => SetProperty(ref _statusText, value);
    }

    public bool IsRunning
    {
        get => _isRunning;
        set
        {
            if (!SetProperty(ref _isRunning, value)) return;
            (StartCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (StopCommand as RelayCommand)?.RaiseCanExecuteChanged();

            if (_isRunning) _timer.Start();
            else _timer.Stop();
        }
    }

    public DateTimeOffset? StartedAt
    {
        get => _startedAt;
        private set => SetProperty(ref _startedAt, value);
    }

    public TimeSpan Elapsed
    {
        get => _elapsed;
        private set
        {
            if (!SetProperty(ref _elapsed, value)) return;
            ElapsedText = FormatElapsed(value);
        }
    }

    public string ElapsedText
    {
        get => _elapsedText;
        private set => SetProperty(ref _elapsedText, value);
    }

    public int Rounds
    {
        get => _rounds;
        set => SetProperty(ref _rounds, value);
    }

    public decimal Revenue
    {
        get => _revenue;
        set => SetProperty(ref _revenue, value);
    }

    public ICommand StartCommand { get; }
    public ICommand StopCommand { get; }

    public MainViewModel()
    {
        StartCommand = new RelayCommand(Start, () => !IsRunning);
        StopCommand = new RelayCommand(Stop, () => IsRunning);

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(250)
        };
        _timer.Tick += (_, _) =>
        {
            if (!IsRunning || StartedAt is null) return;
            Elapsed = DateTimeOffset.Now - StartedAt.Value;
        };
    }

    private void Start()
    {
        StartedAt = DateTimeOffset.Now;
        Elapsed = TimeSpan.Zero;
        Rounds = 0;
        Revenue = 0m;

        IsRunning = true;
        StatusText = $"Running… ({DateTime.Now:HH:mm:ss})";
    }

    private void Stop()
    {
        IsRunning = false;
        StatusText = $"Stopped. ({DateTime.Now:HH:mm:ss})";
    }

    private static string FormatElapsed(TimeSpan elapsed)
    {
        // Keep it stable and readable.
        if (elapsed < TimeSpan.Zero) elapsed = TimeSpan.Zero;
        var totalHours = (int)elapsed.TotalHours;
        return $"{totalHours:00}:{elapsed.Minutes:00}:{elapsed.Seconds:00}";
    }
}
