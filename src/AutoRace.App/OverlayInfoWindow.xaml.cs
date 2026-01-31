using System.Windows;
using System.Windows.Input;
using AutoRace.App.Services;

namespace AutoRace.App;

public partial class OverlayInfoWindow : Window
{
    private readonly OverlaySettings _settings;

    public OverlayInfoWindow()
    {
        InitializeComponent();

        _settings = OverlaySettings.Load();
        Loaded += OnLoaded;
        LocationChanged += (_, _) => SavePosition();
        Closed += (_, _) => SavePosition();

        MouseLeftButtonDown += (_, e) =>
        {
            if (e.ButtonState != MouseButtonState.Pressed) return;
            try
            {
                DragMove();
            }
            catch
            {
                // ignore
            }
            SavePosition();
        };
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (_settings.Left is double left) Left = left;
        if (_settings.Top is double top) Top = top;
    }

    private void SavePosition()
    {
        _settings.Left = Left;
        _settings.Top = Top;
        _settings.Save();
    }
}
