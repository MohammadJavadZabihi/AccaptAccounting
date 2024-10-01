using Accapt.DataLayer.Entities;
using Accapt.Views.Loading;
using Accapt.WpfServies;
using ApiRequest.Net.CallApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Accapt.Views.BankChek
{
    public partial class ChekBankShowWindow : Window
    {
        private LoadGetApisServies _loadGetApisServies;
        private string? _url = ConfigurationManager.AppSettings["ApiURL"];
        private CallApi _callApi;
        private int _currentPage = 1;
        private int _pageSize = 10;
        private int _click = 0;
        public ChekBankShowWindow()
        {
            InitializeComponent();
            _loadGetApisServies = new LoadGetApisServies();
            _callApi = new CallApi();
        }

        private async void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            await _loadGetApisServies.LoadedBanckCheck(_currentPage, chekDataGrid, txtSearch.Text, _pageSize);
        }

        private async void btnBefor_Click(object sender, RoutedEventArgs e)
        {
            _currentPage--;

            if (_currentPage <= 0)
            {
                _currentPage++;
            }
            else
            {
                LoadingWindow loadingWindow = new LoadingWindow();
                loadingWindow.Show();
                await _loadGetApisServies.LoadedBanckCheck(_currentPage, chekDataGrid, txtSearch.Text, _pageSize);
                loadingWindow.Close();
            }
        }

        private async void btnNext_Click(object sender, RoutedEventArgs e)
        {
            _currentPage++;
            var statuceOfLoad = await _loadGetApisServies.LoadedBanckCheck(_currentPage, chekDataGrid, txtSearch.Text, _pageSize);

            if(statuceOfLoad)
            {
                LoadingWindow loadingWindow = new LoadingWindow();
                loadingWindow.Show();
                await _loadGetApisServies.LoadedBanckCheck(_currentPage, chekDataGrid, txtSearch.Text, _pageSize);
                loadingWindow.Close();
            }
            else
            {
                _currentPage--;
            }
            
        }


        private void btnAddInoice_Click(object sender, RoutedEventArgs e)
        {
            AddBankChekWindow addBankChekWindow = new AddBankChekWindow();
            addBankChekWindow.ShowDialog();
        }

        private async void RefreshPage_Click(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();
            await _loadGetApisServies.LoadedBanckCheck(_currentPage, chekDataGrid, txtSearch.Text, _pageSize);
            loadingWindow.Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();
            await _loadGetApisServies.LoadedBanckCheck(_currentPage, chekDataGrid, txtSearch.Text, _pageSize);
            loadingWindow.Close();
        }

        private async void btnDelet_Click(object sender, RoutedEventArgs e)
        {
            if (_click == 0)
            {
                _click++;
                LoadingWindow loadingWindow = new LoadingWindow();
                try
                {
                    if (chekDataGrid.SelectedItem is Chek chek)
                    {
                        string chekNumber = chek.CheckNumber;

                        if (chekNumber != null)
                        {
                            if (MessageBox.Show("آیا از حذف ایتم انتخوابی مطمعئن هستید؟", "اطمینان", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                loadingWindow.Show();
                                var responseMessage = await _callApi.SendDeletRequest<bool>($"{_url}/api/ChekManger/Delet?chekNumber={chekNumber}&userId={UserSession.Instance.UserId}", jwt: UserSession.Instance.JwtToken);
                                _click--;
                                loadingWindow.Close();
                                if (responseMessage.IsSuccess)
                                {
                                    MessageBox.Show("محصول انتخوابی با موفقیت حذف شد", "موفقیت", MessageBoxButton.OK, MessageBoxImage.Information);
                                    await _loadGetApisServies.LoadedBanckCheck(_currentPage, chekDataGrid, txtSearch.Text, _pageSize);
                                }
                                else if (responseMessage.StatusCode == null)
                                {
                                    _click--;
                                    MessageBox.Show($"لطفا اتصال خود را به اینترنت بررسی کنید و دوباره تلاش کنید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                                else if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                                {
                                    _click--;
                                    MessageBox.Show($"خطای احراز حویت", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                                else if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
                                {
                                    _click--;
                                    MessageBox.Show($"لطفا در ارسال اطلاعات دقت فرمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                                else if (responseMessage.StatusCode == HttpStatusCode.InternalServerError)
                                {
                                    _click--;
                                    MessageBox.Show($"خطا از طرف سرور, لطفا بعدا دوباره تلاش کنید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _click--;
                    MessageBox.Show($"{ex.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
