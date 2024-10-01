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

namespace Accapt.Views.Invoices
{
    public partial class ChoseTypeOfInvoice : Window
    {
        public ChoseTypeOfInvoice()
        {
            InitializeComponent();
        }

        private void btnSell_Click(object sender, RoutedEventArgs e)
        {
            AddInvoices addInvoices = new AddInvoices();
            addInvoices.typeOfInvoice = "فاکتور فروش";
            addInvoices.ShowDialog();
        }

        private void btnBuye_Click(object sender, RoutedEventArgs e)
        {
            AddInvoices addInvoices = new AddInvoices();
            addInvoices.typeOfInvoice = "فاکتور خرید";
            addInvoices.ShowDialog();
        }
    }
}
