using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Accapt.Mobile.Views;

namespace Accapt.Mobile
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient _httpClient;
        public MainPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                await DisplayAlert("خطا", "لطفا نام کاربری و کلمه عبور خود را وارد نمایید", "باشه");
            }
            else
            {
                var data = new
                {
                    Username = txtUserName.Text,
                    Password = txtPassword.Text,
                    UserId = "ee28b85504c24ad08df08226645eb710"
                };

                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    var response = await _httpClient.PostAsync("https://localhost:7146/api/ProviderMnager/Login", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        await DisplayAlert("موفق", "خوش آمدید", "باشه");
                    }
                    else if(response.IsSuccessStatusCode == null)
                    {
                        await DisplayAlert("خطا", "از اتصال خود به اینترنت مطمئن شوید", "باشه");
                        App.Current.MainPage = new NavigationPage(new HomePage());
                    }
                    else
                    {
                        await DisplayAlert("خطا", "لطفا در ارسال اطلاعات دقت فرمایید", "باشه");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"خطا: {ex.Message}", "OK");
                }
            }
        }
    }
}
