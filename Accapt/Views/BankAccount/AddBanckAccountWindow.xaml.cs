using Accapt.Core.DTOs;
using Accapt.Views.Loading;
using Accapt.WpfServies;
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
using System.Windows.Shapes;

namespace Accapt.Views.BankAccount
{
    public partial class AddBanckAccount : Window
    {
        private readonly CallApi _callApi;
        private string? url = ConfigurationManager.AppSettings["ApiURL"];
        private int _click = 0;
        public AddBanckAccount()
        {
            InitializeComponent();
            _callApi = new CallApi();
        }

        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (_click == 0)
            {
                _click++;
                LoadingWindow loadingWindow = new LoadingWindow();
                try
                {
                    loadingWindow.Show();
                    if (txtBankBrach.Text == string.Empty || txtBankName.Text == string.Empty ||
                        txtBankNumber.Text == string.Empty)
                    {
                        _click--;
                        loadingWindow.Close();
                        MessageBox.Show("لطفا فیلدهای اجباری را پر نمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        var data = new
                        {
                            Id = UserSession.Instance.UserId,
                            BankName = txtBankName.Text,
                            BankBranch = txtBankBrach.Text,
                            BankAddress = txtAddress.Text,
                            BankNumber = txtBankNumber.Text,
                            HaseCheck = false,
                        };

                        var responseMessage = await _callApi.SendPostRequest<AddBankDTO?>($"{url}/api/BankManager/Add", data, UserSession.Instance.JwtToken);

                        loadingWindow.Close();
                        _click--;
                        if (responseMessage.IsSuccess)
                        {
                            MessageBox.Show("حساب بانکی با موفقیت اضافه شد", "موفق", MessageBoxButton.OK, MessageBoxImage.Information);
                            txtBankBrach.Text = "";
                            txtAddress.Text = "";
                            txtBankName.Text = "";
                            txtBankNumber.Text = "";
                        }
                        else if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            MessageBox.Show("خطای احراز هویت", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
                        {
                            MessageBox.Show("خطا در اضافه کردن حساب بانکی, لطفا تمامی فیلدهای ضروری را بررسی کنید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else if (responseMessage.StatusCode == null)
                        {
                            MessageBox.Show("لطفا اتصال به اینترنت خود را بررسی کنید و دوباره تاش کنید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    loadingWindow.Close();
                    _click--;
                    MessageBox.Show($"Error is : {ex.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
