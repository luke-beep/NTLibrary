using CommunityToolkit.WinUI.UI.Animations;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using NTLibrary.Contracts.Services;
using NTLibrary.Models;
using NTLibrary.ViewModels;

namespace NTLibrary.Views;

public sealed partial class LibraryDetailPage : Page
{
    public LibraryDetailViewModel ViewModel
    {
        get;
    }

    public LibraryDetailPage()
    {
        ViewModel = App.GetService<LibraryDetailViewModel>();
        InitializeComponent();
        UserList.ItemsSource = ViewModel.GetUsers().Select(x => x.Name).ToList();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        this.RegisterElementForConnectedAnimation("animationKeyContentGrid", itemHero);
    }

    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
    {
        base.OnNavigatingFrom(e);
        if (e.NavigationMode == NavigationMode.Back)
        {
            var navigationService = App.GetService<INavigationService>();

            if (ViewModel.Book != null)
            {
                navigationService.SetListDataItemForNextConnectedAnimation(ViewModel.Book);
            }
        }
    }

    private void Return(object sender, RoutedEventArgs e)
    {
        var userName = UserList.SelectedItem.ToString();
        if (userName != null)
        {
            var user = ViewModel.GetUsers().FirstOrDefault(x => x.Name == userName);
            if (ViewModel.Book != null && user != null)
            {
                var bookId = ViewModel.Book.Id;
                var userId = user.Id;
                ViewModel.ReturnBook(bookId, userId);
            }
        }
    }

    private void Borrow(object sender, RoutedEventArgs e)
    {
        var userName = UserList.SelectedItem.ToString();
        var user = ViewModel.GetUsers().FirstOrDefault(x => x.Name == userName);
        if (ViewModel.Book != null && user != null)
        {
            var bookId = ViewModel.Book.Id;
            var userId = user.Id;
            ViewModel.BorrowBook(bookId, userId);
        }
    }
}