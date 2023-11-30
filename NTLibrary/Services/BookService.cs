using NTLibrary.Contracts.Services;
using NTLibrary.Core.Contracts.Services;
using NTLibrary.Models;

namespace NTLibrary.Services;

public class BookService : IBookService
{
    private const string _defaultApplicationDataFolder = "NTLibrary/ApplicationData";
    private const string _defaultLocalSettingsFile = "Books.json";

    private readonly IFileService _fileService;
    private List<Book> _books;

    private readonly string _localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    private readonly string _applicationDataFolder;

    public BookService(IFileService fileService)
    {
        _fileService = fileService;
        _applicationDataFolder = Path.Combine(_localApplicationData, _defaultApplicationDataFolder);
        LoadBooks();
    }

    public Book GetBook(Guid id)
    {
        return _books.FirstOrDefault(x => x.Id == id) ?? throw new InvalidOperationException();
    }

    public void AddBook(Book book)
    {
        _books.Add(book);
        SaveChanges();
    }

    public void DeleteBook(Book book)
    {
        _books.Remove(book);
        SaveChanges();
    }

    public void UpdateBook(Book book)
    {
        var oldBook = GetBook(book.Id);
        _books.Remove(oldBook);
        _books.Add(book);
        SaveChanges();
    }

    public List<Book> GetBooks()
    {
        return _books;
    }

    private void SaveChanges()
    {
        _fileService.Save(_applicationDataFolder, _defaultLocalSettingsFile, _books);
    }
    private void LoadBooks()
    {
        _books = _fileService.Read<List<Book>>(_applicationDataFolder, _defaultLocalSettingsFile) ?? new List<Book>();
    }

    public void BorrowBook(Book book, User user)
    {
        if (book.Owner != null)
        {
            Console.WriteLine("Book is already loaned.");
        }

        book.Owner = user.Id;
        SaveChanges();
    }

    public void ReturnBook(Book book, User user)
    {
        if (book.Owner == null)
        {
            throw new InvalidOperationException("Book is not loaned.");
        }

        if (book.Owner != user.Id)
        {
            Console.WriteLine("Book is loaned by another user.");
        }

        book.Owner = null;
        SaveChanges();
    }
}