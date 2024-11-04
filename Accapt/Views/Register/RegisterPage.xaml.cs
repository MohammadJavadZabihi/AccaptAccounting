using Accapt.Views.Loading;
using ApiRequest.Net.CallApi;
using System.Configuration;
using System.Net;
using System.Windows;

namespace AccaptFullyVersion.App.Views
{
    public partial class RegisterPage : Window
    {
        private readonly MainWindow _mainWindow;
        private readonly CallApi _callApiServies;
        private string? url = ConfigurationManager.AppSettings["LocalHost"];
        private int _click = 0;
        public RegisterPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _callApiServies = new CallApi();
        }

        private void btnSingin_Click(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage(_mainWindow);
            loginPage.Visibility = Visibility.Visible;
            this.Close();
        }

        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (_click == 0)
            {
                _click++;
                LoadingWindow loading = new LoadingWindow();

                if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtFamily.Text) ||
                    string.IsNullOrWhiteSpace(txtUserName.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Password)
                    || string.IsNullOrWhiteSpace(txtRePassword.Password) || string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
                {
                    MessageBox.Show("لطفا فیلدهای اجباری را پر نمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                    _click--;
                }
                else
                {
                    _click++;
                    try
                    {
                        loading.Show();

                        var data = new
                        {
                            Name = txtName.Text,
                            Family = txtFamily.Text,
                            UserName = txtUserName.Text,
                            Email = txtEmail.Text,
                            Password = txtPassword.Password,
                            RePassword = txtRePassword.Password,
                            PhoneNumber = txtPhoneNumber.Text
                        };

                        var responesMessage = await _callApiServies.SendPostRequest<object>($"{url}/api/ManageUsers/Register", data);

                        if (responesMessage.IsSuccess)
                        {
                            loading.Close();
                            MessageBox.Show("ثبت نام با موفقیت انجام شد، لطفا برای فعال کردن حساب کاربری خود بر روی لینکی که برای شما ایمیل شده است کلیک فرمایید", "موفقیت", MessageBoxButton.OK, MessageBoxImage.Information);
                            LoginPage loginPage = new LoginPage(_mainWindow);
                            _click--;
                            loginPage.Visibility = Visibility.Visible;
                            this.Close();
                        }
                        else
                        {
                            _click--;
                            loading.Close();

                            if (responesMessage.StatusCode == null)
                            {
                                MessageBox.Show("لطفا از وصل بودن اینترنت خود اطمینان حاصل فرمایید و دوباره برای ثبت نام تلاش کنید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else if (responesMessage.StatusCode == HttpStatusCode.BadRequest)
                            {
                                MessageBox.Show("این نام کاربری و یا ایمیل و یا شماره تلفن قبلا ثبت نام شده است", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else if (responesMessage.StatusCode == HttpStatusCode.InternalServerError)
                            {
                                MessageBox.Show("درحال حاضر سرور دچار مشکلاتی است. لطفا چند دقیقه دیگر تلاش کنید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _click--;
                        loading.Close();
                        MessageBox.Show(ex.Message, "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        _click--;
                        loading.Close();
                    }
                }
            }
        }
    }
}
