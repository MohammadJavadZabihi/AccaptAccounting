using Accapt.Mobile.MauiService;
using Maui.DataGrid;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Timers;
using System.Xml;

namespace Accapt.Mobile.Views;

public partial class SubmitPage : ContentPage
{
    private string _customerName { get; set; }
    private string _address { get; set; }
    private string _phoneNumber { get; set; }

    private ObservableCollection<InvoiceDetailsDTO> invoiceDetailsList = new ObservableCollection<InvoiceDetailsDTO>();

    private List<string> _allProducts = new List<string>();

    private System.Timers.Timer _searchTimer;

    public decimal TotalPrice { get; set; }

    public SubmitPage(string CustomerName, string Address, string PhoneNumber)
	{
		InitializeComponent();
        _customerName = CustomerName;
        _address = Address;
        _phoneNumber = PhoneNumber;
        _searchTimer = new System.Timers.Timer(500);
        _searchTimer.Elapsed += OnSearchTimerElapsed;
        _searchTimer.AutoReset = false;
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        _searchTimer.Stop();
        _searchTimer.Start();
    }

    private async void OnSearchTimerElapsed(object sender, ElapsedEventArgs e)
    {
        var searchText = searchEntry.Text;

        await LoadDataFromApi(searchText);
    }

    private async Task LoadDataFromApi(string searchText)
    {
        try
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ProviderSesstions.Instance.Token);

            var responseMessage = await client.GetAsync(
                $"https://accaptacounting.ir/api/MangeProduct/GetAll?pageNumber=1&pageSize=800&filter={searchEntry.Text}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<ShowProductDTO>(result);

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    productPicker.Items.Clear();

                    if(responseData.Products != null)
                    {
                        foreach (var item in responseData.Products)
                        {
                            productPicker.Items.Add(item.ProductName);
                        }
                    }
                });
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("خطا", ex.Message, "باشه");
        }
    }

    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtServiceAmount.Text) || string.IsNullOrEmpty(txtAyab.Text) || string.IsNullOrEmpty(txtAmountpaid.Text))
        {
            await DisplayAlert("خطا", "لطفا کادرهای مورد نیاز برای اضافه کردن محصول را وارد نمایید", "باشه");
        }
        else
        {
            try
            {
                double persentageForAyab = 0.6;
                double persentageForServiec = 0.5;

                double amount = 0;

                double ayab = 0;
                double serviceAmount = 0;

                serviceAmount = Convert.ToDouble(txtServiceAmount.Text) * persentageForServiec;

                if (Convert.ToDouble(txtAyab.Text) <= 50000)
                {
                    ayab = Convert.ToDouble(txtAyab.Text);
                }
                else
                {
                    ayab = Convert.ToDouble(txtAyab.Text) * persentageForAyab;
                }

                amount = serviceAmount + ayab;


                var data = new
                {
                    Address = _address,
                    UserId = "ee28b85504c24ad08df08226645eb710",
                    ServiceName = _customerName,
                    Date = DateTime.UtcNow.ToShortDateString(),
                    IsDone = "انجام شده",
                    Amount = amount,

                };

                InvoiceDetailsDTO invoiceDetailsDTO = new InvoiceDetailsDTO
                {
                    ProductName = "اجرت سرویس, اجرت نصب",
                    ProductPrice = Convert.ToInt32(txtServiceAmount.Text),
                    ProductCount = 1,
                    Discount = 0,
                    ProductTotalPrice = Convert.ToInt32(txtServiceAmount.Text)
                };

                invoiceDetailsList.Add(invoiceDetailsDTO);

                InvoiceDetailsDTO invoiceDetailsDTO2 = new InvoiceDetailsDTO
                {
                    ProductName = "ایاب ذهاب",
                    ProductPrice = Convert.ToInt32(txtAyab.Text),
                    ProductCount = 1,
                    Discount = 0,
                    ProductTotalPrice = Convert.ToInt32(txtAyab.Text)
                };

                invoiceDetailsList.Add(invoiceDetailsDTO2);

                TotalPrice += Convert.ToDecimal(txtServiceAmount.Text) + Convert.ToDecimal(txtAyab.Text);

                string currentDate = DateConvertor.ConvertToShamsi(DateTime.UtcNow);
                string nextVisit = DateConvertor.ConvertToShamsi(DateTime.UtcNow.AddMonths(6));

                bool std = false;

                if(Convert.ToDecimal(txtAmountpaid.Text) < TotalPrice)
                {
                    std = true;
                }

                var data2 = new
                {
                    InvoiceName = _customerName,
                    CreditorStatuce = std,
                    TypeOfInvoice = "فاکتور فروش",
                    UserId = "ee28b85504c24ad08df08226645eb710",
                    TotalPrice = TotalPrice,
                    AmountPaid = Convert.ToDecimal(txtAmountpaid.Text),
                    TotalDiscount = 0,
                    DateOfSubmitInvoice = Convert.ToDateTime(currentDate),
                    Description = _address,
                    InvoiceDetails = invoiceDetailsList,
                    NextDateVisit = Convert.ToDateTime(nextVisit),
                    PepolName = _customerName,
                };



                var statuce = await IsDon(data, data2);

                if (statuce)
                {
                    DisplayAlert("موفقیت", $"سرویس {_customerName} با موفقیت ثبت شد", "باشه");
                    await Navigation.PopAsync();
                }
                else
                {
                    DisplayAlert("خطا", $"خطا از سمت سرور", "Ok");
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("خطا", ex.Message, "باشه");
            }
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


    private async void btnAddProduct_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtProductCount.Text) || string.IsNullOrEmpty(productPicker.Items[productPicker.SelectedIndex]) || string.IsNullOrEmpty(txtProductPrice.Text))
        {
            await DisplayAlert("خطا","لطفا کادرهای مورد نیاز برای اضافه کردن محصول را وارد نمایید","باشه");
        }
        else
        {
            try
            {
                InvoiceDetailsDTO invoiceDetailsDTO = new InvoiceDetailsDTO
                {
                    ProductName = productPicker.Items[productPicker.SelectedIndex],
                    ProductPrice = Convert.ToInt32(txtProductPrice.Text),
                    ProductCount = Convert.ToInt32(txtProductCount.Text),
                    Discount = 0,
                    ProductTotalPrice = Convert.ToInt32(txtProductPrice.Text) * Convert.ToInt32(txtProductCount.Text)
                };

                TotalPrice += Convert.ToDecimal(txtProductPrice.Text) * Convert.ToDecimal(txtProductCount.Text);

                invoiceDetailsList.Add(invoiceDetailsDTO);

                await DisplayAlert("موفقیت", "محصول با موفقیت به فاکتور اضافه شد", "باشه");

                productPicker.SelectedIndex = -1;
                txtProductCount.Text = "";
                txtProductPrice.Text = "";
            }
            catch (Exception ex)
            {
                await DisplayAlert("خطا", ex.Message, "باشه");
            }
        }
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        try
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ProviderSesstions.Instance.Token);

            var responseMessage = await client.GetAsync($"https://accaptacounting.ir/api/MangeProduct/GetAll?pageNumber=1&pageSize=20&userId=ee28b85504c24ad08df08226645eb710");

            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<ShowProductDTO>(result);

                foreach(var item in responseData.Products)
                {
                    productPicker.Items.Add(item.ProductName);
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("خطا",ex.Message, "ok");
        }
    }
}