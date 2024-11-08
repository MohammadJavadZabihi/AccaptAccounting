
using Accapt.Core.DTOs;
using Accapt.Views.Loading;
using Accapt.WpfServies;
using ApiRequest.Net.CallApi;
using System.Configuration;
using System.Windows;
using System.Net;

namespace AccaptFullyVersion.App.Views
{
    public partial class LoginPage : Window
    {
        private readonly MainWindow _mainWindow;
        private readonly CallApi _callApiServies;
        private string? url = ConfigurationManager.AppSettings["ApiURL"];
        private string? _localUrl = ConfigurationManager.AppSettings["LocalHost"];
        public string userName;
        private int _click = 0;
        public LoginPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _callApiServies = new CallApi();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (_click == 0)
            {
                _click++;
                LoadingWindow loading = new LoadingWindow();

                if (string.IsNullOrEmpty(txtPassword.Password) || string.IsNullOrEmpty(txtUserName.Text))
                {
                    MessageBox.Show("لطفا فیلدهای اجباری را پرنمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
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
                            UserName = txtUserName.Text,
                            Password = txtPassword.Password
                        };

                        var responesMessage = await _callApiServies.SendPostRequest<LoginResponseDTO>($"{url}/api/ManageUsers/Login", data);

                        if (responesMessage.IsSuccess)
                        {
                            var token = responesMessage.Data.Token;

                            var userName = JwtHelper.GetUsernameFromToken(token);
                            var userId = JwtHelper.GetUserIdFromToken(token);

                            UserSession.Instance.JwtToken = token;
                            UserSession.Instance.Username = userName;
                            UserSession.Instance.UserId = userId;

                            loading.Close();

                            MessageBox.Show($"خوش آمدید {txtUserName.Text}", "خوش آمد گویی", MessageBoxButton.OK, MessageBoxImage.Information);
                            _mainWindow.Visibility = Visibility.Visible;
                            _mainWindow.formId = 0;
                            _mainWindow.IsLogin = true;
                            _click--;
                            this.Close();
                            return;
                        }
                        else
                        {
                            _click--;
                            loading.Close();

                            if (responesMessage.StatusCode == null)
                            {
                                MessageBox.Show("لطفا از وصل بودن اینترنت خود اطمینان حاصل فرمایید و دوباره برای ورود تلاش کنید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else if (responesMessage.StatusCode == HttpStatusCode.BadRequest)
                            {
                                MessageBox.Show("نام کاربری و یا رمز عبور اشتباه است", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else if (responesMessage.StatusCode == HttpStatusCode.NotFound)
                            {
                                MessageBox.Show("کاربری با این مشخصات یافت نشد", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else if (responesMessage.StatusCode == HttpStatusCode.InternalServerError)
                            {
                                MessageBox.Show("درحال حاضر سرور دچار مشکلاتی است. لطفا چند دقیقه دیگر تلاش کنید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else if(responesMessage.StatusCode == HttpStatusCode.Unauthorized)
                            {
                                MessageBox.Show("حساب کاربری شما فعال نیست، لطفا ار طریق لینکی که به ایمیل شما فرستاده شده است حساب خود را فعال کنید و بعد دوباره امتحان کنید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        _click--;
                    }
                    finally
                    {
                        _click--;
                        loading.Close();
                    }
                }
            }
        }

        private void btnSingUp_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            RegisterPage registerPage = new RegisterPage(_mainWindow);
            registerPage.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
