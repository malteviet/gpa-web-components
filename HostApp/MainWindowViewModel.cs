using System.Collections.ObjectModel;
using CefSharp;
using CefSharp.JavascriptBinding;
using CefSharp.Wpf;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GongSolutions.Wpf.DragDrop;

namespace HostApp;

public partial class MainWindowViewModel : ObservableObject, IDropTarget
{
    private readonly ChromiumWebBrowser _browser;

    [ObservableProperty]
    private string _address = $"{AppDefaults.LocalSchemeName}://{AppDefaults.LocalDomainName}";
    private readonly DeviceCatalogService _deviceCatalogService = new();

    public MainWindowViewModel(ChromiumWebBrowser browser)
    {
        this._browser = browser;
        _browser.JavascriptObjectRepository.ResolveObject += (sender, args) =>
        {
            var repo = args.ObjectRepository;
            if (args.ObjectName == "catalogService")
            {
                repo.NameConverter = new CamelCaseJavascriptNameConverter();
                repo.Register("catalogService", _deviceCatalogService);
            }
        };
        //_browser.JavascriptObjectRepository.NameConverter = new CefSharp.JavascriptBinding.CamelCaseJavascriptNameConverter();
        //_browser.JavascriptObjectRepository.Register("catalogService", _deviceCatalogService, options: BindingOptions.DefaultBinder);
    }

    public ObservableCollection<Device> Items { get; } = new();

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

    [RelayCommand]
    private void DevPage()
    {
        Address = "http://localhost:5173";
    }

    public void DragOver(IDropInfo dropInfo)
    {
        //var xml = data.GetXml();
        // TODO
    }{"name":"call display module","id":"GIDS01DCIP-1.0.934.0-10","urn":"de.gira.schema.components.DcsIp.CallDisplayModule","imageKey":"browserImage"}

    public void Drop(IDropInfo dropInfo)
    {
        // TODO
    }
}
