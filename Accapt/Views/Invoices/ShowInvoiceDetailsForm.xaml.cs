using Accapt.DataLayer.Entities;
using Accapt.Views.Loading;
using Accapt.Views.PrintInvoice;
using Accapt.WpfServies;
using ApiRequest.Net.CallApi;
using Microsoft.Win32;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using Xceed.Words.NET;

namespace Accapt.Views.Invoices
{
    public partial class ShowInvoiceDetailsForm : Window
    {
        private int invoiceId = -1;
        private readonly CallApi _callApi;
        private readonly LoadGetApisServies _loadGetApisServies;
        private string? url = ConfigurationManager.AppSettings["ApiURL"];
        public string customerName = "";
        public string address = "";
        public string customerCode = "";
        public string phoneNumber = "";
        public string dueDate = "";
        public string currentDate = "";
        public decimal AmountPaied = 0;
        public decimal TotalPrice = 0;
        public ShowInvoiceDetailsForm()
        {
            InitializeComponent();
            _callApi = new CallApi();
            _loadGetApisServies = new LoadGetApisServies();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await _loadGetApisServies.LoadedInvoiceDeatails(invoiceId, invoicesDataGrid);
        }

        public void SetInvoicId(int Id)
        {
            invoiceId = Id;
        }

        private async void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();

            loadingWindow.Show();

            var responseMessae = await _callApi.SendGetRequest<Accapt.DataLayer.Entities.Pepole>
                ($"{url}/api/PepoleManger/GetSingle/{UserSession.Instance.UserId}/{customerName}", jwt: UserSession.Instance.JwtToken);

