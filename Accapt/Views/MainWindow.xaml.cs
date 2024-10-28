using Accapt.Invoices;
using Accapt.Views.Account;
using Accapt.Views.BankAccount;
using Accapt.Views.Billan;
using Accapt.Views.DebtOrCreditor;
using Accapt.Views.Pepole;
using Accapt.Views.Products;
using Accapt.Views.ProviderManager;
using Accapt.Views.SalaryandCosts;
using AccaptFullyVersion.App.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AccaptFullyVersion.App
{
    public partial class MainWindow : Window
    {
        public bool IsLogin = false;
        public int formId = -1;
        private Button _selectedButton;
        public MainWindow()
        {
            InitializeComponent();
            if (formId == -1 && !IsLogin)
            {
                LoginPage loginPage = new LoginPage(this);
                loginPage.ShowDialog();
            }
        }

        private void btnAccountDetails_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedButton != null)
            {
                _selectedButton.Background = new SolidColorBrush(Colors.Transparent);
                _selectedButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1f1f1"));
            }

            _selectedButton = sender as Button;

            if (_selectedButton != null)
            {
                _selectedButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AD07F5"));
                _selectedButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1f1f1"));
            }

            UserMangeAccount manageAccountPage = new UserMangeAccount();
            fContainer.Navigate(manageAccountPage);

            txtPage.Text = "تنظیمات";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                UserMangeAccount manageAccountPage = new UserMangeAccount();
                fContainer.Navigate(manageAccountPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (_selectedButton != null)
            {
                _selectedButton.Background = new SolidColorBrush(Colors.Transparent);
                _selectedButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1f1f1"));
            }

            _selectedButton = sender as Button;

            if (_selectedButton != null)
            {
                _selectedButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AD07F5"));
                _selectedButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1f1f1"));
            }

            txtPage.Text = "تنظیمات";
        }

        private void btnStore_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedButton != null)
            {
                _selectedButton.Background = new SolidColorBrush(Colors.Transparent);
                _selectedButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1f1f1"));
            }

            _selectedButton = sender as Button;

            if (_selectedButton != null)
            {
                _selectedButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AD07F5"));
                _selectedButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1f1f1"));
            }

            ShowProductPage showProductPage = new ShowProductPage();
            fContainer.Navigate(showProductPage);

            txtPage.Text = "انبار";
        }

        private void btnInvoices_Click(object sender, RoutedEventArgs e)
        {

            if (_selectedButton != null)
            {
                _selectedButton.Background = new SolidColorBrush(Colors.Transparent);
                _selectedButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1f1f1"));
            }

            _selectedButton = sender as Button;

            if (_selectedButton != null)
            {
                _selectedButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AD07F5"));
                _selectedButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1f1f1"));
            }

            Invoices invoices = new Invoices();
            fContainer.Navigate(invoices);

            txtPage.Text = "فاکتورها";
        }

        private void btnPepole_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedButton != null)
            {
                _selectedButton.Background = new SolidColorBrush(Colors.Transparent);
                _selectedButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1f1f1"));
            }

            _selectedButton = sender as Button;

            if (_selectedButton != null)
            {
                _selectedButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AD07F5"));
                _selectedButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1f1f1"));
            }

            ShowPepolPage showPepolPage = new ShowPepolPage();
            fContainer.Navigate(showPepolPage);

            txtPage.Text = "اشخاص";
        }

        private void btnBankAccount_Click(object sender, RoutedEventArgs e)
        {

            if (_selectedButton != null)
            {
                _selectedButton.Background = new SolidColorBrush(Colors.Transparent);
                _selectedButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1f1f1"));
            }

            _selectedButton = sender as Button;

            if (_selectedButton != null)
            {
                _selectedButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AD07F5"));
                _selectedButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1f1f1"));
            }

            BanckAccountShowPage banckAccountShowPage = new BanckAccountShowPage();
            fContainer.Navigate(banckAccountShowPage);

            txtPage.Text = "حساب های بانکی";
        }

        private void btnDebtOrCreditor_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedButton != null)
            {
                _selectedButton.Background = new SolidColorBrush(Colors.Transparent);
                _selectedButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1f1f1"));
            }

            _selectedButton = sender as Button;

            if (_selectedButton != null)
            {
                _selectedButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AD07F5"));
                _selectedButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1f1f1"));
            }

            DebtOrCreditorShowPage debtOrCreditorShowPage = new DebtOrCreditorShowPage();
            fContainer.Navigate(debtOrCreditorShowPage);

            txtPage.Text = "یدهکار بستانکار";
        }

        private void btnBillan_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedButton != null)
            {
                _selectedButton.Background = new SolidColorBrush(Colors.Transparent);
                _selectedButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1f1f1"));
            }

            _selectedButton = sender as Button;

            if (_selectedButton != null)
            {
                _selectedButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AD07F5"));
                _selectedButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1f1f1"));
            }

            BillanShow billanShow = new BillanShow();
            fContainer.Navigate(billanShow);

            txtPage.Text = "بیلان";
        }

        private void btnSalaryCosts_Click(object sender, RoutedEventArgs e)
        {

            if (_selectedButton != null)
            {
                _selectedButton.Background = new SolidColorBrush(Colors.Transparent);
                _selectedButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1f1f1"));
            }

            _selectedButton = sender as Button;

            if (_selectedButton != null)
            {
                _selectedButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AD07F5"));
                _selectedButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1f1f1"));
            }

            SalaryAndCostsPage salaryAndCostsPage = new SalaryAndCostsPage();
            fContainer.Navigate(salaryAndCostsPage);

            txtPage.Text = "هزینه ها";
        }

        private void btnProviderManager_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedButton != null)
            {
                _selectedButton.Background = new SolidColorBrush(Colors.Transparent);
                _selectedButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1f1f1"));
            }

            _selectedButton = sender as Button;

            if (_selectedButton != null)
            {
                _selectedButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AD07F5"));
                _selectedButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1f1f1"));
            }

            ProvidermanagerShow providermanagerShow = new ProvidermanagerShow();
            fContainer.Navigate(providermanagerShow);

            txtPage.Text = "سرویس کارها";
        }
    }
}