using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using NTLibrary.ViewModels;

namespace NTLibrary.Views;

public sealed partial class LibraryPage : Page
{
    public LibraryViewModel ViewModel
    {
        get;
    }

    public LibraryPage()
    {
        ViewModel = App.GetService<LibraryViewModel>();
        InitializeComponent();
    }

    private void OpenAddBookDialog(object sender, RoutedEventArgs e)
    {
        AddBookDialog.IsOpen = true;
    }

    private void OpenDeleteBookDialog(object sender, RoutedEventArgs e)
    {
        DeleteBookDialog.IsOpen = true;
        BookList.ItemsSource = ViewModel.GetBooks().Select(x => x.Title).ToList();
    }

    private void DeleteBookButtonClick(TeachingTip sender, object args)
    {
        var title = BookList.SelectedItem.ToString();
        if (title != null)
        {
            ViewModel.DeleteBook(title);
        }
    }

    private void AddBookButtonClick(TeachingTip sender, object args)
    {
        ViewModel.AddBook(BookTitle.Text, BookAuthor.Text, BookDescription.Text);
    }
}
