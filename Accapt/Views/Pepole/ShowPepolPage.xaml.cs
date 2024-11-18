using Accapt.Core.Convertor;
using Accapt.DataLayer.Entities;
using Accapt.Views.Loading;
using Accapt.WpfServies;
using ApiRequest.Net.CallApi;
using Microsoft.VisualBasic;
using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Words.NET;

namespace Accapt.Views.Pepole
{
    public partial class ShowPepolPage : Page
    {
        private string? url = ConfigurationManager.AppSettings["ApiURL"];
        private readonly LoadGetApisServies _loadGetApisServies;
        private readonly CallApi _callApi;
        private int _pageSize = 10;
        private int _currentPage = 1;
        public ShowPepolPage()
        {
            InitializeComponent();
            _loadGetApisServies = new LoadGetApisServies();
            _callApi = new CallApi();
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
            }
        }
        private async void RefreshPage_Click(object sender, RoutedEventArgs e)
        {
            await _loadGetApisServies.LoadedPepole(_currentPage, pepoleDataGrid, txtSearch.Text, _pageSize);
        }

        private async void btnAddPepole_Click(object sender, RoutedEventArgs e)
        {
            AddOrEditePepleWindow addOrEditePepleWindow = new AddOrEditePepleWindow();
            addOrEditePepleWindow.ShowDialog();
        }

        private async void btnNext_Click(object sender, RoutedEventArgs e)
        {
            _currentPage++;
            var statuce = await _loadGetApisServies.LoadedPepole(_currentPage, pepoleDataGrid, txtSearch.Text, _pageSize);
            if (!statuce)
            {
                _currentPage--;
            }
        }

        private async void btnBefor_Click(object sender, RoutedEventArgs e)
        {
            _currentPage--;
            if (_currentPage > 0)
            {
                var statuce = await _loadGetApisServies.LoadedPepole(_currentPage, pepoleDataGrid, txtSearch.Text, _pageSize);
            }
            else
            {
                _currentPage++;
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await _loadGetApisServies.LoadedPepole(_currentPage, pepoleDataGrid, txtSearch.Text, _pageSize);
        }

        private async void btnDelet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pepoleDataGrid.SelectedItem is Accapt.DataLayer.Entities.Pepole pepo)
                {
                    if (MessageBox.Show("آیا از حذف ایتم انتخوابی مطمعئن هستید؟", "اطمینان", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        var pepoName = pepo.PepoName;
                        var pepoCode = pepo.PepoCode;
                        var responseMessage = await _callApi.SendDeletRequest<bool>($"{url}/api/PepoleManger/Delet?pepoName={pepoName}&pepolCode={pepoCode}", jwt: UserSession.Instance.JwtToken);
                        if (responseMessage.IsSuccess)
                        {
                            MessageBox.Show("محصول انتخوابی با موفقیت حذف شد", "موفقیت", MessageBoxButton.OK, MessageBoxImage.Information);
                            await _loadGetApisServies.LoadedPepole(_currentPage, pepoleDataGrid, txtSearch.Text, _pageSize);
                        }
                        else if (responseMessage.StatusCode == null)
                        {
                            MessageBox.Show($"لطفا اتصال خود را به اینترنت بررسی کنید و دوباره تلاش کنید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            MessageBox.Show($"خطای احراز حویت", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
                        {
                            MessageBox.Show($"لطفا در ارسال اطلاعات دقت فرمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else if (responseMessage.StatusCode == HttpStatusCode.InternalServerError)
                        {
                            MessageBox.Show($"خطا از طرف سرور, لطفا بعدا دوباره تلاش کنید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Message is : {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnEdite_Click(object sender, RoutedEventArgs e)
        {
            if (pepoleDataGrid.SelectedItem is Accapt.DataLayer.Entities.Pepole pepo)
            {
                var pepoName = pepo.PepoName;
                var pepolCode = pepo.PepoCode;

                if (pepoName != null)
                {
                    var responseMessage = await _callApi.SendGetRequest<Accapt.DataLayer.Entities.Pepole?>
                        ($"{url}/api/PepoleManger/GetSingle/{pepoName}/{pepolCode}", jwt: UserSession.Instance.JwtToken);

                    if (responseMessage.IsSuccess)
                    {
                        var data = responseMessage.Data;

                        if (data == null)
                        {
                            MessageBox.Show(responseMessage.Message, "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            AddOrEditePepleWindow addOrEditePepleWindow = new AddOrEditePepleWindow();
                            addOrEditePepleWindow.GetPepol(pepoName, data);
                            addOrEditePepleWindow.ShowDialog();
                        }
                    }
                }
            }
        }

        private async void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            await _loadGetApisServies.LoadedPepoleForSearch(_currentPage, pepoleDataGrid, txtSearch.Text, _pageSize);
        }

        private async void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadingWindow loadingWindow = new LoadingWindow();
                loadingWindow.Show();

                if (pepoleDataGrid.SelectedItem is Accapt.DataLayer.Entities.Pepole pepo)
                {
                    var pepoName = pepo.PepoName;
                    var pepolCode = pepo.PepoCode;

                    var responseMessage = await _callApi.SendGetRequest<Accapt.DataLayer.Entities.Pepole?>
                       ($"{url}/api/PepoleManger/GetSingle/{pepoName}/{pepolCode}", jwt: UserSession.Instance.JwtToken);

                    if (responseMessage.IsSuccess)
                    {
                        var data = responseMessage.Data;

                        if (data != null)
                        {
                            SaveFileDialog saveFileDialog = new SaveFileDialog();
                            saveFileDialog.Filter = "Word Document (*.docx)|*.docx";
                            saveFileDialog.Title = "انتخاب مسیر ذخیره‌سازی فایل";

                            if (saveFileDialog.ShowDialog() == true)
                            {
                                string selectedPath = saveFileDialog.FileName;

                                string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CutomerDocument.docx");
                                var doc = DocX.Load(filePath);

                                var pepoCode = data.PepoCode;

                                if (pepoCode == null)
                                    pepoCode = "ندارد";

                                var currentDate = DateConvertor.ConvertToShamsi(DateTime.UtcNow);
                                var dueDate = DateConvertor.ConvertToShamsi(DateTime.UtcNow.AddMonths(6));

                                doc.ReplaceText("۳نام مشتری", pepoName.Replace(pepoCode, ""));
                                doc.ReplaceText("کداشتراک شخص", pepoCode);
                                doc.ReplaceText("آدرس شخص", data.Address);
                                doc.ReplaceText("تلفن شخص", data.PhoneNumber);
                                doc.ReplaceText("تاریخ جاری", currentDate);
                                doc.ReplaceText("تاریخ بعدی", dueDate);

                                doc.SaveAs(selectedPath);
                            }

                            loadingWindow.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Message is : {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
