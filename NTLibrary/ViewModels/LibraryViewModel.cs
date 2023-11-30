using System.Collections.ObjectModel;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using NTLibrary.Contracts.Services;
using NTLibrary.Contracts.ViewModels;
using NTLibrary.Core.Contracts.Services;
using NTLibrary.Models;

namespace NTLibrary.ViewModels;

public partial class LibraryViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IBookService _bookService;

    public ObservableCollection<Book> Source { get; } = new ObservableCollection<Book>();

    public LibraryViewModel(INavigationService navigationService, IBookService bookService)
    {
        _navigationService = navigationService;
        _bookService = bookService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        Source.Clear();

        var data = _bookService.GetBooks();
        foreach (var item in data)
        {
            Source.Add(item);
        }
    }

    public void OnNavigatedFrom()
    {
    }

    [RelayCommand]
    private void OnItemClick(Book? clickedItem)
    {
        if (clickedItem != null)
        {
            _navigationService.SetListDataItemForNextConnectedAnimation(clickedItem);
            _navigationService.NavigateTo(typeof(LibraryDetailViewModel).FullName!, clickedItem.Id);
        }
    }

    public void AddBook(string title, string author, string description)
    {
        var book = new Book
        {
            Title = title,
            Author = author,
            Description = description,
        };
        _bookService.AddBook(book);
    }

    public void DeleteBook(string title)
    {
        var book = _bookService.GetBooks().FirstOrDefault(x => x.Title == title);
        if (book != null)
        {
            _bookService.DeleteBook(book);
        }
    }

    public List<Book> GetBooks()
    {
        return _bookService.GetBooks();
    }

}
