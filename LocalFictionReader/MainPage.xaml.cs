using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace LocalFictionReader
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = Ioc.Default.GetService<MainViewModel>();

        public MainPage()
        {
            this.InitializeComponent();
            DataContext = ViewModel;
        }

        private Book SelectedBook => (Book)BooksList.SelectedItem;
    }
}