using Accapt.WpfServies;
using ApiRequest.Net.CallApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
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

namespace Accapt.Views.ProviderManager
{
    /// <summary>
    /// Interaction logic for AddProviderManagerWindow.xaml
    /// </summary>
    public partial class AddProviderManagerWindow : Window
    {
        private CallApi _callApi;
        private string _url = ConfigurationManager.AppSettings["ApiURL"];
        public AddProviderManagerWindow()
        {
            InitializeComponent();
            _callApi = new CallApi();
        }

        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(txtProviderName.Text) || !string.IsNullOrEmpty(txtProviderName.Text))
            {
                var data = new
                {
                    ProviderName = txtProviderName.Text,
                    ProviderPassword = txtPassword.Text,
                    UserId = UserSession.Instance.UserId
                };

                var responseMessage = await _callApi.SendPostRequest<bool>($"{_url}/api/ProviderMnager/Register", data, UserSession.Instance.JwtToken);

                if (responseMessage.StatusCode == HttpStatusCode.OK)
                {
                    MessageBox.Show("سرویس کار با موفقیت ثبت شد", "موفقیت", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else if(responseMessage.StatusCode == null)
                {
                    MessageBox.Show("لطفا از ارتباط خود با اینترنت اطمینان فرمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if(responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show("خطای سطح دسترسی", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if(responseMessage.StatusCode == HttpStatusCode.BadRequest)
                {
                    MessageBox.Show("لطفا در ارسال اطلاعات دقت فرمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if(responseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    MessageBox.Show("خطای سطح دسترسی", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show($"خطای نا شناخته : {responseMessage.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
