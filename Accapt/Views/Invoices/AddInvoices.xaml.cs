using Accapt.Core.Convertor;
using Accapt.Core.DTOs;
using Accapt.Views.Loading;
using Accapt.Views.Pepole;
using Accapt.Views.Products;
using Accapt.WpfServies;
using ApiRequest.Net.CallApi;
using Mohsen.PersianDateControls;
using System.Configuration;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace Accapt.Views.Invoices
{
    public partial class AddInvoices : Window
    {
        private string? url = ConfigurationManager.AppSettings["ApiURL"];
        private readonly LoadGetApisServies _loadGetApisServies;
        private readonly CallApi _callApi;
        private int _pageSize = 10;
        private int _currentPage = 1;
        public string typeOfInvoice = "";
        private int _click = 0;
        public AddInvoices()
        {
            InitializeComponent();
            _loadGetApisServies = new LoadGetApisServies();
            _callApi = new CallApi();
            Loaded += Window_Loaded;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtDate.Text = DateConvertor.ConvertToShamsi(DateTime.UtcNow);

            await _loadGetApisServies.LoadedProductForComboBox(_currentPage, txtProductName, txtProductName.Text, _pageSize);
            await _loadGetApisServies.LoadedCustomerForComboBox(_currentPage, txtCustomerName, txtCustomerName.Text, _pageSize);
            txtTotalAmount.Value = 0;
            txtDiscount.Value = 0;
            txtNextDate.Text = DateConvertor.ConvertToShamsi(DateTime.UtcNow.AddMonths(6));
        }

        private async void addProductToDataGrid_Click(object sender, RoutedEventArgs e)
        {
            if (_click == 0)
            {
                _click++;
                if (string.IsNullOrEmpty(txtProductName.Text) || txtProductPrice.Value == null || txtProductCount.Value == null)
                {
                    _click--;
                    MessageBox.Show("لطفا کادرهای اجباری را کامل نمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {

                    var data = new
                    {
                        ProductName = txtProductName.Text,
                        UserId = UserSession.Instance.UserId
                    };

                    if (typeOfInvoice == "فاکتور فروش")
                    {
                        LoadingWindow loadingWindow = new LoadingWindow();
                        loadingWindow.Show();

                        var responseMessage = await _callApi.SendPostRequest<bool>($"{url}/api/MangeProduct/ExistProduct", data, UserSession.Instance.JwtToken);

                        loadingWindow.Close();

                        if (responseMessage.IsSuccess)
                        {
                            if (responseMessage.Data == false)
                            {
                                if (MessageBox.Show("کالا مورد نظر شما در انبار وجود ندارد, ایا مایل هستید که آن را به انبار خود اضافه کنید؟", "هشدار",
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                {
                                    AddOrEditeProducts addOrEditeProducts = new AddOrEditeProducts();
                                    addOrEditeProducts.txtProductName.Text = txtProductName.Text;
                                    addOrEditeProducts.txtproductPrice.Text = txtProductPrice.Text;
                                    addOrEditeProducts.ShowDialog();

                                    int totalPrice = 0;
                                    InvoiceDetailsDTO detailsDTO = new InvoiceDetailsDTO();
                                    if (txtDiscount.Value != 0)
                                    {
                                        totalPrice = (int)((txtProductPrice.Value * txtProductCount.Value) * txtDiscount.Value) / 100;
                                    }
                                    else
                                    {
                                        totalPrice = (int)(txtProductCount.Value * txtProductPrice.Value);
                                    }

                                    detailsDTO.ProductName = txtProductName.Text;
                                    detailsDTO.ProductPrice = txtProductPrice.Value;
                                    detailsDTO.ProductCount = txtProductCount.Value;
                                    detailsDTO.Discount = txtDiscount.Value;
                                    detailsDTO.ProductTotalPrice = totalPrice;

                                    addProducDataGrid.Items.Add(detailsDTO);

                                    int? totalAmount = txtProductCount.Value * txtProductPrice.Value;

                                    txtTotalAmount.Value += totalAmount;

                                    _click--;
                                }
                                else
                                {
                                    int totalPrice = 0;
                                    InvoiceDetailsDTO detailsDTO = new InvoiceDetailsDTO();
                                    if (txtDiscount.Value != 0)
                                    {
                                        totalPrice = (int)((txtProductPrice.Value * txtProductCount.Value) * txtDiscount.Value) / 100;
                                    }
                                    else
                                    {
                                        totalPrice = (int)(txtProductCount.Value * txtProductPrice.Value);
                                    }

                                    detailsDTO.ProductName = txtProductName.Text;
                                    detailsDTO.ProductPrice = txtProductPrice.Value;
                                    detailsDTO.ProductCount = txtProductCount.Value;
                                    detailsDTO.Discount = txtDiscount.Value;
                                    detailsDTO.ProductTotalPrice = totalPrice;

                                    addProducDataGrid.Items.Add(detailsDTO);

                                    int? totalAmount = txtProductCount.Value * txtProductPrice.Value;

                                    txtTotalAmount.Value += totalAmount;

                                    _click--;
                                }


                            }
                            else
                            {
                                int totalPrice = 0;
                                InvoiceDetailsDTO detailsDTO = new InvoiceDetailsDTO();
                                if (txtDiscount.Value != 0)
                                {
                                    totalPrice = (int)((txtProductPrice.Value * txtProductCount.Value) * txtDiscount.Value) / 100;
                                }
                                else
                                {
                                    totalPrice = (int)(txtProductCount.Value * txtProductPrice.Value);
                                }

                                detailsDTO.ProductName = txtProductName.Text;
                                detailsDTO.ProductPrice = txtProductPrice.Value;
                                detailsDTO.ProductCount = txtProductCount.Value;
                                detailsDTO.Discount = txtDiscount.Value;
                                detailsDTO.ProductTotalPrice = totalPrice;

                                addProducDataGrid.Items.Add(detailsDTO);

                                int? totalAmount = txtProductCount.Value * txtProductPrice.Value;

                                txtTotalAmount.Value += totalAmount;

                                _click--;
                            }
                        }
                        else if (responseMessage.StatusCode == null)
                        {
                            _click--;
                            MessageBox.Show($"لطفا اتصال خود را به اینترنت بررسی کنید و دوباره تلاش کنید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            _click--;
                            MessageBox.Show($"خطای احراز حویت", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
                        {
                            _click--;
                            MessageBox.Show($"لطفا در ارسال اطلاعات دقت فرمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else if (responseMessage.StatusCode == HttpStatusCode.InternalServerError)
                        {
                            _click--;
                            MessageBox.Show($"خطا از طرف سرور, لطفا بعدا دوباره تلاش کنید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        int totalPrice = 0;
                        InvoiceDetailsDTO detailsDTO = new InvoiceDetailsDTO();
                        if (txtDiscount.Value != 0)
                        {
                            totalPrice = (int)((txtProductPrice.Value * txtProductCount.Value) * txtDiscount.Value) / 100;
                        }
                        else
                        {
                            totalPrice = (int)(txtProductCount.Value * txtProductPrice.Value);
                        }

                        detailsDTO.ProductName = txtProductName.Text;
                        detailsDTO.ProductPrice = txtProductPrice.Value;
                        detailsDTO.ProductCount = txtProductCount.Value;
                        detailsDTO.Discount = txtDiscount.Value;
                        detailsDTO.ProductTotalPrice = totalPrice;

                        addProducDataGrid.Items.Add(detailsDTO);

                        int? totalAmount = txtProductCount.Value * txtProductPrice.Value;

                        txtTotalAmount.Value += totalAmount;

                        _click--;
                    }

                    txtProductCount.Value = 0;
                    txtProductName.Text = "";
                    txtDiscount.Value = 0;
                    txtProductPrice.Value = 0;
                }
            }
        }

        private void btnDelet_Click(object sender, RoutedEventArgs e)
        {
            if (_click == 0)
            {
                _click++;
                var selectedP = addProducDataGrid.SelectedItem;
                if (selectedP != null)
                {
                    var data = selectedP as InvoiceDetailsDTO;
                    if (data != null && typeOfInvoice != "فاکتور خرید")
                    {
                        txtTotalAmount.Value -= Convert.ToInt32(data.ProductPrice);
                    }
                    addProducDataGrid.Items.Remove(selectedP);

                    _click--;
                }
            }
        }

        private async void btnSubmitInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_click == 0)
                {
                    if (MessageBox.Show("آیا از ثبت فاکتور خود مطمئن هستید؟", "اطمینان", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        _click++;
                        if (txtTotalAmount.Value == 0 || string.IsNullOrEmpty(txtCustomerName.Text))
                        {
                            _click--;
                            MessageBox.Show("باید محصولی را وارد کنید و باید نوع فاکتور انتخوابی معلوم باشد", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            var dataForIsExistPerson = new
                            {
                                PeronName = txtCustomerName.Text,
                                UserId = UserSession.Instance.UserId
                            };

                            LoadingWindow loadingWindow = new LoadingWindow();
                            loadingWindow.Show();

                            var responseMessage = await _callApi.SendPostRequest<bool>($"{url}/api/PepoleManger/IsExistPerson", dataForIsExistPerson, UserSession.Instance.JwtToken);

                            loadingWindow.Close();

                            if (responseMessage.IsSuccess)
                            {
                                if (responseMessage.Data == false)
                                {
                                    if (MessageBox.Show("شخص مورد نظر شما در قسمت اشخاص وجود ندارد, ایا مایل هستید که او را به قسمت اشخاص خود اضافه کنید؟", "هشدار",
                                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        AddOrEditePepleWindow addOrEditePepleWindow = new AddOrEditePepleWindow();
                                        addOrEditePepleWindow.txtPepoName.Text = txtCustomerName.Text;
                                        addOrEditePepleWindow.ShowDialog();
                                    }
                                }
                            }

                            bool creditorStatuce = false;

                            if (txtAmountPaid.Value == txtTotalAmount.Value)
                                creditorStatuce = false;
                            else
                                creditorStatuce = true;


                            IEnumerable<InvoiceDetailsDTO> invoiceDetailsList = addProducDataGrid.Items.Cast<InvoiceDetailsDTO>();

                            var data = new
                            {
                                InvoiceName = txtCustomerName.Text,
                                CreditorStatuce = creditorStatuce,
                                TypeOfInvoice = typeOfInvoice,
                                UserId = UserSession.Instance.UserId,
                                TotalPrice = Convert.ToDecimal(txtTotalAmount.Value),
                                AmountPaid = Convert.ToDecimal(txtAmountPaid.Value),
                                TotalDiscount = 0,
                                DateOfSubmitInvoice = Convert.ToDateTime(txtDate.Text),
                                Description = txtDescriptions.Text,
                                InvoiceDetails = invoiceDetailsList,
                                NextDateVisit = Convert.ToDateTime(txtNextDate.Text),
                                PepolName = txtCustomerName.Text,
                            };

                            var reponseMessage = await _callApi.SendPostRequest<AddInvoiceReturnApiDTO?>($"{url}/api/InvoiceManger/Add", data, UserSession.Instance.JwtToken);


                            if (reponseMessage.IsSuccess)
                            {
                                _click--;
                                MessageBox.Show("فاکتور شما با موفقیت ثبت شد", "پیام", MessageBoxButton.OK, MessageBoxImage.Information);
                                txtProductCount.Value = 0;
                                txtProductName.Text = "";
                                txtDiscount.Value = 0;
                                txtProductPrice.Value = 0;
                                txtDescriptions.Text = "";
                                txtCustomerName.Text = "";
                                txtTotalAmount.Value = 0;
                                txtAmountPaid.Value = 0;
                                addProducDataGrid.Items.Clear();
                            }
                            else if (responseMessage.StatusCode == null)
                            {
                                _click--;
                                MessageBox.Show($"لطفا اتصال خود را به اینترنت بررسی کنید و دوباره تلاش کنید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                            {
                                _click--;
                                MessageBox.Show($"خطای احراز حویت", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
                            {
                                _click--;
                                MessageBox.Show($"لطفا در ارسال اطلاعات دقت فرمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else if (responseMessage.StatusCode == HttpStatusCode.InternalServerError)
                            {
                                _click--;
                                MessageBox.Show($"خطا از طرف سرور, لطفا بعدا دوباره تلاش کنید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطای ناشناخته : {ex.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void txtCustomerName_KeyUp(object sender, KeyEventArgs e)
        {
            await _loadGetApisServies.LoadedCustomerForComboBox(_currentPage, txtCustomerName, txtCustomerName.Text, _pageSize);
        }

        private async void txtProductName_KeyUp(object sender, KeyEventArgs e)
        {
            await _loadGetApisServies.LoadedProductForComboBox(_currentPage, txtProductName, txtProductName.Text, _pageSize);
        }
    }
}
