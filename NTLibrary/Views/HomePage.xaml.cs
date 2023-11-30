using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using NTLibrary.ViewModels;

namespace NTLibrary.Views;

public sealed partial class HomePage : Page
{
    public HomeViewModel ViewModel
    {
        get;
    }

    public HomePage()
    {
        ViewModel = App.GetService<HomeViewModel>();
        InitializeComponent();
        UserCount.Text = ViewModel.GetUsers().Count.ToString();
        BookCount.Text = ViewModel.GetBooks().Count.ToString();
    }

    private void OpenAddUserDialog(object sender, RoutedEventArgs e)
    {
        AddUserDialog.IsOpen = true;
    }

    private void AddUserButtonClick(TeachingTip sender, object args)
    {
        ViewModel.AddUser(UserName.Text);
    }


    private void OpenDeleteUserDialog(object sender, RoutedEventArgs e)
    {
        DeleteUserDialog.IsOpen = true;
        UserList.ItemsSource = ViewModel.GetUsers().Select(x => x.Name).ToList();
    }

    private void DeleteUserButtonClick(TeachingTip sender, object args)
    {
        var name = UserList.SelectedItem.ToString();
        if (name != null)
        {
            ViewModel.DeleteUser(name);
        }
    }
}
