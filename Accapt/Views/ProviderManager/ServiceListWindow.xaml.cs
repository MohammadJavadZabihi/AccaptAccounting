using Accapt.Views.Loading;
using Accapt.WpfServies;
using System.Windows;
using System.Windows.Controls;

namespace Accapt.Views.ProviderManager
{
    public partial class ServiceListWindow : Window
    {
        private LoadGetApisServies _loadGetApisServies;
        private int _pageNumber = 1;
        private int _pageSize = 10;
        public ServiceListWindow()
        {
            InitializeComponent();
            _loadGetApisServies = new LoadGetApisServies();
        }

        private void btnAddService_Click(object sender, RoutedEventArgs e)
        {
            AddServiceWindow addServiceWindow = new AddServiceWindow();
            addServiceWindow.ShowDialog();
        }

        private async void RefreshPage_Click(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();

            await _loadGetApisServies.LoadProviderSericeList(_pageNumber, serviceListDataGrid, txtSearch.Text, _pageSize);

            loadingWindow.Close();
        }

        private async void btnNext_Click(object sender, RoutedEventArgs e)
        {
            _pageNumber++;

            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();

            var statuce = await _loadGetApisServies.LoadProviderSericeList(_pageNumber, serviceListDataGrid, txtSearch.Text, _pageSize);

            if(!statuce)
            {
                _pageNumber--;
                await _loadGetApisServies.LoadProviderSericeList(_pageNumber, serviceListDataGrid, txtSearch.Text, _pageSize);
            }

            loadingWindow.Close();
        }

        private async void btnBefor_Click(object sender, RoutedEventArgs e)
        {
            if(_pageNumber >= 0)
            {
                _pageNumber--;
                await _loadGetApisServies.LoadProviderSericeList(_pageNumber, serviceListDataGrid, txtSearch.Text, _pageSize);
            }
        }

        private void btnDelet_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();

            await _loadGetApisServies.LoadProviderSericeList(_pageNumber, serviceListDataGrid, txtSearch.Text, _pageSize);

            loadingWindow.Close();
        }

        private async void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            await _loadGetApisServies.LoadProviderSericeList(_pageNumber, serviceListDataGrid, txtSearch.Text, _pageSize);
        }
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
