using Accapt.DataLayer.Entities;
using Accapt.Views.BankChek;
using Accapt.Views.Loading;
using Accapt.WpfServies;
using AccaptFullyVersion.App;
using ApiRequest.Net.CallApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
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

namespace Accapt.Views.BankAccount
{
    public partial class BanckAccountShowPage : Page
    {
        private readonly CallApi _callApi;
        private readonly LoadGetApisServies _loadGetApisServies;
        private int _currentPage = 1;
        private string? url = ConfigurationManager.AppSettings["ApiURL"];
        private int _pageSize = 10;
        private int _click = 0;
        public BanckAccountShowPage()
        {
            InitializeComponent();
            _callApi = new CallApi();
            _loadGetApisServies = new LoadGetApisServies();
            Loaded += Page_Loaded;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await _loadGetApisServies.LoadedBankAccounts(_currentPage, bankAccountDataGrid, txtSearch.Text, _pageSize);
        }

        private async void RefreshPage_Click(object sender, RoutedEventArgs e)
        {
            if (_click == 0)
            {
                _click++;
                await _loadGetApisServies.LoadedBankAccounts(_currentPage, bankAccountDataGrid, txtSearch.Text, _pageSize);
                _click--;
            }
        }

        private async void btnDelet_Click(object sender, RoutedEventArgs e)
        {
            if (_click == 0)
            {
                _click++;
                try
                {
                    if (MessageBox.Show("آیا از حذف ایتم انتخوابی مطمئن هستید؟", "سوال", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        LoadingWindow loadingWindow = new LoadingWindow();
                        loadingWindow.Show();
                        if (bankAccountDataGrid.SelectedItem is BankT bankAccount)
                        {
                            int bankId = bankAccount.BankId;

                            if (bankId != 0)
                            {
                                var responseMessage = await _callApi.SendDeletRequest<bool>($"{url}/api/BankManager/Delet?bankId={bankId}&userId={UserSession.Instance.UserId}", jwt: UserSession.Instance.JwtToken);

                                _click--;
                                if (responseMessage.IsSuccess)
                                {
                                    loadingWindow.Close();
                                    MessageBox.Show("ایتم انتخوابی با موفقیت حذف شد", "موفقیت", MessageBoxButton.OK, MessageBoxImage.Information);
                                    await _loadGetApisServies.LoadedBankAccounts(_currentPage, bankAccountDataGrid, txtSearch.Text, _pageSize);
                                }
                                else if (responseMessage.StatusCode == HttpStatusCode.Unauthorized || responseMessage.StatusCode == HttpStatusCode.BadRequest)
                                {
                                    MessageBox.Show("خطای احراز هویت", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                                else if (responseMessage.StatusCode == HttpStatusCode.NotFound)
                                {
                                    MessageBox.Show("حساب بانکی مورد نظر وجود ندارد", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                                else if (responseMessage.StatusCode == null)
                                {
                                    MessageBox.Show("لطفا اتصال به اینترنت خود را بررسی کنید و دوباره تاش کنید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                            else
                            {
                                _click--;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"خطا : {ex.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void btnBefor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddInoice_Click(object sender, RoutedEventArgs e)
        {
            ChekBankShowWindow chekbankShowPage = new ChekBankShowWindow();
            chekbankShowPage.ShowDialog();
        }

        private async void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            await _loadGetApisServies.LoadedBankAccountsSearch(_currentPage, bankAccountDataGrid, txtSearch.Text, _pageSize);
        }

        private void btnAddChek_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddInoice_Click_1(object sender, RoutedEventArgs e)
        {
            AddBanckAccount addBanckAccount = new AddBanckAccount();
            addBanckAccount.ShowDialog();
        }
    }
}
