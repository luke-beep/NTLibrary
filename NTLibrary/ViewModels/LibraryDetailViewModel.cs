using System.Runtime.InteropServices;
using CommunityToolkit.Mvvm.ComponentModel;
using NTLibrary.Contracts.Services;
using NTLibrary.Contracts.ViewModels;
using NTLibrary.Core.Contracts.Services;
using NTLibrary.Models;

namespace NTLibrary.ViewModels;

public partial class LibraryDetailViewModel : ObservableRecipient, INavigationAware
{
    private readonly IBookService _bookService;
    private readonly ILibraryService _libraryService;
    private readonly IUserService _userService;

    [ObservableProperty]
    private Book? book;

    public LibraryDetailViewModel(IBookService bookService, ILibraryService libraryService, IUserService userService)
    {
        _bookService = bookService;
        _libraryService = libraryService;
        _userService = userService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        if (parameter is Guid bookID)
        {
            var data = _bookService.GetBooks();
            Book = data.First(i => i.Id == bookID);
        }
    }

    public void OnNavigatedFrom()
    {
    }

    public void ReturnBook(Guid bookid, Guid userid)
    {
        var book = _bookService.GetBook(bookid);
        var user = _userService.GetUser(userid);
        if (book.Owner != user.Id)
        {
            return;
        }
        _libraryService.ReturnBook(book, user);
    }

    public void BorrowBook(Guid bookid, Guid userid)
    {
        var book = _bookService.GetBook(bookid);
        var user = _userService.GetUser(userid);
        if (book.Owner != null)
        {
            return;
        }
        _libraryService.BorrowBook(book, user);
    }

    public List<User> GetUsers()
    {
        return _userService.GetUsers();
    }
}
