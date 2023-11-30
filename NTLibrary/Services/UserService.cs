using NTLibrary.Contracts.Services;
using NTLibrary.Core.Contracts.Services;
using NTLibrary.Models;

namespace NTLibrary.Services;

public class UserService : IUserService
{
    private const string _defaultApplicationDataFolder = "NTLibrary/ApplicationData";
    private const string _defaultLocalSettingsFile = "Users.json";

    private readonly IFileService _fileService;
    private List<User> _users;

    private readonly string _localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    private readonly string _applicationDataFolder;

    public UserService(IFileService fileService)
    {
        _fileService = fileService;
        _applicationDataFolder = Path.Combine(_localApplicationData, _defaultApplicationDataFolder);
        LoadUsers();
    }

    public User GetUser(Guid id)
    {
        return _users.FirstOrDefault(x => x.Id == id) ?? throw new InvalidOperationException();
    }

    public void AddUser(User user)
    {
        _users.Add(user);
        SaveChanges();
    }

    public void DeleteUser(User user)
    {
        _users.Remove(user);
        SaveChanges();
    }

    public void UpdateName(User user, string name)
    {
        var oldUser = GetUser(user.Id);
        _users.Remove(oldUser);
        user.Name = name;
        _users.Add(user);
        SaveChanges();
    }

    public void UpdateUser(User user)
    {
        var oldUser = GetUser(user.Id);
        _users.Remove(oldUser);
        _users.Add(user);
        SaveChanges();
    }

    public List<User> GetUsers()
    {
        return _users;
    }

    private void LoadUsers()
    {
        _users = _fileService.Read<List<User>>(_applicationDataFolder, _defaultLocalSettingsFile) ?? new List<User>();
    }

    private void SaveChanges()
    {
        _fileService.Save(_applicationDataFolder, _defaultLocalSettingsFile, _users);
    }

    public void BorrowBook(Book book, User user)
    {
        if (user.Books.Contains(book))
        {
            Console.WriteLine("User already has this book.");
        }

        user.Books.Add(book);
        SaveChanges();
    }

    public void ReturnBook(Book book, User user)
    {
        if (!user.Books.Contains(book))
        {
            Console.WriteLine("User does not have this book.");
        }

       
        user.Books.Remove(book);
        SaveChanges();
    }

    public List<Book> GetBorrowedBooks(User user)
    {
        return user.Books;
    }
}