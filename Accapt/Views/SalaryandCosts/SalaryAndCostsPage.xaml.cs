using Accapt.DataLayer.Entities;
using Accapt.Views.Loading;
using Accapt.WpfServies;
using ApiRequest.Net.CallApi;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace Accapt.Views.SalaryandCosts
{
    public partial class SalaryAndCostsPage : Page
    {
        private CallApi _callApi;
        private LoadGetApisServies _LoadGetApisServies;
        private int _currentPage = 1;
        private int _pageSize = 10;
        private string? _url = ConfigurationManager.AppSettings["ApiURL"];

        public SalaryAndCostsPage()
        {
            InitializeComponent();
            _callApi = new CallApi();
            _LoadGetApisServies = new LoadGetApisServies();
        }

        private async void RefreshPage_Click(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();

            await _LoadGetApisServies.LoadedSallaryandCosts(_currentPage, sallaryDataGrid, txtSearch.Text, _pageSize);

            loadingWindow.Close();
        }

        private void btnAddPepole_Click(object sender, RoutedEventArgs e)
        {
            AddSallaryandCosts addSallaryandCosts = new AddSallaryandCosts();
            addSallaryandCosts.ShowDialog();
        }

        private async void btnBefor_Click(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();

            _currentPage--;

            if (_currentPage <= 0)
            {
                _currentPage++;
            }
            else
            {
                await _LoadGetApisServies.LoadedSallaryandCosts(_currentPage, sallaryDataGrid, txtSearch.Text, _pageSize);
            }

            loadingWindow.Close();
        }

        private async void btnNext_Click(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();

            _currentPage++;

            var statudce = await _LoadGetApisServies.LoadedSallaryandCosts(_currentPage, sallaryDataGrid, txtSearch.Text, _pageSize);

            if (!statudce)
            {
                _currentPage--;
                await _LoadGetApisServies.LoadedSallaryandCosts(_currentPage, sallaryDataGrid, txtSearch.Text, _pageSize);
            }

            loadingWindow.Close();
        }

        private async void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            await _LoadGetApisServies.LoadedSallaryandCosts(_currentPage, sallaryDataGrid, txtSearch.Text, _pageSize);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();

            await _LoadGetApisServies.LoadedSallaryandCosts(_currentPage, sallaryDataGrid, txtSearch.Text, _pageSize);

            loadingWindow.Close();
        }

        private async void btnDelet_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("آیا از حذف آیتم انتخوابی اطمینان دارید؟", "سوال", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                LoadingWindow loadingWindow = new LoadingWindow();
                loadingWindow.Show();

                if (sallaryDataGrid.SelectedItem is SallaryAndCosts sallary)
                {
                    int sallaryId = sallary.SallaryAndCostsId;
                    var responseMessage = await _callApi.SendDeletRequest<bool>($"{_url}/api/SalaryAndCostsManager/DeletSallary/{UserSession.Instance.UserId}/{sallaryId}", jwt: UserSession.Instance.JwtToken);
                    loadingWindow.Close();

                    if (responseMessage.IsSuccess)
                    {
                        MessageBox.Show("محصول انتخوابی با موفقیت حذف شد", "موفقیت", MessageBoxButton.OK, MessageBoxImage.Information);

                        await _LoadGetApisServies.LoadedSallaryandCosts(_currentPage, sallaryDataGrid, txtSearch.Text, _pageSize);
                    }
                    else
                    {
                        MessageBox.Show($"{responseMessage.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

            }
        }

        private async void btnEdite_Click(object sender, RoutedEventArgs e)
        {
            if (sallaryDataGrid.SelectedItem is SallaryAndCosts sallary)
            {
                int sallaryId = sallary.SallaryAndCostsId;

                var respnoseMessage = await _callApi.SendGetRequest<SallaryAndCosts?>(
                    $"{_url}/api/SalaryAndCostsManager/GetSingle/{UserSession.Instance.UserId}/{sallaryId}", jwt: UserSession.Instance.JwtToken);

                if (respnoseMessage.IsSuccess)
                {
                    AddSallaryandCosts addSallaryandCosts = new AddSallaryandCosts();
                    addSallaryandCosts.SetData(respnoseMessage.Data, sallaryId);

                    addSallaryandCosts.ShowDialog();
                }
                else
                {
                    MessageBox.Show($"{respnoseMessage.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
