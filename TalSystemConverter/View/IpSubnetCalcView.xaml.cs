using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace TalSystemConverter.View
{
    /// <summary>
    /// Interaction logic for IpSubnetCalcView.xaml
    /// </summary>
    public partial class IpSubnetCalcView : UserControl
    {
        public IpSubnetCalcView()
        {
            InitializeComponent();
        }
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ipAddress = IPAddress.Parse(IpAddressTextBox.Text);
                var netMask = int.Parse(NetMaskTextBox.Text.Replace("/", ""));

                // Beregn subnetmask
                var subnetMask = GetSubnetMask(netMask);

                // Beregn networkaddress
                var networkAddress = GetNetworkAddress(ipAddress, subnetMask);

                // Beregn broadcastaddress
                var broadcastAddress = GetBroadcastAddress(networkAddress, subnetMask);

                // Beregn hostrange
                var (firstHost, lastHost) = GetHostRange(networkAddress, broadcastAddress);

                // Beregn nummer af hosts
                var numberOfHosts = (int)Math.Pow(2, 32 - netMask) - 2;

                // Display resultater
                SubnetMaskTextBox.Text = subnetMask.ToString();
                NetworkAddressTextBox.Text = networkAddress.ToString();
                RangeTextBox.Text = $"{firstHost} - {lastHost}";
                NumberOfHostsTextBox.Text = numberOfHosts.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private IPAddress GetSubnetMask(int netMask)
        {
            // Sikre netMask er valid
            if (netMask < 0 || netMask > 32)
            {
                throw new ArgumentException("Invalid netmask værdi.");
            }

            byte[] maskBytes = new byte[4];

            // Beregn subnet mask bytes
            for (int i = 0; i < 4; i++)
            {
                if (netMask >= 8)
                {
                    maskBytes[i] = 255; // 8 bits sat til 1 (255 i decimal)
                    netMask -= 8;
                }
                else if (netMask > 0)
                {
                    maskBytes[i] = (byte)(255 << (8 - netMask)); // Bits tilovers sat til 1
                    netMask = 0;
                }
                else
                {
                    maskBytes[i] = 0; // Bits tilovers sat til 0
                }
            }

            return new IPAddress(maskBytes);
        }

        private IPAddress GetNetworkAddress(IPAddress ipAddress, IPAddress subnetMask)
        {
            var ipBytes = ipAddress.GetAddressBytes();
            var maskBytes = subnetMask.GetAddressBytes();
            var networkBytes = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                networkBytes[i] = (byte)(ipBytes[i] & maskBytes[i]);
            }

            return new IPAddress(networkBytes);
        }

        private IPAddress GetBroadcastAddress(IPAddress networkAddress, IPAddress subnetMask)
        {
            var networkBytes = networkAddress.GetAddressBytes();
            var maskBytes = subnetMask.GetAddressBytes();
            var broadcastBytes = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                broadcastBytes[i] = (byte)(networkBytes[i] | ~maskBytes[i]);
            }

            return new IPAddress(broadcastBytes);
        }

        private (IPAddress firstHost, IPAddress lastHost) GetHostRange(IPAddress networkAddress, IPAddress broadcastAddress)
        {
            var networkBytes = networkAddress.GetAddressBytes();
            var broadcastBytes = broadcastAddress.GetAddressBytes();

            networkBytes[3]++;
            broadcastBytes[3]--;

            return (new IPAddress(networkBytes), new IPAddress(broadcastBytes));
        }

    }
}
