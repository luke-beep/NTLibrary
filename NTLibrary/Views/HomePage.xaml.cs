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
        BorrowedBookCount.Text = ViewModel.GetBooks().Count(x => x.Owner != null).ToString();
        BorrowedUserCount.Text = ViewModel.GetUsers().Count(x => x.Books.Count > 0).ToString();
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

    private void UpdateUserName(TeachingTip sender, object args)
    {
        var oldName = UpdateUsernameList.SelectedItem.ToString();
        var newName = UpdateUserNameTextBox.Text;
        if (oldName != null && newName != null)
        {
            ViewModel.UpdateUserName(oldName, newName);
        }
    }

    private void OpenUpdateUsernameDialog(object sender, RoutedEventArgs e)
    {
        UpdateUsernameDialog.IsOpen = true;
        UpdateUsernameList.ItemsSource = ViewModel.GetUsers().Select(x => x.Name).ToList();
    }

    private void OpenAllBorrowedBooks(object sender, RoutedEventArgs e)
    {
        BorrowedBooksDialog.IsOpen = true;
        BorrowedBooksList.ItemsSource = ViewModel.GetBooks().Where(x => x.Owner != null).Select(x => x.Title).ToList();
    }


    private void OpenAllUsersDialog(object sender, RoutedEventArgs e)
    {
        AllUsersDialog.IsOpen = true;
        AllUserList.ItemsSource = ViewModel.GetUsers().Select(x => x.Name).ToList();
    }

    private void OpenAllBooks(object sender, RoutedEventArgs e)
    {
        AllBooksDialog.IsOpen = true;
        AllBooksList.ItemsSource = ViewModel.GetBooks().Select(x => x.Title).ToList();
    }

    private void OpenAllBorrowedUsers(object sender, RoutedEventArgs e)
    {
        AllBorrowedUsersDialog.IsOpen = true;
        AllBorrowedUsersList.ItemsSource = ViewModel.GetUsers().Where(x => x.Books.Count > 0).Select(x => x.Name).ToList();
    }
}
