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
        //Wait for the page to finish loading (all resources will have been loaded, rendering is likely still happening)
        browser.LoadingStateChanged += async (sender, args) =>
        {
            //Wait for the Page to finish loading
            if (args.IsLoading == false)
            {
                await _browser.EvaluateScriptAsync($"window.setEntries(\"{_deviceCatalogService.Entries()}\")");
            }
        };

        //Wait for the MainFrame to finish loading
        browser.FrameLoadEnd += async (sender, args) =>
        {
            //Wait for the MainFrame to finish loading
            if (args.Frame.IsMain)
            {
                await args.Frame.EvaluateScriptAsync($"window.setEntries(\"{_deviceCatalogService.Entries()}\")");
                //args.Frame.ExecuteJavaScriptAsync("alert('MainFrame finished loading');");
            }
        };
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
    }

    public void Drop(IDropInfo dropInfo)
    {
        // TODO
    }
}
