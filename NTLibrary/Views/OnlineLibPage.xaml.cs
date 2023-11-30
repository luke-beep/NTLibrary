using Microsoft.UI.Xaml.Controls;

using NTLibrary.ViewModels;

namespace NTLibrary.Views;

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
