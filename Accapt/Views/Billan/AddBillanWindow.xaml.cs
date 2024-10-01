using Accapt.Core.DTOs;
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
using System.Windows.Shapes;

namespace Accapt.Views.Billan
{
    /// <summary>
    /// Interaction logic for AddBillanWindow.xaml
    /// </summary>
    public partial class AddBillanWindow : Window
    {
        private BillanSummaryDTO _billanSummaryDTO { get; set; }
        public AddBillanWindow()
        {
            InitializeComponent();
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            sallaryDataGrid.ItemsSource = _billanSummaryDTO.NegetiveInvoices;
            IncomingDataGrid.ItemsSource = _billanSummaryDTO.PlusInvoices;
            lblSallary.Text = _billanSummaryDTO.NegetivePrice.ToString();
            lblSells.Text = _billanSummaryDTO.PlusPrice.ToString();
            lblTotalPrice.Text = _billanSummaryDTO.TotalPrice.ToString();
        }

        public void SetData(BillanSummaryDTO billanSummaryDTO)
        {
            _billanSummaryDTO = billanSummaryDTO;
        }
    }
}
