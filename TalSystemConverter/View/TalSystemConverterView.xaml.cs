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

namespace TalSystemConverter.View
{
    /// <summary>
    /// Interaction logic for TalSystemConverterView.xaml
    /// </summary>
    public partial class TalSystemConverterView : UserControl
    {
        public TalSystemConverterView()
        {
            InitializeComponent();
        }
        private void ConvertDecimalButton_Click(object sender, RoutedEventArgs e)
        {
            string input = DecimalInputOutput.Text.Trim();

            if (int.TryParse(input, out int decimalNumber))
            {
                // Decimal to Binary
                string binaryResult = Convert.ToString(decimalNumber, 2);
                BinaryInputOutput.Text = binaryResult;

                // Decimal to Hexadecimal
                string hexResult = Convert.ToString(decimalNumber, 16).ToUpper();
                HexInputOutput.Text = hexResult;

                // Decimal Output
                DecimalInputOutput.Text = decimalNumber.ToString();
            }
            else
            {
                MessageBox.Show("Skriv et validt decimal tal.");
            }
        }

        private void ConvertBinaryButton_Click(object sender, RoutedEventArgs e)
        {
            string input = BinaryInputOutput.Text.Trim();

            if (IsBinary(input))
            {
                try
                {
                    int decimalResult = Convert.ToInt32(input, 2);

                    // Binary to Decimal
                    DecimalInputOutput.Text = decimalResult.ToString();

                    // Binary to Hexadecimal
                    string hexResult = Convert.ToString(decimalResult, 16).ToUpper();
                    HexInputOutput.Text = hexResult;

                    // Binary Output
                    BinaryInputOutput.Text = input;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Problem med at convert: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Skriv et validt binær tal.");
            }
        }

        private void ConvertHexButton_Click(object sender, RoutedEventArgs e)
        {
            string input = HexInputOutput.Text.Trim();

            if (IsHexadecimal(input))
            {
                try
                {
                    int decimalResult = Convert.ToInt32(input, 16);

                    // Hexadecimal to Decimal
                    DecimalInputOutput.Text = decimalResult.ToString();

                    // Hexadecimal to Binary
                    string binaryResult = Convert.ToString(decimalResult, 2);
                    BinaryInputOutput.Text = binaryResult;

                    // Hexadecimal Output
                    HexInputOutput.Text = input.ToUpper();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Problem med at convert: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Skriv et validt hexadecimal tal.");
            }
        }

        private bool IsBinary(string input)
        {
            foreach (char c in input)
            {
                if (c != '0' && c != '1')
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsHexadecimal(string input)
        {
            foreach (char c in input)
            {
                if (!((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f')))
                {
                    return false;
                }
            }
            return true;
        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            DecimalInputOutput.Clear();
            BinaryInputOutput.Clear();
            HexInputOutput.Clear();
        }
    }

}

