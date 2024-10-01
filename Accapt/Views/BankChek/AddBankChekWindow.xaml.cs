using Accapt.Core.DTOs;
using Accapt.Views.Loading;
using Accapt.WpfServies;
using ApiRequest.Net.CallApi;
using System.Configuration;
using System.Net;
using System.Windows;

namespace Accapt.Views.BankChek
{
    public partial class AddBankChekWindow : Window
    {
        private CallApi _callApi;
        private string? _url = ConfigurationManager.AppSettings["ApiURL"];
        private int _click = 0;
        public AddBankChekWindow()
        {
            InitializeComponent();
            _callApi = new CallApi();
        }

        private async void btnAddChek_Click(object sender, RoutedEventArgs e)
        {
            if (_click == 0)
            {
                LoadingWindow loadingWindow = new LoadingWindow();
                loadingWindow.Show();
                _click++;
                try
                {
                    if (string.IsNullOrEmpty(txtChekNumber.Text) || string.IsNullOrEmpty(txtChekBank.Text) ||
                    string.IsNullOrEmpty(txtEndDate.Text) || string.IsNullOrEmpty(txtPerson.Text) || string.IsNullOrEmpty(txtPrice.Text) ||
                    string.IsNullOrEmpty(txtSubmitDate.Text) || string.IsNullOrEmpty(txtEndDate.Text))
                    {
                        loadingWindow.Close();
                        MessageBox.Show("لطفا فیلدهای الزامی را پرنمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        _click--;
                    }
                    else
                    {
                        var data = new
                        {
                            UserId = UserSession.Instance.UserId,
                            ChekNumber = txtChekNumber.Text,
                            ChekBankName = txtChekBank.Text,
                            ChekPrice = Convert.ToDecimal(txtPrice.Text),
                            CurrentDate = Convert.ToDateTime(txtSubmitDate.Text),
                            DueDate = Convert.ToDateTime(txtEndDate.Text),
                            Person = txtPerson.Text,
                        };

                        var responseMessage = await _callApi.SendPostRequest<SingleChekDTO?>($"{_url}/api/ChekManger/Add", data, UserSession.Instance.JwtToken);
                        loadingWindow.Close();
                        if (responseMessage.IsSuccess)
                        {
                            _click--;
                            MessageBox.Show("چک مورد نظر با موفقیت ثبت شد", "موفق", MessageBoxButton.OK, MessageBoxImage.Information);
                            txtChekNumber.Text = "";
                            txtChekBank.Text = "";
                            txtEndDate.Text = "";
                            txtPerson.Text = "";
                            txtPrice.Text = "";
                            txtSubmitDate.Text = "";
                            txtEndDate.Text = "";
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
                catch (Exception ex)
                {
                    _click--;
                    loadingWindow.Close();
                    MessageBox.Show($"{ex.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
