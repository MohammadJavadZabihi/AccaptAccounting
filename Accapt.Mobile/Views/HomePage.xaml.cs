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
            var responseMessage = await client.GetAsync($"https://accaptacounting.ir/api/ServiceListManager/GetAll/Mobile?pageNumber=1&&pageSize=0&&userId=ee28b85504c24ad08df08226645eb710&&providerName={ProviderSesstions.Instance.PtoviderName}");

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
            BorderColor = Colors.White,
            CornerRadius = 10,
            Padding = 10,
            Margin = new Thickness(10, 40, 10, 10),
            BackgroundColor = Colors.Transparent
        };

        var stackLayout = new StackLayout();

        var nameLabel = new Label { Text = visibleService.SrviceName, FontSize = 20, FontFamily = "VazirFont", TextColor=Colors.WhiteSmoke };
        var phoneLabel = new Label { Text = visibleService.PhoneNumber, FontFamily = "VazirFont", TextColor = Colors.WhiteSmoke };
        var addressLabel = new Label { Text = visibleService.Address, FontFamily = "VazirFont", TextColor = Colors.WhiteSmoke };
        var statuce = new Label { Text = visibleService.Statuce, FontFamily = "VazirFont", TextColor = Colors.WhiteSmoke };

        var completeButton = new Button
        {
            Text = "اتمام سرویس",
            BackgroundColor = Color.FromArgb("#00cc99"),
            TextColor = Colors.White,
            FontFamily = "VazirFont" 
        };

        completeButton.Clicked += async (sender, args) =>
        {
            SubmitPage submitPage = new SubmitPage(nameLabel.Text, addressLabel.Text, phoneLabel.Text);
            await Navigation.PushAsync(submitPage);
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
}