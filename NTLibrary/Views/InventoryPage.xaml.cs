using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NTLibrary.Models;
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
        UserList.ItemsSource = ViewModel.GetUsers().Select(x => x.Name).ToList();
    }

    private void OpenUserList(object sender, RoutedEventArgs e) => UserListDialog.IsOpen = true;


    private void SelectUser(TeachingTip sender, object args)
    {
        var username = UserList.SelectedItem.ToString();
        if (username != null)
        {
            OpenUserListButton.Content = username;
            var user = ViewModel.GetUsers().FirstOrDefault(x => x.Name == username);
            if (user != null)
            {
                ViewModel.UpdateInventory(user);
            }
        }
    }
}
