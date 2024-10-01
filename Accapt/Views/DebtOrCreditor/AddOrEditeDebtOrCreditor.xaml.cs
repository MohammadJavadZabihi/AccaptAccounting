using Accapt.Core.DTOs;
using Accapt.DataLayer.Entities;
using Accapt.Views.Loading;
using Accapt.WpfServies;
using ApiRequest.Net.CallApi;
using Microsoft.AspNetCore.Mvc;
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

namespace Accapt.Views.DebtOrCreditor
{
    public partial class AddOrEditeDebtOrCreditor : Window
    {
        private int _id = -1;
        private DebtorCreditor _debtorCreditor;
        private CallApi _callAPi;
        private string? _url = ConfigurationManager.AppSettings["ApiURL"];
        private int _click = 0;
        public AddOrEditeDebtOrCreditor()
        {
            InitializeComponent();
            _callAPi = new CallApi();
        }

        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_id == -1 && _click == 0)
                {
                    _click++;
                    var data = new
                    {
                        UserId = UserSession.Instance.UserId,
                        CustomerName = txtPepoName.Text,
                        PriceOfDebtorCreditor = Convert.ToDouble(txtPrice.Text),
                        Statuce = txtStatuc.Text,
                        DateOfPurchase = Convert.ToDateTime(txtDate.Text),
                        DateOfPurchaseForShow = txtDate.Text,
                        Desctiptions = txtAddress.Text,
                    };

                    LoadingWindow loadingWindow = new LoadingWindow();

                    loadingWindow.Show();

                    var responseMessage = await _callAPi.SendPostRequest<AddDebtorCreditorsDTO?>(
                        $"{_url}/api/DebtorCreditorManager/AddDebtorCreditor", data, UserSession.Instance.JwtToken);

                    loadingWindow.Close();

                    if (responseMessage.IsSuccess)
                    {
                        _click--;

                        MessageBox.Show("عملیات با موفقیت انجام شد", "موفقیت",
                            MessageBoxButton.OK, MessageBoxImage.Information);

                        txtAddress.Text = "";
                        txtDate.Text = "";
                        txtPepoName.Text = "";
                        txtPrice.Text = "";
                        txtStatuc.Text = "";
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
                else if (_click == 0)
                {
                    _click++;

                    var data = new
                    {
                        CreditorId = _id,
                        UserId = UserSession.Instance.UserId,
                        CustomerName = txtPepoName.Text,
                        Price = Convert.ToDouble(txtPrice.Text),
                        Descriptions = txtAddress.Text,
                        Statuce = txtStatuc.Text,
                    };

                    LoadingWindow loadingWindow = new LoadingWindow();
                    loadingWindow.Show();

                    var responseMessage = await _callAPi.SendPutRequest<bool>(
                        $"{_url}/api/DebtorCreditorManager/EditDebtorCreditor", data, UserSession.Instance.JwtToken);

                    loadingWindow.Close();

                    if (responseMessage.IsSuccess)
                    {
                        _click--;

                        MessageBox.Show("عملیات با موفقیت انجام شد", "موفقیت",
                             MessageBoxButton.OK, MessageBoxImage.Information);

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
            }
            catch (Exception ex)
            {
                _click--;
                MessageBox.Show($"{ex.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SetDebtOrCreditor(int id, DebtorCreditor debtorCreditor)
        {
            _debtorCreditor = debtorCreditor;
            _id = id;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(_id != -1)
            {
                txtAddress.Text = _debtorCreditor.Desctiptions;
                txtDate.Text = _debtorCreditor.DateOfPurchaseForShow;
                txtPepoName.Text = _debtorCreditor.CustomerName;
                txtPrice.Text = _debtorCreditor.PriceOfDebtorCreditor.ToString();
                txtStatuc.Text = _debtorCreditor.Statuce;
            }
        }
    }
}
