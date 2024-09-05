﻿using System.Collections.ObjectModel;
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
    private void Navigate(string address)
    {
        _browser.Address = address;
        _browser.BrowserCore.Reload();
    }

    [RelayCommand]
    private void ExecuteScript()
    {
        var entries = _deviceCatalogService.EntriesAsJson();
        _browser.BrowserCore.ExecuteScriptAsync($"console.log(\"Test\")");
        _browser.BrowserCore.ExecuteScriptAsync($"console.log(window)");
        _browser.BrowserCore.ExecuteScriptAsync($"window.component.setEntries(\'{entries}\')");
    }

    [RelayCommand]
    private void DevPage()
    {
        Address = "http://localhost:5173/device-lib";
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
