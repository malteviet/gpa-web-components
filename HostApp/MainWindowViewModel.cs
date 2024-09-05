using System.Collections.ObjectModel;
using System.Windows;
using CefSharp;
using CefSharp.JavascriptBinding;
using CefSharp.Wpf.HwndHost;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GongSolutions.Wpf.DragDrop;
using DataObject = System.Windows.DataObject;

namespace HostApp;

public partial class MainWindowViewModel : ObservableObject, IDropTarget
{
    private readonly ChromiumWebBrowser _browser;

    [ObservableProperty]
    private string _address = $"{AppDefaults.LocalSchemeName}://{AppDefaults.LocalDomainName}/device-lib.html";
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
                // _browser.BrowserCore.ExecuteScriptAsync($"console.log(\"Test\")");
                // _browser.BrowserCore.ExecuteScriptAsync($"console.log(window)");
                // _browser.BrowserCore.ExecuteScriptAsync($"window.component.setEntries({_deviceCatalogService.Entries()})");
            }
        };

        //Wait for the MainFrame to finish loading
        browser.FrameLoadEnd += async (sender, args) =>
        {
            //Wait for the MainFrame to finish loading
            if (args.Frame.IsMain) { }
        };
    }

    public ObservableCollection<Device> Items { get; } = new();

    [RelayCommand]
    private void OpenDevTools()
    {
        this._browser.ShowDevTools();
    }

    [RelayCommand]
    private async Task NavigateAsync(string address)
    {
        await _browser.LoadUrlAsync(address);
    }

    [RelayCommand]
    private void ExecuteScript()
    {
        var entries = _deviceCatalogService.EntriesAsJson();
        _browser.BrowserCore.ExecuteScriptAsync($"console.log(\"Test\")");
        _browser.BrowserCore.ExecuteScriptAsync($"window.setEntries(\'{entries}\')");
    }

    [RelayCommand]
    private async Task DevPageAsync()
    {
        await _browser.LoadUrlAsync("http://localhost:5173/device-lib");
    }

    public void DragOver(IDropInfo dropInfo)
    {
        var dataObject = dropInfo.Data as DataObject;
        var urn = dataObject?.GetText();
        if (urn != null)
        {
            dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
            dropInfo.Effects = DragDropEffects.Copy;
        }
    }

    public void Drop(IDropInfo dropInfo)
    {
        var dataObject = dropInfo.Data as DataObject;
        var urn = dataObject?.GetText();
        if (urn != null)
        {
            if (_deviceCatalogService.GetDeviceByUrn(urn) is { } device)
                Items.Insert(dropInfo.InsertIndex, device);
        }
    }
}
