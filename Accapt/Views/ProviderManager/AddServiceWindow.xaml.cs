using Accapt.Views.Loading;
using Accapt.WpfServies;
using ApiRequest.Net.CallApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
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

namespace Accapt.Views.ProviderManager
{
    public partial class AddServiceWindow : Window
    {
        private CallApi _callAPi;
        private string _url = ConfigurationManager.AppSettings["LocalHost"];
        public AddServiceWindow()
        {
            InitializeComponent();
            _callAPi = new CallApi();
        }

        private void txtProviderName_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var data = new
            {
                UserId = UserSession.Instance.UserId,
                ProviderName = txtProviderName.Text,
                PhoneNumber = txtPhoneNumber.Text,
                Address = txtAddress.Text,
                TotalAmount = 0,
                CustomerName = txtCustomerName.Text,
                IsDone = txtStatuce.Text,
                DateOfServiceForShow = txtDate.Text,
                Descriptions = txtDescriptions.Text
            };

            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();

            var response = await _callAPi.SendPostRequest<bool>($"{_url}/api/ServiceListManager/AddService", data, UserSession.Instance.JwtToken);

            loadingWindow.Close();

            if (response.IsSuccess)
            {
                MessageBox.Show("عملیات با موفقیت انجام شد", "موفق", MessageBoxButton.OK, MessageBoxImage.Information);
                txtProviderName.Text = "";
                txtPhoneNumber.Text = "";
                txtAddress.Text = "";
                txtCustomerName.Text = "";
                txtStatuce.Text = "";
                txtDate.Text = "";
                txtDescriptions.Text = "";
            }
            else if(response.StatusCode == null)
            {
                MessageBox.Show("لطفا از اتصال خود به اینترنت اطمینان حاصل فرمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(response.StatusCode == HttpStatusCode.BadRequest)
            {
                MessageBox.Show("لطفا از صحت اطلاعات ارسال شده اطمینان حاصل فرمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(response.StatusCode == HttpStatusCode.NotFound)
            {
                MessageBox.Show("کاربری پیدا نشد", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(response.StatusCode == HttpStatusCode.Unauthorized)
            {
                MessageBox.Show("خطای احراز حویت", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show($"خطای ناشناخته : {response.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
