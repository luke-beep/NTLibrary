using NTLibrary.Contracts.Services;
using NTLibrary.Models;

namespace NTLibrary.Services;

public class LibraryService : ILibraryService
{
    private readonly IBookService _bookService;
    private readonly IUserService _userService;

    public LibraryService(IUserService userService, IBookService bookService)
    {
        _userService = userService;
        _bookService = bookService;
    }

    public void BorrowBook(Book book, User user)
    {
        _bookService.BorrowBook(book, user);
        _userService.BorrowBook(book, user);
    }

    public void ReturnBook(Book book, User user)
    {
        _bookService.ReturnBook(book, user);
        _userService.ReturnBook(book, user);
    }

    public List<Book> GetBorrowedBooks(User user)
    {
        return _userService.GetBorrowedBooks(user);
    }
}