using Accapt.DataLayer.Entities;
using Accapt.Invoices;
using Accapt.WpfServies;
using ApiRequest.Net.CallApi;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Accapt.Views.Invoices
{
    public partial class EditeInvoicesForm : Window
    {
        private readonly CallApi _callApi;
        private string? url = ConfigurationManager.AppSettings["ApiURL"];
        private int _click = 0;
        private int _invoiceId { get; set; }
        private Invoice _invoice { get; set; }

        public EditeInvoicesForm()
        {
            InitializeComponent();
            _callApi = new CallApi();
        }

        public void SetInvoice(Invoice inv, int invId)
        {
            _invoiceId = invId;
            _invoice = inv;
        }

        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (_click == 0)
            {
                _click++;
                if (!string.IsNullOrEmpty(txtAmountPaid.Text) && !string.IsNullOrEmpty(txtInvoiceName.Text))
                {
                    var data = new
                    {
                        UserId = UserSession.Instance.UserId,
                        InvoiceId = _invoiceId,
                        InvoiceName = txtInvoiceName.Text,
                        AmountPaide = Convert.ToDecimal(txtAmountPaid.Text)
                    };

                    var responseMessage = await _callApi.SendPutRequest<bool>($"{url}/api/InvoiceManger/Update", data,
                        UserSession.Instance.JwtToken);

                    if (responseMessage.IsSuccess)
                    {
                        _click--;
                        MessageBox.Show("عملیات ما موفقیت انجام شد", "موفقیت", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
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
                    MessageBox.Show("لطفا فیلدهای الزامی را وارد نمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtAmountPaid.Text = _invoice.AmountPaid.ToString();
            txtInvoiceName.Text = _invoice.InvoiceName.ToString();
        }
    }
}
