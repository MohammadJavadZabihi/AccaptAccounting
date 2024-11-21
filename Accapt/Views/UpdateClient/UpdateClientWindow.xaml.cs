using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace Accapt.Views.UpdateClient
{
    /// <summary>
    /// Interaction logic for UpdateClientWindow.xaml
    /// </summary>
    public partial class UpdateClientWindow : Window
    {
        public UpdateClientWindow()
        {
            InitializeComponent();
        }

        private string _newVersion = "";
        private string _realesNote = "";
        private string _downlaodURL = "";
        private bool _isMandatory = false;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtReleasNote.Text = _realesNote;
            txtVersion.Text = _newVersion;
        }

        public void LoadData(string newVersion, string realesNote, bool isMandatory, string downloadURL)
        {
            _newVersion = newVersion;
            _realesNote = realesNote; 
            _isMandatory = isMandatory;
            _downlaodURL = downloadURL;
        }

        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {_downlaodURL}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", _downlaodURL);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", _downlaodURL);
                }
                else
                {
                    MessageBox.Show("سیستم عامل پشتیبانی نمی‌شود.", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطایی رخ داد: {ex.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            if(_isMandatory)
            {
                Application.Current.Shutdown();
            }

            this.Close();
        }
    }
}
