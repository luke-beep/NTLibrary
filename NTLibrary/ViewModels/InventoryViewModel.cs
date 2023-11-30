using System.Collections.ObjectModel;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using NTLibrary.Contracts.Services;
using NTLibrary.Contracts.ViewModels;
using NTLibrary.Core.Contracts.Services;
using NTLibrary.Models;

namespace NTLibrary.ViewModels;

public partial class InventoryViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IBookService _bookService;

    public ObservableCollection<Book> Source { get; } = new ObservableCollection<Book>();

    public InventoryViewModel(INavigationService navigationService, IBookService bookService)
    {
        _navigationService = navigationService;
        _bookService = bookService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        Source.Clear();

        var data = _bookService.GetBooks();
        foreach (var item in data)
        {
            Source.Add(item);
        }
    }

    public void OnNavigatedFrom()
    {
    }

    [RelayCommand]
    private void OnItemClick(Book? clickedItem)
    {
        if (clickedItem != null)
        {
            _navigationService.SetListDataItemForNextConnectedAnimation(clickedItem);
            _navigationService.NavigateTo(typeof(LibraryDetailViewModel).FullName!, clickedItem.Id);
        }
    }
}
