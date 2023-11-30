using NTLibrary.Models;

namespace NTLibrary.Contracts.Services;

public interface ILibraryService
{
    void BorrowBook(Book book, User user);
    void ReturnBook(Book book, User user);
    List<Book> GetBorrowedBooks(User user);
}