            if (responseMessae.IsSuccess)
            {
                var data = responseMessae.Data;

                var invDataGrid = invoicesDataGrid.ItemsSource;

                var invdata = invDataGrid as IEnumerable<InvoiceDetails>;

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Word Document (*.docx)|*.docx";
                saveFileDialog.Title = "انتخاب مسیر ذخیره‌سازی فایل";

                if (saveFileDialog.ShowDialog() == true)
                {
                    string selectedPath = saveFileDialog.FileName;

                    string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Invoices.docx");
                    var doc = DocX.Load(filePath);

                    var pepoCode = data.PepoCode;

                    if (pepoCode == null)
                        pepoCode = "ندارد";

                    doc.ReplaceText("۳نام مشتری", customerName.Replace(pepoCode, ""));
                    doc.ReplaceText("کداشتراک شخص", pepoCode);
                    doc.ReplaceText("آدرس شخص", data.Address);
                    doc.ReplaceText("تلفن شخص", data.PhoneNumber);
                    doc.ReplaceText("تاریخ جاری", currentDate);
                    doc.ReplaceText("تاریخ بعدی", dueDate);

                    var table = doc.Tables.FirstOrDefault();

                    if (table != null)
                    {
                        var fontSize = 8;
                        var fontFamily = "Vazir";

                        for (int i = 0; i < invoicesDataGrid.Items.Count; i++)
                        {
                            var row = table.InsertRow();
                            row.Cells[5].Paragraphs[0].Append((i + 1).ToString()).Font(fontFamily).FontSize(fontSize);
                            row.Cells[0].Paragraphs[0].Append(((TextBlock)invoicesDataGrid.Columns[5].GetCellContent(invoicesDataGrid.Items[i])).Text).Font(fontFamily).FontSize(fontSize);
                            row.Cells[1].Paragraphs[0].Append(((TextBlock)invoicesDataGrid.Columns[4].GetCellContent(invoicesDataGrid.Items[i])).Text).Font(fontFamily).FontSize(fontSize);
                            row.Cells[3].Paragraphs[0].Append(((TextBlock)invoicesDataGrid.Columns[3].GetCellContent(invoicesDataGrid.Items[i])).Text).Font(fontFamily).FontSize(fontSize);
                            row.Cells[2].Paragraphs[0].Append(((TextBlock)invoicesDataGrid.Columns[2].GetCellContent(invoicesDataGrid.Items[i])).Text).Font(fontFamily).FontSize(fontSize);
                            row.Cells[4].Paragraphs[0].Append(((TextBlock)invoicesDataGrid.Columns[1].GetCellContent(invoicesDataGrid.Items[i])).Text).Font(fontFamily).FontSize(fontSize);
                        }
                    }

                    doc.ReplaceText("مبلغ کلی", Convert.ToDouble(TotalPrice).ToString());
                    doc.ReplaceText("مبلغ پیش پرداختی", Convert.ToDouble(AmountPaied).ToString());

                    doc.SaveAs(selectedPath);
                }

                loadingWindow.Close();
            }
            else if(customerName != null)
            {
                string[] parts = customerName.Split(' ');

                string user = parts[0];

                var responseMessaeForAnotherTry = await _callApi.SendGetRequest<Accapt.DataLayer.Entities.Pepole>
                ($"{url}/api/PepoleManger/GetSingle/{UserSession.Instance.UserId}/{user}", jwt: UserSession.Instance.JwtToken);

                loadingWindow.Close();

                if (responseMessaeForAnotherTry.IsSuccess)
                {

                    var data = responseMessaeForAnotherTry.Data;

                    var invDataGrid = invoicesDataGrid.ItemsSource;

                    var invdata = invDataGrid as IEnumerable<InvoiceDetails>;

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Word Document (*.docx)|*.docx";
                    saveFileDialog.Title = "انتخاب مسیر ذخیره‌سازی فایل";

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        string selectedPath = saveFileDialog.FileName;

                        string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Invoices.docx"); ;
                        var doc = DocX.Load(filePath);

                        var pepoCode = data.PepoCode;

                        if (pepoCode == null)
                            pepoCode = "ندارد";

                        doc.ReplaceText("۳نام مشتری", customerName.Replace(pepoCode, ""));
                        doc.ReplaceText("کداشتراک شخص", pepoCode);
                        doc.ReplaceText("آدرس شخص", data.Address);
                        doc.ReplaceText("تلفن شخص", data.PhoneNumber);
                        doc.ReplaceText("تاریخ جاری", currentDate);
                        doc.ReplaceText("تاریخ بعدی", dueDate);

                        var table = doc.Tables.FirstOrDefault();

                        if (table != null)
                        {
                            var fontSize = 8;
                            var fontFamily = "Vazir";

                            for (int i = 0; i < invoicesDataGrid.Items.Count; i++)
                            {
                                var row = table.InsertRow();
                                row.Cells[5].Paragraphs[0].Append((i + 1).ToString()).Font(fontFamily).FontSize(fontSize);
                                row.Cells[0].Paragraphs[0].Append(((TextBlock)invoicesDataGrid.Columns[5].GetCellContent(invoicesDataGrid.Items[i])).Text).Font(fontFamily).FontSize(fontSize);
                                row.Cells[1].Paragraphs[0].Append(((TextBlock)invoicesDataGrid.Columns[4].GetCellContent(invoicesDataGrid.Items[i])).Text).Font(fontFamily).FontSize(fontSize);
                                row.Cells[3].Paragraphs[0].Append(((TextBlock)invoicesDataGrid.Columns[3].GetCellContent(invoicesDataGrid.Items[i])).Text).Font(fontFamily).FontSize(fontSize);
                                row.Cells[2].Paragraphs[0].Append(((TextBlock)invoicesDataGrid.Columns[2].GetCellContent(invoicesDataGrid.Items[i])).Text).Font(fontFamily).FontSize(fontSize);
                                row.Cells[4].Paragraphs[0].Append(((TextBlock)invoicesDataGrid.Columns[1].GetCellContent(invoicesDataGrid.Items[i])).Text).Font(fontFamily).FontSize(fontSize);
                            }
                        }

                        doc.ReplaceText("مبلغ کلی", Convert.ToDouble(TotalPrice).ToString());
                        doc.ReplaceText("مبلغ پیش پرداختی", Convert.ToDouble(AmountPaied).ToString());

                        doc.SaveAs(selectedPath);
                        loadingWindow.Close();
                    }
                    else
                    {
                        loadingWindow.Close();
                        MessageBox.Show($"خطا : {responseMessae.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                loadingWindow.Close();
                MessageBox.Show($"خطا : {responseMessae.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
