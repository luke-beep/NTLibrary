using Microsoft.UI.Xaml.Controls;

using NTLibrary.ViewModels;

namespace NTLibrary.Views;

public sealed partial class InventoryPage : Page
{
    public InventoryViewModel ViewModel
    {
        get;
    }

    public InventoryPage()
    {
        ViewModel = App.GetService<InventoryViewModel>();
        InitializeComponent();
    }
}
