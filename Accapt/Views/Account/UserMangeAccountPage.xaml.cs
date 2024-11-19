using Accapt.Core.DTOs;
using Accapt.Views.Loading;
using Accapt.WpfServies;
using AccaptFullyVersion.App;
using ApiRequest.Net.CallApi;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace Accapt.Views.Account
{
    public partial class UserMangeAccount : Page
    {
        private readonly CallApi _callApi;
        private string? url = ConfigurationManager.AppSettings["ApiURL"];

        public GetSingleUserDTO data;
        public UserMangeAccount()
        {
            InitializeComponent();
            _callApi = new CallApi();
        }

        private async void frmUserManegAcc_Loaded(object sender, RoutedEventArgs e)
        {
            LoadingWindow loading = new LoadingWindow();

            try
            {
                loading.Show();

                var data2 = new
                {
                    userName = UserSession.Instance.Username
                };
                var user = await _callApi.SendGetRequest<GetSingleUserDTO>($"{url}/api/ManageUsers/GetSingle", jwt:UserSession.Instance.JwtToken);
                loading.Close();

                if (user.IsSuccess)
                {
                    data = user.Data;
                    lblWelcomUser.Text = $"خوش آمدید {data.UserName}";
                    lblEmail.Text = data.Email;
                    lblFullName.Text = data.RealFullName;
                    lblUserName.Text = data.UserName;
                    lblPhoneNumber.Text = data.PhoneNumber;
                }
                else
                {
                    MessageBox.Show("خطا در نمایش اطلاعات کاربر", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                    lblWelcomUser.Text = "خطا در نمایش اطلاعات کاربر";
                    lblEmail.Text = "خطا در نمایش اطلاعات کاربر";
                    lblFullName.Text = "خطا در نمایش اطلاعات کاربر";
                    lblUserName.Text = "خطا در نمایش اطلاعات کاربر";
                    lblPhoneNumber.Text = "خطا در نمایش اطلاعات کاربر";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                loading.Close();
            }
        }

        private void btnEditeUserName_Click(object sender, RoutedEventArgs e)
        {
            UserMabgeAccountEditeOrDeletPage page = new UserMabgeAccountEditeOrDeletPage();
            page.SetProperty(UserSession.Instance.Username);
            page.Id = 1;
            page.ShowDialog();
        }

        private void btnEditeEmail_Click(object sender, RoutedEventArgs e)
        {
            UserMabgeAccountEditeOrDeletPage page = new UserMabgeAccountEditeOrDeletPage();
            page.SetProperty(data.Email);
            page.Id = 2;
            page.ShowDialog();
        }

        private void btnEditePhoneNumber_Click(object sender, RoutedEventArgs e)
        {
            UserMabgeAccountEditeOrDeletPage page = new UserMabgeAccountEditeOrDeletPage();
            page.SetProperty(data.PhoneNumber);
            page.Id = 3;
            page.ShowDialog();
        }

        private void btnEditeFullName_Click(object sender, RoutedEventArgs e)
        {
            UserMabgeAccountEditeOrDeletPage page = new UserMabgeAccountEditeOrDeletPage();
            page.SetProperty(data.RealFullName);
            page.Id = 4;
            page.ShowDialog();
        }
    }
}
