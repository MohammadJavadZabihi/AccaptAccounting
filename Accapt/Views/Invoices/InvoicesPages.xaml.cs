using Accapt.Core.Convertor;
using Accapt.Core.DTOs;
using Accapt.DataLayer.Entities;
using Accapt.Views.Invoices;
using Accapt.Views.Loading;
using Accapt.Views.PrintInvoice;
using Accapt.WpfServies;
using ApiRequest.Net.CallApi;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace Accapt.Invoices
{
    public partial class Invoices : Page
    {
        private readonly CallApi _callApi;
        private int _pageSize = 10;
        private int _currentPage = 1;
        private string? url = ConfigurationManager.AppSettings["ApiURL"];
        private readonly LoadGetApisServies _loadGetApisServies;
        public Invoices()
        {
            InitializeComponent();
            _callApi = new CallApi();
            _loadGetApisServies = new LoadGetApisServies();
            Loaded += Page_Loaded;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();
            await _loadGetApisServies.LoadedInvoices(_currentPage, invoicesDataGrid, txtSearch.Text, _pageSize);
            loadingWindow.Close();
        }

        private async void btnNext_Click(object sender, RoutedEventArgs e)
        {
            _currentPage++;
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();
            var chekStatuce = await _loadGetApisServies.LoadedInvoices(_currentPage, invoicesDataGrid, txtSearch.Text, _pageSize);
            loadingWindow.Close();
            if (!chekStatuce)
            {
                _currentPage--;
                await _loadGetApisServies.LoadedInvoices(_currentPage, invoicesDataGrid, txtSearch.Text, _pageSize);
            }
        }

        private async void btnBefor_Click(object sender, RoutedEventArgs e)
        {
            _currentPage--;
            if (_currentPage > 0)
            {
                LoadingWindow loadingWindow = new LoadingWindow();
                loadingWindow.Show();
                await _loadGetApisServies.LoadedInvoices(_currentPage, invoicesDataGrid, txtSearch.Text, _pageSize);
                loadingWindow.Close();
            }
            else
            {
                _currentPage++;
            }
        }

        private async void RefreshPage_Click(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();
            await _loadGetApisServies.LoadedInvoices(_currentPage, invoicesDataGrid, txtSearch.Text, _pageSize);
            loadingWindow.Close();
        }

        private async void btnDelet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (invoicesDataGrid.SelectedItem is Invoice invoice)
                {
                    int invoiceId = invoice.InvoiceId;

                    if (invoiceId >= 0)
                    {
                        if (MessageBox.Show("آیا از حذف ایتم انتخوابی مطمعئن هستید؟", "اطمینان", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            var responseMessage = await _callApi.SendDeletRequest<bool>($"{url}/api/InvoiceManger/Delet?invoiceId={invoiceId}&userId={UserSession.Instance.UserId}", jwt: UserSession.Instance.JwtToken);

                            if (responseMessage.IsSuccess)
                            {
                                MessageBox.Show("محصول انتخوابی با موفقیت حذف شد", "موفقیت", MessageBoxButton.OK, MessageBoxImage.Information);
                                LoadingWindow loadingWindow = new LoadingWindow();
                                loadingWindow.Show();
                                await _loadGetApisServies.LoadedInvoices(_currentPage, invoicesDataGrid, txtSearch.Text, _pageSize);
                                loadingWindow.Close();
                            }
                            else
                            {
                                MessageBox.Show($"Error Message : {responseMessage.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(nameof(ex.Message));
            }
        }

        private void btnAddInoice_Click(object sender, RoutedEventArgs e)
        {
            ChoseTypeOfInvoice choseTypeOfInvoice = new ChoseTypeOfInvoice();
            choseTypeOfInvoice.ShowDialog();
        }

        private async void btnShoeDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(invoicesDataGrid.SelectedItem is Invoice invoice)
                {
                    int invoiceId = invoice.InvoiceId;
                    ShowInvoiceDetailsForm form = new ShowInvoiceDetailsForm();
                    form.customerName = invoice.InvoiceName;
                    form.currentDate = invoice.DateOfSubmitInvoice.ToShortDateString();
                    form.dueDate = invoice.NextDateVisit.ToShortDateString();
                    form.address = invoice.Description;
                    form.AmountPaied = invoice.AmountPaid;
                    form.TotalPrice = invoice.TotalPrice;
                    form.SetInvoicId(invoiceId);
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async void btnEdite_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (invoicesDataGrid.SelectedItem is Invoice invoice)
                {
                    int invoiceId = invoice.InvoiceId;

                    if (invoiceId >= 0)
                    {
                        EditeInvoicesForm form = new EditeInvoicesForm();

                        var responseMessage = await _callApi.SendGetRequest<Invoice?>($"{url}/api/InvoiceManger/GetSingle?invoiceId={invoiceId}&userId={UserSession.Instance.UserId}", jwt:UserSession.Instance.JwtToken);


                        if(responseMessage.IsSuccess)
                        {
                            var data = responseMessage.Data;
                            if(data != null)
                                form.SetInvoice(data, invoiceId);

                            form.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show($"Error is : {responseMessage.Message}", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(nameof(ex.Message));
            }
        }

        private async void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            await _loadGetApisServies.LoadedInvoices(_currentPage, invoicesDataGrid, txtSearch.Text, _pageSize);
        }
    }
}
