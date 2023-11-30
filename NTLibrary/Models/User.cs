namespace NTLibrary.Models;

public class User
{
    public string Name
    {
        get; set;
    }
    public Guid Id
    {
        get; set;
    } = Guid.NewGuid();

    public List<Book> Books
    {
        get; set;
    } = new List<Book>();
}