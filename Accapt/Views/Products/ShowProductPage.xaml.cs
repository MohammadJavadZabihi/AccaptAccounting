using Accapt.Core.DTOs;
using Accapt.DataLayer.Entities;
using Accapt.Views.Loading;
using Accapt.WpfServies;
using ApiRequest.Net.CallApi;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace Accapt.Views.Products
{
    public partial class ShowProductPage : Page
    {
        private readonly CallApi _calApiServies;
        private int _pageSize = 10;
        private int _currentPage = 1;
        private readonly LoadGetApisServies _loadGetApisServies;
        private string? url = ConfigurationManager.AppSettings["ApiURL"];
        public ShowProductPage()
        {
            InitializeComponent();
            _calApiServies = new CallApi();
            _loadGetApisServies = new LoadGetApisServies();
            Loaded += Page_Loaded;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();
            await _loadGetApisServies.LoadedProduct(_currentPage, productDataGrid, txtSearch.Text, _pageSize);
            loadingWindow.Close();
        }

        private async void btnNext_Click(object sender, RoutedEventArgs e)
        {
            _currentPage++;

            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();
            var statuce = await _loadGetApisServies.LoadedProduct(_currentPage, productDataGrid, txtSearch.Text, _pageSize);
            loadingWindow.Close();

            if (!statuce)
            {
                _currentPage--;

                await _loadGetApisServies.LoadedProduct(_currentPage, productDataGrid, txtSearch.Text, _pageSize);
            }
        }

        private async void btnBefor_Click(object sender, RoutedEventArgs e)
        {
            _currentPage--;
            if (_currentPage > 0)
            {
                LoadingWindow loadingWindow = new LoadingWindow();
                loadingWindow.Show();
                await _loadGetApisServies.LoadedProduct(_currentPage, productDataGrid, txtSearch.Text, _pageSize);
                loadingWindow.Close();
            }
            else
            {
                _currentPage++;
            }
        }

        private async void btnDelet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (productDataGrid.SelectedItem is Product product)
                {
                    int productId = product.ProductId;

                    SingleProductNameDTO dtoP = new SingleProductNameDTO
                    {
                        ProductId = productId,
                        UserId = UserSession.Instance.UserId
                    };

                    if (productId != 0)
                    {
                        if (MessageBox.Show("آیا از حذف ایتم انتخوابی مطمعئن هستید؟", "اطمینان", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            var responseMessage = await _calApiServies.SendDeletRequest<object>($"{url}/api/MangeProduct/Delet", dtoP, UserSession.Instance.JwtToken);
                            if (responseMessage.IsSuccess)
                            {
                                MessageBox.Show("محصول انتخوابی با موفقیت حذف شد", "موفقیت", MessageBoxButton.OK, MessageBoxImage.Information);
                                LoadingWindow loadingWindow = new LoadingWindow();
                                loadingWindow.Show();
                                await _loadGetApisServies.LoadedProduct(_currentPage, productDataGrid, txtSearch.Text, _pageSize);
                                loadingWindow.Close();
                            }
                            else
                            {
                                MessageBox.Show($"Error Message : {responseMessage.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("لطفا اول ایتمی را انتخواب کنید و بعد اقدام به اجرای عملیات بر روی آن بکنید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Message : {nameof(ex.Message)}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnEdite_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (productDataGrid.SelectedItem is Product product)
                {
                    int productId = product.ProductId;

                    var responseMessage = await _calApiServies.SendGetRequest<Product?>($"{url}/api/MangeProduct/GetSingle/{productId}", jwt: UserSession.Instance.JwtToken);

                    if (responseMessage.IsSuccess)
                    {
                        var data = responseMessage.Data;

                        if (data == null)
                        {
                            MessageBox.Show(responseMessage.Message, "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        AddOrEditeProducts addOrEditeProducts = new AddOrEditeProducts();
                        addOrEditeProducts.Product(productId, data);
                        addOrEditeProducts.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show(responseMessage.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Message : {ex.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void RefreshPage_Click(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();
            await _loadGetApisServies.LoadedProduct(_currentPage, productDataGrid, txtSearch.Text, _pageSize);
            loadingWindow.Close();
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            AddOrEditeProducts addOrEditeProducts = new AddOrEditeProducts();
            addOrEditeProducts.ShowDialog();
        }

        private async void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            await _loadGetApisServies.LoadedProduct(_currentPage, productDataGrid, txtSearch.Text, _pageSize);
        }
    }
}
