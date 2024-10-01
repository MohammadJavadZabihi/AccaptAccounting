using Accapt.Core.DTOs;
using Accapt.DataLayer.Entities;
using Accapt.Views.Loading;
using Accapt.WpfServies;
using ApiRequest.Net.CallApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Drawing2D;
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

namespace Accapt.Views.Pepole
{
    public partial class AddOrEditePepleWindow : Window
    {
        private Accapt.DataLayer.Entities.Pepole _pepole;
        private string? url = ConfigurationManager.AppSettings["ApiURL"];
        private string _pepolName = "";
        private readonly CallApi _callApi;
        private string statuceOfDebCR = "";
        private int _click = 0;
        public AddOrEditePepleWindow()
        {
            InitializeComponent();
            _callApi = new CallApi();
        }

        public void GetPepol(string pepoName, Accapt.DataLayer.Entities.Pepole pepole)
        {
            _pepole = pepole;
            _pepolName = pepoName;
        }

        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (_click == 0)
            {
                _click++;

                try
                {
                    if (_pepole != null || _pepolName != "")
                    {
                        LoadingWindow loadingWindow = new LoadingWindow();
                        loadingWindow.Show();

                        var dataForUpdate = new
                        {
                            Id = UserSession.Instance.UserId,
                            PepoName = txtPepoName.Text,
                            PhoneNumber = txtPhoneNumber.Text,
                            PepoType = txtTypeOfPepo.Text,
                            Address = txtAddress.Text,
                            PepoCode = txtPepoCode.Text
                        };

                        var responseMessage = await _callApi.SendPutRequest<bool>($"{url}/api/PepoleManger/Update/{_pepolName}", dataForUpdate, UserSession.Instance.JwtToken);

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
                    else
                    {
                        LoadingWindow loadingWindow = new LoadingWindow();
                        loadingWindow.Show();

                        var dataForAddNew = new
                        {
                            Id = UserSession.Instance.UserId,
                            PepoName = txtPepoName.Text,
                            PhoneNumber = txtPhoneNumber.Text,
                            Address = txtAddress.Text,
                            PepoType = txtTypeOfPepo.Text,
                            PepoCode = txtPepoCode.Text,
                        };

                        var responseMessage = await _callApi.SendPostRequest<AddPepolDTO?>($"{url}/api/PepoleManger/Add/", dataForAddNew, UserSession.Instance.JwtToken);

                        loadingWindow.Close();

                        if (responseMessage.IsSuccess)
                        {
                            _click--;
                            MessageBox.Show("شخص مورد نظر با موفقیت ثبت شد", "تغیرات", MessageBoxButton.OK, MessageBoxImage.Information);
                            txtTypeOfPepo.Text = "";
                            txtPhoneNumber.Text = "";
                            txtPepoName.Text = "";
                            statuceOfDebCR = "";
                            txtAddress.Text = "";
                            txtPepoCode.Text = "";
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
                    MessageBox.Show($"{ex.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_pepolName != "" && _pepole != null)
                {
                    txtAddress.Text = _pepole.Address;
                    txtPepoName.Text = _pepole.PepoName;
                    txtPhoneNumber.Text = _pepole.PhoneNumber;
                    txtTypeOfPepo.Text = _pepole.PepoType;
                    txtPepoCode.Text = _pepole.PepoCode;
                    btnSubmit.Content = "ذخیره تغیرات";
                }
                else
                {
                    btnSubmit.Content = "افزودن شخص جدید";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Message is : {ex.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
