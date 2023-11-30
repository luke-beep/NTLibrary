namespace NTLibrary.Models;

public class Book
{
    public string Title
    {
        get; set;
    }
    public string Author
    {
        get; set;
    }
    public string? Description
    {
        get; set;
    }
    public Guid? Owner
    {
        get; set;
    }
    public Guid Id
    {
        get; set;
    } = Guid.NewGuid();
}