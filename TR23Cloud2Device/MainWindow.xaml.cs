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

using Microsoft.Azure.Devices;


namespace TR23Cloud2Device
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServiceClient serviceClient;
        string connectionString = "HostName=rt3IoTHub.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=oANMWce9ymneEuKC4opJ6BodqBxzIlt95g5lyjbpgzQ=";
        string message = string.Empty;

        public MainWindow()
        {
            InitializeComponent();

            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);

        }

        private async void btnSend_Click(object sender, RoutedEventArgs e)
        {

            await SendCloudToDeviceMessageAsync();

        }

        private async Task SendCloudToDeviceMessageAsync()
        {
            var commandMessage = new Message(Encoding.ASCII.GetBytes(message));
            await serviceClient.SendAsync("VendingMachineOne", commandMessage);
        }


    }
}
