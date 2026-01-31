using System;
using System.Windows.Input;
using AutoRace.App.Mvvm;

namespace AutoRace.App.ViewModels;

public sealed class MainViewModel : ObservableObject
{
    private string _statusText = "Ready";
    private bool _isRunning;

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
        }
    }

    public ICommand StartCommand { get; }
    public ICommand StopCommand { get; }

    public MainViewModel()
    {
        StartCommand = new RelayCommand(Start, () => !IsRunning);
        StopCommand = new RelayCommand(Stop, () => IsRunning);
    }

    private void Start()
    {
        IsRunning = true;
        StatusText = $"Running… ({DateTime.Now:HH:mm:ss})";
    }

    private void Stop()
    {
        IsRunning = false;
        StatusText = $"Stopped. ({DateTime.Now:HH:mm:ss})";
    }
}
