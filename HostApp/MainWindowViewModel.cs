using CefSharp;
using CefSharp.Wpf;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace HostApp;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly ChromiumWebBrowser _browser;
    [ObservableProperty] private string _address = $"{AppDefaults.LocalSchemeName}://{AppDefaults.LocalDomainName}";
    public MainWindowViewModel(ChromiumWebBrowser browser)
    {
        this._browser = browser;
    }

    [RelayCommand]
    private void OpenDevTools()
    {
        this._browser.ShowDevTools();
    }

    [RelayCommand]
    private void Navigate(string address)
    {
        _browser.Address = address;
    }
}