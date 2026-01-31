using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using AutoRace.App.Services;

namespace AutoRace.App;

public partial class OverlayInfoWindow : Window
{
    private readonly OverlaySettings _settings;
    private readonly DispatcherTimer _saveDebounce;

    public OverlayInfoWindow()
    {
        InitializeComponent();

        _settings = OverlaySettings.Load();

        _saveDebounce = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(500)
        };
        _saveDebounce.Tick += (_, _) =>
        {
            _saveDebounce.Stop();
            SavePositionCore();
        };

        Loaded += OnLoaded;
        LocationChanged += (_, _) => ScheduleSavePosition();
        Closed += (_, _) => SavePositionCore();

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
            ScheduleSavePosition();
        };
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (_settings.Left is double left) Left = left;
        if (_settings.Top is double top) Top = top;

        ClampToWorkArea();
    }

    private void ClampToWorkArea()
    {
        // Prevent the overlay from restoring off-screen (e.g., monitor config changes).
        var wa = SystemParameters.WorkArea;

        if (double.IsNaN(Left) || double.IsInfinity(Left)) Left = wa.Left;
        if (double.IsNaN(Top) || double.IsInfinity(Top)) Top = wa.Top;

        var maxLeft = wa.Right - ActualWidth;
        var maxTop = wa.Bottom - ActualHeight;

        if (ActualWidth <= 0) maxLeft = wa.Right - Width;
        if (ActualHeight <= 0) maxTop = wa.Bottom - Height;

        if (Left < wa.Left) Left = wa.Left;
        else if (Left > maxLeft) Left = maxLeft;

        if (Top < wa.Top) Top = wa.Top;
        else if (Top > maxTop) Top = maxTop;

        ScheduleSavePosition();
    }

    private void ScheduleSavePosition()
    {
        _saveDebounce.Stop();
        _saveDebounce.Start();
    }

    private void SavePositionCore()
    {
        _settings.Left = Left;
        _settings.Top = Top;
        _settings.Save();
    }
}
