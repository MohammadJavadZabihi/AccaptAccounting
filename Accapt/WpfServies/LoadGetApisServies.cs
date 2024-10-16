using Accapt.Core.DTOs;
using Accapt.DataLayer.Entities;
using Accapt.Invoices;
using Accapt.Views.Loading;
using ApiRequest.Net.CallApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Accapt.WpfServies
{
    public class LoadGetApisServies
    {
        private readonly CallApi _callApi;
        private string? url = ConfigurationManager.AppSettings["ApiURL"];
        private string? _localUrl = ConfigurationManager.AppSettings["LocalHost"];
        public LoadGetApisServies()
        {
            _callApi = new CallApi();
        }

        #region LoadProductGetApi

        public async Task<bool> LoadedProduct(int pageNumber, DataGrid dataGrid, string filter, int _pageSize)
        {
            try
            {

                var responseMessgae = await _callApi.SendGetRequest<ShowProductDTO>($"{url}/api/MangeProduct/GetAll?pageNumber={pageNumber}&pageSize={_pageSize}&filter={filter}&userId={UserSession.Instance.UserId}",
                    data: null, jwt: UserSession.Instance.JwtToken);

                if (responseMessgae.IsSuccess)
                {
                    var data = responseMessgae.Data;
                    int pCount = data.Products.Count();
                    if (pCount != 0)
                    {
                        dataGrid.ItemsSource = data.Products;
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(nameof(ex.Message));
            }
        }

        #endregion

        #region LoadPepolGetApi

        public async Task<bool> LaodePepol(int pageNumber, DataGrid dataGrid, string filter, int _pageSize)
        {
            LoadingWindow loading = new LoadingWindow();
            try
            {
                loading.Show();

                var responseMessgae = await _callApi.SendGetRequest<ShowPepoleDTO>($"{url}/api/PepoleManger/GetAll?pageNumber={pageNumber}&pageSize={_pageSize}&filter={filter}&userId={UserSession.Instance.UserId}",
                    data: null, jwt: UserSession.Instance.JwtToken);

                if (responseMessgae.IsSuccess)
                {
                    var data = responseMessgae.Data;
                    int pCount = data.Pepoles.Count();
                    if (pCount != 0)
                    {
                        loading.Close();
                        dataGrid.ItemsSource = data.Pepoles;
                        return true;
                    }
                    loading.Close();
                    return false;
                }
                loading.Close();
                return false;
            }
            catch (Exception ex)
            {
                loading.Close();
                throw new ArgumentException(ex.Message);
            }
        }

        #endregion

        #region LoadInvoicesGetApi

        public async Task<bool> LoadedInvoices(int pageNumber, DataGrid dataGrid, string filter, int _pageSize)
        {
            try
            {

                var responseMessage = await _callApi.SendGetRequest<ShowInvDTO>($"{url}/api/InvoiceManger/GetAll?pageNumber={pageNumber}&pageSize={_pageSize}&filter={filter}&userId={UserSession.Instance.UserId}", jwt: UserSession.Instance.JwtToken);
                if (responseMessage.IsSuccess)
                {
                    var data = responseMessage.Data;
                    int iCount = data.Invoices.Count();
                    if (iCount > 0)
                    {
                        dataGrid.ItemsSource = data.Invoices;
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(nameof(ex.Message));
            }
        }

        #endregion

        #region LoadedInvoiceDeatailsGetApi

        public async Task<bool> LoadedInvoiceDeatails(int invoiceId, DataGrid dataGrid)
        {
            LoadingWindow loading = new LoadingWindow();
            try
            {
                loading.Show();

                var responseMessage = await _callApi.SendGetRequest<IEnumerable<InvoiceDetails>>($"{url}/api/InvoiceManger/GetDetails?userId={UserSession.Instance.UserId}&inoviceId={invoiceId}", jwt: UserSession.Instance.JwtToken);
                if (responseMessage.IsSuccess)
                {
                    var data = responseMessage.Data;
                    loading.Close();
                    dataGrid.ItemsSource = data;
                    return true;
                }
                loading.Close();
                return false;
            }
            catch (Exception ex)
            {
                loading.Close();
                throw new ArgumentException(nameof(ex.Message));
            }
        }

        #endregion

        #region LoadPepoleGetApi

        public async Task<bool> LoadedPepole(int pageNumber, DataGrid dataGrid, string filter, int _pageSize)
        {
            LoadingWindow loading = new LoadingWindow();
            try
            {
                loading.Show();

                var responseMessage = await _callApi.SendGetRequest<ShowPepoleDTO>($"{url}/api/PepoleManger/GetAll?pageNumber={pageNumber}&pageSize={_pageSize}&filter={filter}&userId={UserSession.Instance.UserId}", jwt: UserSession.Instance.JwtToken);
                if (responseMessage.IsSuccess)
                {
                    var data = responseMessage.Data;
                    int iCount = data.Pepoles.Count();
                    if (iCount > 0)
                    {
                        loading.Close();
                        dataGrid.ItemsSource = data.Pepoles;
                        return true;
                    }
                    loading.Close();
                    return false;
                }
                loading.Close();
                return false;
            }
            catch (Exception ex)
            {
                loading.Close();
                throw new ArgumentException(nameof(ex.Message));
            }
        }

        #endregion

        #region LoadeProductGetApiForSearch

        public async Task<bool> LoadedPepoleForSearch(int pageNumber, DataGrid dataGrid, string filter, int _pageSize)
        {
            try
            {
                var responseMessage = await _callApi.SendGetRequest<ShowPepoleDTO>($"{url}/api/PepoleManger/GetAll?pageNumber={pageNumber}&pageSize={_pageSize}&filter={filter}&userId={UserSession.Instance.UserId}", jwt: UserSession.Instance.JwtToken);
                if (responseMessage.IsSuccess)
                {
                    var data = responseMessage.Data;
                    int iCount = data.Pepoles.Count();
                    if (iCount > 0)
                    {
                        dataGrid.ItemsSource = data.Pepoles;
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(nameof(ex.Message));
            }
        }

        #endregion

        #region LoadBankAccountsGetApi

        public async Task<bool> LoadedBankAccounts(int pageNumber, DataGrid dataGrid, string filter, int _pageSize)
        {
            LoadingWindow loading = new LoadingWindow();
            try
            {
                loading.Show();

                var responseMessage = await _callApi.SendGetRequest<ShowBankAccountDTO?>($"{url}/api/BankManager/GetAll?pageNumber={pageNumber}&pageSize={_pageSize}&filter={filter}&userId={UserSession.Instance.UserId}", jwt: UserSession.Instance.JwtToken);
                if (responseMessage.IsSuccess)
                {
                    var data = responseMessage.Data;
                    int iCount = data.BankAccounts.Count();
                    if (iCount > 0)
                    {
                        loading.Close();
                        dataGrid.ItemsSource = data.BankAccounts;
                        return true;
                    }
                    loading.Close();
                    return false;
                }
                loading.Close();
                return false;
            }
            catch (Exception ex)
            {
                loading.Close();
                throw new ArgumentException(nameof(ex.Message));
            }
        }

        #endregion

        #region LoadBankAccountsGetApiForTextBoxSearch

        public async Task<bool> LoadedBankAccountsSearch(int pageNumber, DataGrid dataGrid, string filter, int _pageSize)
        {
            try
            {
                var responseMessage = await _callApi.SendGetRequest<ShowBankAccountDTO?>($"{url}/api/BankManager/GetAll?pageNumber={pageNumber}&pageSize={_pageSize}&filter={filter}&userId={UserSession.Instance.UserId}", jwt: UserSession.Instance.JwtToken);
                if (responseMessage.IsSuccess)
                {
                    var data = responseMessage.Data;
                    int iCount = data.BankAccounts.Count();
                    if (iCount > 0)
                    {
                        dataGrid.ItemsSource = data.BankAccounts;
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(nameof(ex.Message));
            }
        }

        #endregion

        #region ChekBank

        public async Task<bool> GetAllChekBank(int pageNumber, DataGrid dataGrid, string filter ,int pageSize)
        {
            LoadingWindow loading = new LoadingWindow();
            try
            {
                loading.Show();

                var responseMessage = await _callApi.SendGetRequest<ShowChekDTO?>
                    ($"{url}/ChekManger/GetAll?filter={filter}&userId={UserSession.Instance.UserId}&pageNumber={pageNumber}&pageSize={pageSize}");

                if(responseMessage.IsSuccess)
                {
                    var data = responseMessage.Data;
                    int cCount = data.Cheks.Count();
                    if(cCount > 0)
                    {
                        loading.Close();
                        dataGrid.ItemsSource = data.Cheks;
                        return true;
                    }
                    loading.Close();
                    return false;
                }
                loading.Close();
                return false;
            }
            catch (Exception ex)
            {
                loading.Close();
                throw new ArgumentException(nameof(ex.Message));
            }
        }

        #endregion

        #region LoadProductGetApiForComboBox

        public async Task<bool> LoadedProductForComboBox(int pageNumber, ComboBox productCMB, string filter, int _pageSize)
        {
            try
            {

                var responseMessgae = await _callApi.SendGetRequest<ShowProductDTO>($"{url}/api/MangeProduct/GetAll?pageNumber={pageNumber}&pageSize={_pageSize}&filter={filter}&userId={UserSession.Instance.UserId}",
                    data: null, jwt: UserSession.Instance.JwtToken);

                if (responseMessgae.IsSuccess)
                {
                    List<string> productNames = new List<string>();
                    var data = responseMessgae.Data.Products;
                    if (data != null)
                    {
                        foreach (var product in data)
                        {
                            productNames.Add(product.ProductName);
                        }
                        productCMB.ItemsSource = productNames;
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(nameof(ex.Message));
            }
        }

        #endregion

        #region LoadProductGetApiForComboBox

        public async Task<bool> LoadedCustomerForComboBox(int pageNumber, ComboBox productCMB, string filter, int _pageSize)
        {
            try
            {

                var responseMessgae = await _callApi.SendGetRequest<ShowPepoleDTO>($"{url}/api/PepoleManger/GetAll?pageNumber={pageNumber}&pageSize={_pageSize}&filter={filter}&userId={UserSession.Instance.UserId}",
                    data: null, jwt: UserSession.Instance.JwtToken);

                if (responseMessgae.IsSuccess)
                {
                    List<string> productNames = new List<string>();
                    var data = responseMessgae.Data.Pepoles;
                    if (data != null)
                    {
                        foreach (var customer in data)
                        {
                            productNames.Add(customer.PepoName + " " + customer.PepoCode);
                        }
                        productCMB.ItemsSource = productNames;
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(nameof(ex.Message));
            }
        }

        #endregion

        #region LoadedBanckCheckGetApi

        public async Task<bool> LoadedBanckCheck(int pageNumber, DataGrid dataGrid, string filter, int _pageSize)
        {
            try
            {
                var responseMessage = await _callApi.SendGetRequest<ShowChekDTO?>($"{url}/api/ChekManger/GetAll?pageNumber={pageNumber}&pageSize={_pageSize}&filter={filter}&userId={UserSession.Instance.UserId}", jwt: UserSession.Instance.JwtToken);
                if (responseMessage.IsSuccess)
                {
                    var data = responseMessage.Data;
                    int iCount = data.Cheks.Count();
                    if (iCount > 0)
                    {
                        dataGrid.ItemsSource = data.Cheks;
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(nameof(ex.Message));
            }
        }

        #endregion

        #region LoadedDebtOrCredito

        public async Task<bool> LoadedDebtOrCredito(int pageNumber, DataGrid dataGrid, string filter, int _pageSize)
        {
            try
            {
                var responseMessage = await _callApi.SendGetRequest<ShowDebtorCreditorsDTO?>($"{_localUrl}/api/DebtorCreditorManager/GetDebtorCreditors?pageNumber={pageNumber}&pageSize={_pageSize}&filter={filter}&userId={UserSession.Instance.UserId}", jwt: UserSession.Instance.JwtToken);
                if (responseMessage.IsSuccess)
                {
                    var data = responseMessage.Data;
                    int iCount = data.DebtorCreditors.Count();
                    if (iCount > 0)
                    {   
                        dataGrid.ItemsSource = data.DebtorCreditors;
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(nameof(ex.Message));
            }
        }

        #endregion

        #region LoadedEmployees

        public async Task<bool> LoadedEmployees(int pageNumber, DataGrid dataGrid, string filter, int _pageSize)
        {
            try
            {
                var responseMessage = await _callApi.SendGetRequest<ShowEmployeesDTO?>($"{url}/api/EmployeeManger/GetAll?pageNumber={pageNumber}&pageSize={_pageSize}&filter={filter}&userId={UserSession.Instance.UserId}", jwt: UserSession.Instance.JwtToken);
                if (responseMessage.IsSuccess)
                {
                    var data = responseMessage.Data;
                    int iCount = data.Employees.Count();
                    if (iCount > 0)
                    {
                        dataGrid.ItemsSource = data.Employees;
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(nameof(ex.Message));
            }
        }

        #endregion

        #region GetIncomingInvoice

        public async Task<IEnumerable<InvoiceSummary>?> GetIncomingFromSalesIvoice(string currentDtae, string endDate)
        {
            try
            {
                var responseMessage = await _callApi.SendGetRequest<IEnumerable<InvoiceSummary>?>($"{url}/api/BillanManager/Incoming?userId={UserSession.Instance.UserId}&currentDtae={currentDtae}&endDate={endDate}", 
                    jwt: UserSession.Instance.JwtToken);
                if (responseMessage.IsSuccess)
                {
                    return responseMessage.Data;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        #endregion

        #region LoadedSallaryandCosts

        public async Task<bool> LoadedSallaryandCosts(int pageNumber, DataGrid dataGrid, string filter, int _pageSize)
        {
            try
            {
                var responseMessage = await _callApi.SendGetRequest<ShowSallaryandCostsDTO?>($"{url}/api/SalaryAndCostsManager/GetAllSallary?pageNumber={pageNumber}&pageSize={_pageSize}&filter={filter}&userId={UserSession.Instance.UserId}", jwt: UserSession.Instance.JwtToken);
                if (responseMessage.IsSuccess)
                {
                    var data = responseMessage.Data;
                    int iCount = data.SallaryAndCosts.Count();
                    if (iCount > 0)
                    {
                        dataGrid.ItemsSource = data.SallaryAndCosts;
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(nameof(ex.Message));
            }
        }

        #endregion

        #region LoadedProviderSerice

        public async Task<bool> LoadProviderSerice(int pageNumber, DataGrid dataGrid, string filter, int _pageSize)
        {
            try
            {
                var responseMessage = await _callApi.SendGetRequest<ShowProviderDTO?>($"{url}/api/ProviderMnager/GetAll?pageNumber={pageNumber}&pageSize={_pageSize}&filter={filter}&userId={UserSession.Instance.UserId}", jwt: UserSession.Instance.JwtToken);
                if (responseMessage.Data.Providers == null)
                {
                    return false;
                }
                if (responseMessage.IsSuccess)
                {
                    var data = responseMessage.Data;
                    int iCount = data.Providers.Count();
                    if (iCount > 0)
                    {
                        dataGrid.ItemsSource = data.Providers;
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        #endregion

        #region LoadedProviderSericeList

        public async Task<bool> LoadProviderSericeList(int pageNumber, DataGrid dataGrid, string filter, int _pageSize)
        {
            try
            {
                var responseMessage = await _callApi.SendGetRequest<ShowProviderServiceList?>($"{_localUrl}/api/ServiceListManager/GetAll?pageNumber={pageNumber}&pageSize={_pageSize}&filter={filter}&userId={UserSession.Instance.UserId}", jwt: UserSession.Instance.JwtToken);
                if (responseMessage.Data == null)
                {
                    return false;
                }
                if (responseMessage.IsSuccess)
                {
                    var data = responseMessage.Data;
                    int iCount = data.ProvidersList.Count();
                    if (iCount > 0)
                    {
                        dataGrid.ItemsSource = data.ProvidersList;
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

    }
}
