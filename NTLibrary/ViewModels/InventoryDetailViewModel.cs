using CommunityToolkit.Mvvm.ComponentModel;
using NTLibrary.Contracts.Services;
using NTLibrary.Contracts.ViewModels;
using NTLibrary.Core.Contracts.Services;
using NTLibrary.Models;

namespace NTLibrary.ViewModels;

public partial class InventoryDetailViewModel : ObservableRecipient, INavigationAware
{
    private readonly IBookService _bookService;

    [ObservableProperty]
    private Book? item;

    public InventoryDetailViewModel(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        if (parameter is Guid bookID)
        {
            var data = _bookService.GetBooks();
            Item = data.First(i => i.Id == bookID);
        }
    }

    public void OnNavigatedFrom()
    {
    }
}
