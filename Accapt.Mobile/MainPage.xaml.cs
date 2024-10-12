using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
            // اطلاعاتی که می‌خواهید به API ارسال کنید
            var data = new
            {
                Username = txtUserName.Text,
                Password = txtPassword.Text
            };

            // تبدیل اطلاعات به فرمت JSON
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                // درخواست POST به API
                var response = await _httpClient.PostAsync("https://localhost:7146/api/ProviderMnager/Login", content);

                // بررسی موفقیت‌آمیز بودن درخواست
                if (response.IsSuccessStatusCode)
                {
                    // دریافت نتیجه در صورت موفقیت
                    var result = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Success", "عالی بود!", "OK");
                }
                else
                {
                    await DisplayAlert("Failed", "ریدی", "OK");
                }
            }
            catch (Exception ex)
            {
                // مدیریت خطا
                await DisplayAlert("Error", $"خطا: {ex.Message}", "OK");
            }
        }
    }
}
