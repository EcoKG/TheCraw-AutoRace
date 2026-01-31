using AutoRace.App.Mvvm;

namespace AutoRace.App.ViewModels;

public sealed class OverlayInfoViewModel : ObservableObject
{
    private string _statusText = "Idle";
    private string _elapsedText = "00:00:00";
    private int _rounds;
    private decimal _revenue;

    public string StatusText
    {
        get => _statusText;
        set => SetProperty(ref _statusText, value);
    }

    public string ElapsedText
    {
        get => _elapsedText;
        set => SetProperty(ref _elapsedText, value);
    }

    public int Rounds
    {
        get => _rounds;
        set => SetProperty(ref _rounds, value);
    }

    public decimal Revenue
    {
        get => _revenue;
        set
        {
            if (!SetProperty(ref _revenue, value)) return;
            OnPropertyChanged(nameof(RevenueText));
        }
    }

    public string RevenueText => $"{Revenue:N0}";
}
