using Accapt.DataLayer.Entities;
using Accapt.Views.Loading;
using Accapt.WpfServies;
using ApiRequest.Net.CallApi;
using System.Configuration;
using System.Net;
using System.Windows;

namespace Accapt.Views.SalaryandCosts
{
    public partial class AddSallaryandCosts : Window
    {
        private SallaryAndCosts _sallaryAndCosts;
        private int _sallaryId = -1;
        private string? _url = ConfigurationManager.AppSettings["ApiURL"];
        private CallApi _callApi;
        private int _click = 0;

        public AddSallaryandCosts()
        {
            InitializeComponent();
            _callApi = new CallApi();
        }

        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (_click == 0)
            {
                _click++;
                LoadingWindow loadingWindow = new LoadingWindow();
                loadingWindow.Show();
                try
                {
                    if (_sallaryId == -1)
                    {
                        var data = new
                        {
                            UserId = UserSession.Instance.UserId,
                            SallaryAndCostsName = txtName.Text,
                            PriceOfSallaryAndCosts = Convert.ToDouble(txtPrice.Text),
                            DateOfSubmitForShow = txtDate.Text,
                            Descriptions = txtDescriptions.Text
                        };

                        var responseMessage = await _callApi.SendPostRequest<bool>($"{_url}/api/SalaryAndCostsManager/AddSallary"
                            , data, UserSession.Instance.JwtToken);

                        loadingWindow.Close();

                        if (responseMessage.IsSuccess)
                        {
                            _click--;
                            MessageBox.Show("هزینه جدید با موفقیت ثبت شد", "موفقیت", MessageBoxButton.OK, MessageBoxImage.Information);
                            txtDate.Text = "";
                            txtDescriptions.Text = "";
                            txtName.Text = "";
                            txtPrice.Text = "";
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
                        var data = new
                        {
                            SallaryId = _sallaryId,
                            UserId = UserSession.Instance.UserId,
                            SallaryAndCostsName = txtName.Text,
                            PriceOfSallaryAndCosts = Convert.ToDouble(txtPrice.Text),
                            DateOfSubmitForShow = txtDate.Text,
                            Descriptions = txtDescriptions.Text
                        };

                        var responseMessage = await _callApi.SendPutRequest<bool>($"{_url}/api/SalaryAndCostsManager/EditSallary"
                            , data, UserSession.Instance.JwtToken);

                        loadingWindow.Close();

                        if (responseMessage.IsSuccess)
                        {
                            _click--;
                            MessageBox.Show("هزینه جدید با موفقیت به روزرسانی شد", "موفقیت", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    loadingWindow.Close();
                    MessageBox.Show($"خطا : {ex.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(_sallaryId != -1)
            {
                txtDate.Text = _sallaryAndCosts.DateOfSubmitForShow;
                txtDescriptions.Text = _sallaryAndCosts.Descriptions;
                txtName.Text = _sallaryAndCosts.SallaryAndCostsName;
                txtPrice.Text = _sallaryAndCosts.PriceOfSallaryAndCosts.ToString();
                lblTitel.Text = "ویرایش هزینه";
            }
        }

        public void SetData(SallaryAndCosts sallary, int sallaryId)
        {
            _sallaryAndCosts = sallary;
            _sallaryId = sallaryId;
        }
    }
}
