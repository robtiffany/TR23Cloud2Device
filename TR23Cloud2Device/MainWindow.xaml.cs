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
using Newtonsoft.Json;

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

        double productOnePrice;


        public MainWindow()
        {
            InitializeComponent();

            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);

            comboBoxVendingMachines.Items.Add("VendingMachineOne");
            comboBoxVendingMachines.Items.Add("VendingMachineTwo");

        }

        private async void btnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Commands commands = new Commands();
                commands.LastUpdated = DateTime.Now;

                if (checkBoxProductOneName.IsChecked == true)
                    commands.ProductOneName = textBoxProductOneName.Text;

                if(checkBoxProductOnePrice.IsChecked == true)
                    commands.ProductOnePrice = Convert.ToDouble(textBoxProductOnePrice.Text);

                if (checkBoxProductTwoName.IsChecked == true)
                    commands.ProductTwoName = textBoxProductTwoName.Text;

                if (checkBoxProductTwoPrice.IsChecked == true)
                    commands.ProductTwoPrice = Convert.ToDouble(textBoxProductTwoPrice.Text);

                var messageString = JsonConvert.SerializeObject(commands);

                await SendCloudToDeviceMessageAsync(messageString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Sending Commands");
            }
        }   


        private async Task SendCloudToDeviceMessageAsync(string message)
        {
            var commandMessage = new Message(Encoding.ASCII.GetBytes(message));
            await serviceClient.SendAsync(comboBoxVendingMachines.Text, commandMessage);
        }


    }
}
