using Accapt.WpfServies;
using LiveCharts;
using LiveCharts.Wpf;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using Accapt.Core.Convertor;
using Accapt.Views.Loading;
using ApiRequest.Net.CallApi;
using Accapt.Core.DTOs;
using System.Configuration;
using System.Net;

namespace Accapt.Views.Billan
{
    public partial class BillanShow : Page, INotifyPropertyChanged
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels2 { get; set; }

        private CallApi _callApi;
        private LoadGetApisServies _loadGetApisServies;
        private string? _url = ConfigurationManager.AppSettings["ApiURL"];
        private int _click = 0;

        public BillanShow()
        {
            InitializeComponent();

            _loadGetApisServies = new LoadGetApisServies();
            _callApi = new CallApi();

            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void Border_Loaded(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();

            string currenDate = "";
            string endDate = "";
            if (string.IsNullOrEmpty(txtEndDate.Text) && string.IsNullOrEmpty(txtStartDate.Text))
            {
                currenDate = DateConvertor.ConvertToShamsi(DateTime.UtcNow.AddDays(-10));
                endDate = DateConvertor.ConvertToShamsi(DateTime.UtcNow);
            }
            else
            {
                currenDate = txtStartDate.Text;
                endDate = txtEndDate.Text;
            }

            var incoming = await _loadGetApisServies.GetIncomingFromSalesIvoice(currenDate, endDate);

            List<double> price = new List<double>();
            List<string> labels = new List<string>();

            List<string> allDates = GetDateRange(currenDate, endDate);

            foreach (var date in allDates)
            {
                var data = incoming.FirstOrDefault(x => x.DateOfSubmit == date);
                if (data != null)
                {
                    price.Add(data.InvoicePrice);
                }
                else
                {
                    price.Add(0);
                }
                labels.Add(date);
            }

            billanChart.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "مبلغ تومان",
                    Values = new ChartValues<double>(price)
                }
            };

            Labels2 = labels.ToArray();
            OnPropertyChanged(nameof(Labels2));

            loadingWindow.Close();
        }

        private List<string> GetDateRange(string startDate, string endDate)
        {
            List<string> dateRange = new List<string>();
            PersianCalendar pc = new PersianCalendar();

            DateTime start = DateTime.ParseExact(startDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            DateTime end = DateTime.ParseExact(endDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);

            while (start <= end)
            {
                dateRange.Add(start.ToString("yyyy/MM/dd"));
                start = start.AddDays(1);
            }

            return dateRange;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();
            try
            {
                string currenDate = "";
                string endDate = "";
                if (string.IsNullOrEmpty(txtEndDate.Text) && string.IsNullOrEmpty(txtStartDate.Text))
                {
                    currenDate = DateConvertor.ConvertToShamsi(DateTime.UtcNow.AddDays(-10));
                    endDate = DateConvertor.ConvertToShamsi(DateTime.UtcNow);
                }
                else
                {
                    currenDate = txtStartDate.Text;
                    endDate = txtEndDate.Text;
                }

                var incoming = await _loadGetApisServies.GetIncomingFromSalesIvoice(currenDate, endDate);

                List<double> price = new List<double>();
                List<string> labels = new List<string>();

                List<string> allDates = GetDateRange(currenDate, endDate);

                foreach (var date in allDates)
                {
                    var data = incoming.FirstOrDefault(x => x.DateOfSubmit == date);
                    if (data != null)
                    {
                        price.Add(data.InvoicePrice);
                    }
                    else
                    {
                        price.Add(0);
                    }
                    labels.Add(date);
                }

                billanChart.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "مبلغ تومان",
                    Values = new ChartValues<double>(price)
                }
            };

                Labels2 = labels.ToArray();
                OnPropertyChanged(nameof(Labels2));

                loadingWindow.Close();
            }
            catch (Exception ex)
            {
                loadingWindow.Close();
                MessageBox.Show($"خطا : {ex.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnBillan_Click(object sender, RoutedEventArgs e)
        {
            if (_click == 0)
            {
                try
                {
                    _click++;
                    var data = new
                    {
                        StartDate = txtStartDate.Text,
                        EndDate = txtEndDate.Text,
                        UserId = UserSession.Instance.UserId
                    };

                    LoadingWindow loadingWindow = new LoadingWindow();
                    loadingWindow.Show();

                    var responseMessage = await _callApi.SendPostRequest<BillanSummaryDTO?>($"{_url}/api/BillanManager/Add",
                        data, UserSession.Instance.JwtToken);

                    loadingWindow.Close();

                    if (responseMessage.IsSuccess)
                    {
                        AddBillanWindow addBillanWindow = new AddBillanWindow();
                        addBillanWindow.SetData(responseMessage.Data);
                        addBillanWindow.ShowDialog();
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
                catch (Exception ex)
                {
                    _click--;
                    MessageBox.Show($"Error : {ex.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
