using NTLibrary.Models;

namespace NTLibrary.Contracts.Services;

public interface IUserService
{
    User GetUser(Guid id);

    void AddUser(User user);

    void DeleteUser(User user);

    void UpdateUser(User user);

    void UpdateName(User user, string name);

    List<User> GetUsers();

    void BorrowBook(Book book, User user);

    void ReturnBook(Book book, User user);

    List<Book> GetBorrowedBooks(User user);
}