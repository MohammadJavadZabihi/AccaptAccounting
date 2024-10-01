using Accapt.Views.Loading;
using Accapt.WpfServies;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Accapt.Views.Employee
{
    /// <summary>
    /// Interaction logic for EmployeePageShow.xaml
    /// </summary>
    public partial class EmployeePageShow : Page
    {
        private LoadGetApisServies _LoadGetApisServies;
        private int _pageSize = 10;
        private int _currentPage = 1;
        public EmployeePageShow()
        {
            InitializeComponent();
            _LoadGetApisServies = new LoadGetApisServies();
        }

        private void btnAddPepole_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void RefreshPage_Click(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();

            await _LoadGetApisServies.LoadedEmployees(_currentPage, employeesDataGrid, txtSearch.Text, _pageSize);

            loadingWindow.Close();
        }

        private async void btnBefor_Click(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();

            _currentPage--;

            if(_currentPage <= 0)
            {
                _currentPage++;
                await _LoadGetApisServies.LoadedEmployees(_currentPage, employeesDataGrid, txtSearch.Text, _pageSize);
            }
            else
            {
                await _LoadGetApisServies.LoadedEmployees(_currentPage, employeesDataGrid, txtSearch.Text, _pageSize);
            }
        }

        private async void btnNext_Click(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();

            _currentPage++;
            var statuce = await _LoadGetApisServies.LoadedEmployees(_currentPage, employeesDataGrid, txtSearch.Text, _pageSize);

            if(!statuce)
            {
                _currentPage--;
                await _LoadGetApisServies.LoadedEmployees(_currentPage, employeesDataGrid, txtSearch.Text, _pageSize);

                loadingWindow.Close();
            }
            else
            {
                await _LoadGetApisServies.LoadedEmployees(_currentPage, employeesDataGrid, txtSearch.Text, _pageSize);
                loadingWindow.Close();  
            }
        }

        private void btnDelet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEdite_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            await _LoadGetApisServies.LoadedEmployees(_currentPage, employeesDataGrid, txtSearch.Text, _pageSize);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
