using Accapt.Views.Loading;
using Accapt.WpfServies;
using ApiRequest.Net.CallApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Accapt.Views.ProviderManager
{
    /// <summary>
    /// Interaction logic for ProvidermanagerShow.xaml
    /// </summary>
    public partial class ProvidermanagerShow : Page
    {
        private LoadGetApisServies _loadGetApisServies;
        private CallApi _callApi;
        private int _pageNumber = 1;
        private int _pageSize = 10;
        public ProvidermanagerShow()
        {
            InitializeComponent();
            _loadGetApisServies = new LoadGetApisServies();
            _callApi = new CallApi();
        }

        private void btnAddProvider_Click(object sender, RoutedEventArgs e)
        {
            AddProviderManagerWindow addProviderManagerWindow = new AddProviderManagerWindow();
            addProviderManagerWindow.ShowDialog();
        }

        private async void RefreshPage_Click(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();

            await _loadGetApisServies.LoadProviderSerice(_pageNumber, PrividerManagerDataGrid, txtSearch.Text, _pageSize);

            loadingWindow.Close();
        }

        private async void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            await _loadGetApisServies.LoadProviderSerice(_pageNumber, PrividerManagerDataGrid, txtSearch.Text, _pageSize);
        }

        private async void btnNext_Click(object sender, RoutedEventArgs e)
        {
            _pageNumber++;
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();

            var response = await _loadGetApisServies.LoadProviderSerice(_pageNumber, PrividerManagerDataGrid, txtSearch.Text, _pageSize);

            if(!response)
            {
                _pageNumber--;
                await _loadGetApisServies.LoadProviderSerice(_pageNumber, PrividerManagerDataGrid, txtSearch.Text, _pageSize);
            }

            loadingWindow.Close();
        }

        private async void btnBefor_Click(object sender, RoutedEventArgs e)
        {
            _pageNumber--;
            if(_pageNumber <= 0)
            {
                _pageNumber++;
            }
            else
            {
                LoadingWindow loadingWindow = new LoadingWindow();
                loadingWindow.Show();

                await _loadGetApisServies.LoadProviderSerice(_pageNumber, PrividerManagerDataGrid, txtSearch.Text, _pageSize);

                loadingWindow.Close();
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();

            await _loadGetApisServies.LoadProviderSerice(_pageNumber, PrividerManagerDataGrid, txtSearch.Text, _pageSize);

            loadingWindow.Close();
        }

        private void btnAddService_Click(object sender, RoutedEventArgs e)
        {
            ServiceListWindow serviceListWindow = new ServiceListWindow();
            serviceListWindow.ShowDialog();
        }
    }
}
