using NTLibrary.Models;

namespace NTLibrary.Contracts.Services;

public interface IBookService
{
    Book GetBook(Guid id);

    void AddBook(Book book);

    void DeleteBook(Book book);

    void UpdateBook(Book book);

    List<Book> GetBooks();

    void BorrowBook(Book book, User user);

    void ReturnBook(Book book, User user);
}