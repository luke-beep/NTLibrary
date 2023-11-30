using CommunityToolkit.Mvvm.ComponentModel;
using NTLibrary.Contracts.Services;
using NTLibrary.Models;
using NTLibrary.Services;

namespace NTLibrary.ViewModels;

public partial class HomeViewModel : ObservableRecipient
{
    private readonly IUserService _userService;
    private readonly IBookService _bookService;

    public HomeViewModel(IUserService userService, IBookService bookService)
    {
        _userService = userService;
        _bookService = bookService;
    }

    public void AddUser(string name)
    {
        var user = new User
        {
            Name = name
        };
        _userService.AddUser(user);
    }

    public void DeleteUser(string name)
    {
        var user = _userService.GetUsers().FirstOrDefault(x => x.Name == name);
        var books = _bookService.GetBooks().Where(x => x.Owner == user.Id);
        if (user != null && books == null)
        {
            _userService.DeleteUser(user);
        }
    }

    public void UpdateUserName(string oldName, string newName)
    {
        var user = _userService.GetUsers().FirstOrDefault(x => x.Name == oldName);
        if (user != null)
        {
            _userService.UpdateName(user, newName);
        }
    }

    public List<User> GetUsers()
    {
        return _userService.GetUsers();
    }

    public List<Book> GetBooks()
    {
        return _bookService.GetBooks();
    }
}
