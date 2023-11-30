using Microsoft.UI.Xaml.Controls;

using NTLibrary.ViewModels;

namespace NTLibrary.Views;

// To learn more about WebView2, see https://docs.microsoft.com/microsoft-edge/webview2/.
public sealed partial class OnlineLibPage : Page
{
    public OnlineLibViewModel ViewModel
    {
        get;
    }

    public OnlineLibPage()
    {
        ViewModel = App.GetService<OnlineLibViewModel>();
        InitializeComponent();

        ViewModel.WebViewService.Initialize(WebView);
    }
}
