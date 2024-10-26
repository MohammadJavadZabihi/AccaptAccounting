using Accapt.Mobile.MauiService;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml;

namespace Accapt.Mobile.Views;

public partial class SubmitPage : ContentPage
{
    private string _customerName { get; set; }
    private string _address { get; set; }
    private string _phoneNumber { get; set; }

    private ObservableCollection<InvoiceDetailsDTO> invoiceDetailsList = new ObservableCollection<InvoiceDetailsDTO>();

    public double TotalPrice { get; set; }

    public SubmitPage(string CustomerName, string Address, string PhoneNumber)
	{
		InitializeComponent();
        _customerName = CustomerName;
        _address = Address;
        _phoneNumber = PhoneNumber;
    }

    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
        var data = new
        {
            Address = _address,
            UserId = "ee28b85504c24ad08df08226645eb710",
            ServiceName = _customerName,
            Date = DateTime.UtcNow.ToShortDateString(),
            IsDone = "انجام شده",
            Amount = Convert.ToDouble(txtServiceAmount.Text) + Convert.ToDouble(txtAyab.Text),

        };

        TotalPrice += Convert.ToDouble(txtServiceAmount.Text) + Convert.ToDouble(txtAyab.Text);

        var data2 = new
        {
            InvoiceName = _customerName,
            CreditorStatuce = true,
            TypeOfInvoice = "فاکتور فروش",
            UserId = "ee28b85504c24ad08df08226645eb710",
            TotalPrice = TotalPrice,
            AmountPaid = 0,
            TotalDiscount = 0,
            DateOfSubmitInvoice = DateTime.UtcNow.ToShortDateString(),
            Description = "ندارد",
            InvoiceDetails = invoiceDetailsList,
            NextDateVisit = DateTime.UtcNow.AddMonths(6).ToShortDateString(),
            PepolName = _customerName,
        };

        var statuce = await IsDon(data, data2);

        if (statuce)
        {
            DisplayAlert("Successfuly", $"Service {_customerName} Done Successfully", "Ok");
            await Navigation.PopAsync();
        }
        else
        {
            DisplayAlert("Error", $"Error", "Ok");  
        }
    }

    private async Task<bool> IsDon(object data, object data2)
    {
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpClient client = new HttpClient();
        var responseMessage = await client.PutAsync($"https://accaptacounting.ir/api/ServiceListManager/Update", content);

        var json2 = JsonConvert.SerializeObject(data2);
        var content2 = new StringContent(json2, Encoding.UTF8, "application/json");

        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ProviderSesstions.Instance.Token);

        var responseMessage2 = await client.PostAsync("https://accaptacounting.ir/api/InvoiceManger/Add", content2);

        if (responseMessage.IsSuccessStatusCode && responseMessage2.IsSuccessStatusCode)
        {
            var result = await responseMessage.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<bool>(result);

            return responseData;
        }
        else
        {
            return false;
        }
    }


    private void btnAddProduct_Clicked(object sender, EventArgs e)
    {
        // ایجاد یک محصول جدید و تنظیم مقادیر آن
        InvoiceDetailsDTO invoiceDetailsDTO = new InvoiceDetailsDTO
        {
            ProductName = txtProductName.Text,
            ProductPrice = Convert.ToInt32(txtProductPrice.Text),
            ProductCount = Convert.ToInt32(txtProductCount.Text),
            Discount = 0,
            ProductTotalPrice = Convert.ToInt32(txtProductPrice.Text) * Convert.ToInt32(txtProductCount.Text)
        };

        TotalPrice += Convert.ToDouble(txtProductPrice.Text) * Convert.ToDouble(txtProductCount.Text);

        // افزودن محصول جدید به ObservableCollection
        invoiceDetailsList.Add(invoiceDetailsDTO);
    }
}