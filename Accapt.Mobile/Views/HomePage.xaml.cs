using Accapt.Mobile.MauiService;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace Accapt.Mobile.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
        LoadDataFromApi();

    }

    private async void LoadDataFromApi()
    {
        try
        {
            HttpClient client = new HttpClient();
            var responseMessage = await client.GetAsync($"https://accaptacounting.ir/api/ServiceListManager/GetAll/Mobile?pageNumber=1&&pageSize=0&&filtser={txtSearch.Text}&&userId=ee28b85504c24ad08df08226645eb710&&providerName={ProviderSesstions.Instance.PtoviderName}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<VisbleServiceListShow?>(result);

                CardContainer.Children.Clear();

                foreach (var customer in data.VisibleService)
                {
                    var card = await CreateCustomerCard(customer);
                    CardContainer.Children.Add(card);
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Failed to load data: " + ex.Message, "OK");
        }
    }

    private async Task<View> CreateCustomerCard(VisibleService visibleService)
    {
        var frame = new Frame
        {
            BorderColor = Colors.Gray,
            CornerRadius = 10,
            Padding = 10,
            Margin = new Thickness(0, 10)
        };

        var stackLayout = new StackLayout();

        var nameLabel = new Label { Text = visibleService.SrviceName, FontSize = 20, FontFamily = "VazirFont" };
        var phoneLabel = new Label { Text = visibleService.PhoneNumber, FontFamily = "VazirFont" };
        var addressLabel = new Label { Text = visibleService.Address, FontFamily = "VazirFont" };
        var statuce = new Label { Text = visibleService.Statuce, FontFamily = "VazirFont" };

        var completeButton = new Button
        {
            Text = "Done",
            BackgroundColor = Colors.Green,
            TextColor = Colors.White,
            FontFamily = "VazirFont" 
        };

        completeButton.Clicked += async (sender, args) =>
        {
            var data = new
            {
                Address = addressLabel.Text,
                UserId = "ee28b85504c24ad08df08226645eb710",
                ServiceName = nameLabel.Text,
                Date = DateTime.UtcNow.ToShortDateString(),
                IsDone = "انجام شده"
            };

            var statuce = await IsDon(data);

            if(statuce)
            {
                DisplayAlert("Successfuly", $"Service {visibleService.SrviceName} Done Successfully", "Ok");
                HttpClient client = new HttpClient();
                var responseMessage = await client.GetAsync($"https://accaptacounting.ir/api/ServiceListManager/GetAll/Mobile?pageNumber=1&&pageSize=0&&filtser={txtSearch.Text}&&userId=ee28b85504c24ad08df08226645eb710&&providerName={ProviderSesstions.Instance.PtoviderName}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    var data2 = JsonConvert.DeserializeObject<VisbleServiceListShow?>(result);

                    CardContainer.Children.Clear();

                    foreach (var customer in data2.VisibleService)
                    {
                        var card = await CreateCustomerCard(customer);
                        CardContainer.Children.Add(card);
                    }
                }

            }
            else
            {
                DisplayAlert("Error", $"Error", "Ok");
            }
        };

        stackLayout.Children.Add(nameLabel);
        stackLayout.Children.Add(phoneLabel);
        stackLayout.Children.Add(addressLabel);
        stackLayout.Children.Add(statuce);
        stackLayout.Children.Add(completeButton);

        frame.Content = stackLayout;

        return frame;
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        LoadDataFromApi();
    }

    private void btnLoad_Clicked(object sender, EventArgs e)
    {
        LoadDataFromApi();
    }

    private async Task<bool> IsDon(object data)
    {
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpClient client = new HttpClient();
        var responseMessage = await client.PutAsync($"https://accaptacounting.ir/api/ServiceListManager/Update" , content);

        if (responseMessage.IsSuccessStatusCode)
        {
            var result = await responseMessage.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<bool>(result);

            return responseData;
        }
        else
            return false;
    }
}