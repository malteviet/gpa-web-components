using System.Windows;
using CefSharp;
using CefSharp.SchemeHandler;
using CefSharp.Wpf;

namespace HostApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        var settings = new CefSettings();
        settings.RemoteDebuggingPort = 9222;
        settings.RegisterScheme(
            new CefCustomScheme
            {
                SchemeName = AppDefaults.LocalSchemeName,
                DomainName = AppDefaults.LocalDomainName,
                SchemeHandlerFactory = new FolderSchemeHandlerFactory(
                    "embedded",
                    hostName: AppDefaults.LocalDomainName
                )
            }
        );

        Cef.Initialize(settings);
        base.OnStartup(e);
    }
}
