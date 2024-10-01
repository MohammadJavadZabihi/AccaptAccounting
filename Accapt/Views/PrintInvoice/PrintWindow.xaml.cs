using Accapt.DataLayer.Entities;
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

namespace Accapt.Views.PrintInvoice
{
    /// <summary>
    /// Interaction logic for PrintWindow.xaml
    /// </summary>
    public partial class PrintWindow : Window
    {
        public PrintWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private FlowDocument CreateInvoiceFlowDocument()
        {
            // Create and format your invoice content here using FlowDocument elements
            FlowDocument flowDocument = new FlowDocument();

            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run("Invoice Content Here"));
            flowDocument.Blocks.Add(paragraph);

            return flowDocument;
        }

        public void GetPrintInvoice()
        {

        }
    }
}
