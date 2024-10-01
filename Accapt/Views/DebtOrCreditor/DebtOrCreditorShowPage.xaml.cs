using Accapt.DataLayer.Entities;
using Accapt.Views.Loading;
using Accapt.WpfServies;
using ApiRequest.Net.CallApi;
using Microsoft.VisualBasic.ApplicationServices;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Accapt.Views.DebtOrCreditor
{
    public partial class DebtOrCreditorShowPage : Page
    {
        private CallApi _callApi;
        private LoadGetApisServies _LoadGetApisServies;
        private string? url = ConfigurationManager.AppSettings["ApiURL"];
        private int _currentPage = 1;
        private int _pageSize = 10;
        private int _click = 0;
        public DebtOrCreditorShowPage()
        {
            InitializeComponent();
            _callApi = new CallApi();
            _LoadGetApisServies = new LoadGetApisServies();
        }

        private async void btnDelet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (debtOrCreditorDataGrid.SelectedItem is DebtorCreditor debtorCreditor)
                {
                    int creditorId = debtorCreditor.DebtorCreditorID;

                    if (creditorId >= 0)
                    {
                        if (MessageBox.Show("آیا از حذف ایتم انتخوابی مطمعئن هستید؟", "اطمینان", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            LoadingWindow loadingWindow = new LoadingWindow();
                            loadingWindow.Show();
                            var data = new
                            {
                                CreditorId = creditorId,
                                UserId = UserSession.Instance.UserId
                            };

                            var responseMessage = await _callApi.SendDeletRequest<bool>($"{url}/api/DebtorCreditorManager/DeletDebtorCreditor", data, UserSession.Instance.JwtToken);

                            loadingWindow.Close();

                            if (responseMessage.IsSuccess)
                            {
                                MessageBox.Show("محصول انتخوابی با موفقیت حذف شد", "موفقیت", MessageBoxButton.OK, MessageBoxImage.Information);
                                await _LoadGetApisServies.LoadedDebtOrCredito(_currentPage, debtOrCreditorDataGrid, txtSearch.Text, _pageSize);
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
                throw new ArgumentException(nameof(ex.Message));
            }
        }

        private void btnIsPay_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btnEdite_Click(object sender, RoutedEventArgs e)
        {
            if (debtOrCreditorDataGrid.SelectedItem is DebtorCreditor debtorCreditor)
            {
                var data = new
                {
                    CreditorId = debtorCreditor.DebtorCreditorID,
                    UserId = UserSession.Instance.UserId
                };

                var responsMessage = await _callApi.SendGetRequest<DebtorCreditor?>
                    ($"{url}/api/DebtorCreditorManager/GetSingleProcess", data, UserSession.Instance.JwtToken);

                if (responsMessage.IsSuccess)
                {
                    AddOrEditeDebtOrCreditor addOrEditeDebtOrCreditor = new AddOrEditeDebtOrCreditor();
                    addOrEditeDebtOrCreditor.SetDebtOrCreditor(debtorCreditor.DebtorCreditorID, responsMessage.Data);
                    addOrEditeDebtOrCreditor.ShowDialog();
                }
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();

            await _LoadGetApisServies.LoadedDebtOrCredito(_currentPage,debtOrCreditorDataGrid, txtSearch.Text, _pageSize);

            loadingWindow.Close();
        }

        private async void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            await _LoadGetApisServies.LoadedDebtOrCredito(_currentPage, debtOrCreditorDataGrid, txtSearch.Text, _pageSize);
        }

        private async void btnBefor_Click(object sender, RoutedEventArgs e)
        {
            _currentPage--;

            if( _currentPage <= 0 )
            {
                _currentPage++;
            }
            else
            {
                LoadingWindow loadingWindow = new LoadingWindow();
                loadingWindow.Show();

                await _LoadGetApisServies.LoadedDebtOrCredito(_currentPage, debtOrCreditorDataGrid, txtSearch.Text, _pageSize);

                loadingWindow.Close();
            }
        }

        private async void btnNext_Click(object sender, RoutedEventArgs e)
        {
            _currentPage++;

            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();

            var satucrOfLoading = await _LoadGetApisServies.LoadedDebtOrCredito(_currentPage, debtOrCreditorDataGrid, txtSearch.Text, _pageSize);

            loadingWindow.Close();
            if (!satucrOfLoading)
            {
                _currentPage--;
                await _LoadGetApisServies.LoadedDebtOrCredito(_currentPage, debtOrCreditorDataGrid, txtSearch.Text, _pageSize);
            }
        }

        private async void btnAddDebtOrCreditor_Click(object sender, RoutedEventArgs e)
        {
            AddOrEditeDebtOrCreditor addOrEditeDebtOrCreditor = new AddOrEditeDebtOrCreditor();
            addOrEditeDebtOrCreditor.ShowDialog();
        }

        private async void RefreshPage_Click(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();

            await _LoadGetApisServies.LoadedDebtOrCredito(_currentPage, debtOrCreditorDataGrid, txtSearch.Text, _pageSize);

            loadingWindow.Close();
        }
    }
}
