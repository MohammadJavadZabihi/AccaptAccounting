using Accapt.Views.Loading;
using Accapt.WpfServies;
using AccaptFullyVersion.App;
using ApiRequest.Net.CallApi;
using System.Configuration;
using System.Net;
using System.Windows;

namespace Accapt.Views.Account
{

    public partial class UserMabgeAccountEditeOrDeletPage : Window
    {
        private CallApi _callApi;
        private string _prop;
        public int Id = 0;
        public string path = "";
        private string? url = ConfigurationManager.AppSettings["ApiURL"];
        private int _click = 0;
        public UserMabgeAccountEditeOrDeletPage()
        {
            InitializeComponent();
            _callApi = new CallApi();
        }

        public void SetProperty(string prop)
        {
            _prop = prop;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txt.Text = _prop;

            switch (Id)
            {
                case 1:
                    path = "UserName";
                    lbl.Text = "نام کاربری";
                    break;

                case 2:
                    path = "Email";
                    lbl.Text = "ایمیل";
                    break;

                case 3:
                    path = "PhoneNumber";
                    lbl.Text = "تلفن همراه";
                    break;

                case 4:
                    path = "RealFullName";
                    lbl.Text = ": نام و نام خوانوادگی";
                    break;

                default:
                    path = "";
                    lbl.Text = "خطا";
                    break;
            }
        }

        private void btnExite_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void App_Exit(object sender, ExitEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
        }

        private async void btnSubmitChanges_Click(object sender, RoutedEventArgs e)
        {
            if (_click == 0)
            {
                _click++;
                if (string.IsNullOrEmpty(txt.Text))
                {
                    MessageBox.Show("لطفا کادر مورد نظر را پرنمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                    _click--;
                }
                else
                {
                    LoadingWindow loading = new LoadingWindow();

                    var data = new[]
                    {
                        new { op = "replace", path = path, value = txt.Text },
                    };

                    try
                    {
                        loading.Show();

                        var responeMessage = await _callApi.SendPatchRequest<DataLayer.Entities.Users>
                            ($"{url}/api/ManageUsers/Update/{UserSession.Instance.Username}", data, UserSession.Instance.JwtToken);

                        _click--;
                        if (responeMessage.IsSuccess)
                        {
                            loading.Close();
                            MessageBox.Show("تغیرات با موفقیت ثبت شد", "تغیرات", MessageBoxButton.OK, MessageBoxImage.Information);
                            MessageBox.Show("اپلیکیشن تا چند ثانیه دیگیر بسته میشود, لطفا دوباره اپلیکیشن را باز کنید", "تغیرات", MessageBoxButton.OK, MessageBoxImage.Information);
                            Application.Current.Shutdown();
                        }
                        else if(responeMessage.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            MessageBox.Show("خطای احراز هویت", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else if(responeMessage.StatusCode == HttpStatusCode.NotFound)
                        {
                            MessageBox.Show("متاستفانه درخواست شما معتبر نیست", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else if(responeMessage.StatusCode == HttpStatusCode.BadRequest)
                        {
                            MessageBox.Show("این نام کاربری در دسترس نیست", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else if(responeMessage.StatusCode == null)
                        {
                            MessageBox.Show("لطفا اتصال به اینترنت خود را بررسی کنید و دوباره تاش کنید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        _click--;
                        loading.Close();
                        MessageBox.Show($"Error Message : {ex.Message}");
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
