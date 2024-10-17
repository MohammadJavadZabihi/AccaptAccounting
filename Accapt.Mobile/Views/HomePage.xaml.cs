using Accapt.Mobile.MauiService;
using Newtonsoft.Json;
using System.Net.Http.Json;

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
            var responseMessage = await client.GetAsync($"https://accaptacounting.ir/api/ServiceListManager/GetAll/Mobile?pageNumber=1&&pageSize=80&&filtser={txtSearch.Text}&&userId=ee28b85504c24ad08df08226645eb710&&providerName={ProviderSesstions.Instance.PtoviderName}");

            if(responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<VisbleServiceListShow?>(result);

                foreach (var customer in data.VisibleService)
                {
                    var card = CreateCustomerCard(customer);
                    CardContainer.Children.Add(card); // ????? ???? ???? ?? StackLayout
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Failed to load data: " + ex.Message, "OK");
        }
    }

    private View CreateCustomerCard(VisibleService visibleService)
    {
        // ???? ???? ?? ???? ?? Frame
        var frame = new Frame
        {
            BorderColor = Colors.Gray,
            CornerRadius = 10,
            Padding = 10,
            Margin = new Thickness(0, 10)
        };

        // ?????? ???? (??????? ?????)
        var stackLayout = new StackLayout();

        var nameLabel = new Label { Text = visibleService.SrviceName, FontSize = 20 };
        var phoneLabel = new Label { Text = visibleService.PhoneNumber };
        var addressLabel = new Label { Text = visibleService.Address };

        var completeButton = new Button
        {
            Text = "????? ???",
            BackgroundColor = Colors.Green,
            TextColor = Colors.White
        };

        // ????? ???? ?????? ???? ???? ????
        completeButton.Clicked += (sender, args) =>
        {
            // ?????? ????? ??? ???? ?????
            DisplayAlert("Complete", $"Customer {visibleService.SrviceName} marked as complete.", "OK");
        };

        // ????? ???? ???????? ?? StackLayout
        stackLayout.Children.Add(nameLabel);
        stackLayout.Children.Add(phoneLabel);
        stackLayout.Children.Add(addressLabel);
        stackLayout.Children.Add(completeButton);

        // ????? ?????? ???? ?? Frame
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