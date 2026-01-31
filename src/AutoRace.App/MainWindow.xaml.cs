using System.ComponentModel;
using System.Windows;
using AutoRace.App.ViewModels;

namespace AutoRace.App;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly OverlayInfoWindow _overlayWindow;
    private readonly OverlayInfoViewModel _overlayVm;

    public MainWindow()
    {
        InitializeComponent();

        _overlayVm = new OverlayInfoViewModel();
        _overlayWindow = new OverlayInfoWindow
        {
            DataContext = _overlayVm,
            Owner = this
        };

        Loaded += (_, _) => HookViewModel();
        Closing += (_, _) =>
        {
            try { _overlayWindow.Close(); } catch { /* ignore */ }
        };
    }

    private void HookViewModel()
    {
        if (DataContext is not MainViewModel vm) return;

        vm.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName is nameof(MainViewModel.IsRunning))
            {
                if (vm.IsRunning) ShowOverlay(vm);
                else HideOverlay();
            }

            if (e.PropertyName is nameof(MainViewModel.StatusText)
                or nameof(MainViewModel.ElapsedText)
                or nameof(MainViewModel.Rounds)
                or nameof(MainViewModel.Revenue))
            {
                SyncOverlay(vm);
            }
        };

        SyncOverlay(vm);
        if (vm.IsRunning) ShowOverlay(vm);
    }

    private void ShowOverlay(MainViewModel vm)
    {
        SyncOverlay(vm);

        if (_overlayWindow.IsVisible)
        {
            _overlayWindow.Activate();
            return;
        }

        _overlayWindow.Show();
    }

    private void HideOverlay()
    {
        if (!_overlayWindow.IsVisible) return;
        _overlayWindow.Hide();
    }

    private void SyncOverlay(MainViewModel vm)
    {
        _overlayVm.StatusText = vm.StatusText;
        _overlayVm.ElapsedText = vm.ElapsedText;
        _overlayVm.Rounds = vm.Rounds;
        _overlayVm.Revenue = vm.Revenue;
    }
}
