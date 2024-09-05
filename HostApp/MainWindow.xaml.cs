using System.Windows.Input;

namespace HostApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel(this.Browser);
    }

    private void AddressBox_OnKeyUp(object sender, KeyEventArgs e)
    {
    }
}