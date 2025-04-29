using LocalFictionReader.Models;
using LocalFictionReader.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace LocalFictionReader.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly BookParserService _parserService;
        
        public ObservableCollection<Book> Books { get; } = new ObservableCollection<Book>();

        public IAsyncRelayCommand OpenFileCommand { get; }

        public MainViewModel(BookParserService parserService)
        {
            _parserService = parserService;
            OpenFileCommand = new AsyncRelayCommand(OpenFileAsync);
        }

        private async Task OpenFileAsync()
        {
            var picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.List,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            picker.FileTypeFilter.Add(".txt");

            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                var book = await _parserService.ParseBookAsync(file.Path);
                Books.Add(book);
            }
        }
    }
}