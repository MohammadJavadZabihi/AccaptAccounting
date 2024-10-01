using Accapt.Core.DTOs;
using Accapt.DataLayer.Entities;
using Accapt.Views.Loading;
using Accapt.WpfServies;
using ApiRequest.Net.CallApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Accapt.Views.Products
{
    public partial class AddOrEditeProducts : Window
    {
        private int producId = 0;
        private Product _product;
        private string? url = ConfigurationManager.AppSettings["ApiURL"];
        private CallApi _callApi;
        private int _click = 0;
        public AddOrEditeProducts()
        {
            InitializeComponent();
            _callApi = new CallApi();
        }

        public void Product(int productId, Product product)
        {
            producId = productId;
            _product = product;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (producId != 0)
                {
                    if (_product != null)
                    {
                        txtDescriptions.Text = _product.Description;
                        txtProductName.Text = _product.ProductName;
                        txtproductPrice.Text = _product.Price.ToString();
                        txtProductCount.Text = _product.ProductCount.ToString();
                        btnSubmit.Content = "ذخیره تغیرات";
                    }
                }
                else
                {
                    btnSubmit.Content = "افزودن محصول";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Message is : {ex.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (_click == 0)
            {
                _click++;
                try
                {
                    if (producId != 0)
                    {
                        LoadingWindow loadingWindow = new LoadingWindow();
                        loadingWindow.Show();

                        if (!string.IsNullOrEmpty(txtProductName.Text) ||
                            !string.IsNullOrEmpty(txtproductPrice.Text) || !string.IsNullOrEmpty(txtProductCount.Text))
                        {
                            var data = new[]
                            {
                                new { op = "replace", path = "ProductName", value = txtProductName.Text },
                                new { op = "replace", path = "Description", value = txtDescriptions.Text },
                                new { op = "replace", path = "Price", value = txtproductPrice.Text },
                                new { op = "replace", path = "ProductCount", value = txtProductCount.Text },
                            };

                            var responseMessage = await _callApi.SendPatchRequest<Product?>
                                ($"{url}/api/MangeProduct/Update/{UserSession.Instance.UserId}/{producId}", data, UserSession.Instance.JwtToken);

                            loadingWindow.Close();

                            if (responseMessage.IsSuccess)
                            {
                                _click--;
                                MessageBox.Show("تغیرات با موفقیت ثبت شد", "تغیرات", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    else
                    {
                        LoadingWindow loadingWindow = new LoadingWindow();
                        loadingWindow.Show();

                        if (!string.IsNullOrEmpty(txtProductName.Text) ||
                            !string.IsNullOrEmpty(txtproductPrice.Text) || !string.IsNullOrEmpty(txtProductCount.Text) && _click == 0)
                        {
                            var data = new
                            {
                                UserName = UserSession.Instance.Username,
                                Productname = txtProductName.Text,
                                Price = Convert.ToDecimal(txtproductPrice.Text),
                                ProductCount = Convert.ToInt32(txtProductCount.Text),
                                Description = txtDescriptions.Text
                            };

                            var responseMessage = await _callApi.SendPostRequest<AddProductDTO?>
                                ($"{url}/api/MangeProduct/Add", data, UserSession.Instance.JwtToken);

                            loadingWindow.Close();

                            if (responseMessage.IsSuccess)
                            {
                                _click++;
                                MessageBox.Show("کالا با موفقیت ثبت شد", "موفق", MessageBoxButton.OK, MessageBoxImage.Information);
                                txtDescriptions.Text = "";
                                txtProductName.Text = "";
                                txtproductPrice.Text = "";
                                txtProductCount.Text = "";
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
                catch (Exception ex)
                {
                    _click--;
                    MessageBox.Show($"Error Message is :{ex.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
