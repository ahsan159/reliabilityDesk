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

namespace mainApp.Views
{
    /// <summary>
    /// Interaction logic for DiagramInputPropery.xaml
    /// </summary>
    public partial class DiagramInputPropery : Window
    {
        public string UnitName = "Input";
        public string[] Items = { "Series", "Parallel" };
        public string SelectedValue = "";
        public string Quantity = "0";
        public string QuantityRequired = "0";
        public string ConfigurationString = "";
        public bool result = false;
        public DiagramInputPropery()
        {
            InitializeComponent();
        }
        public DiagramInputPropery(string _name)
        {
            InitializeComponent();
            UnitName = _name;
            this.Title = "Properties: " + UnitName;
            comboselect.ItemsSource = Items;
        }

        private void comboselect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboselect.SelectedIndex == 0)
            {
                qTotal.IsEnabled = false;
                qRequired.IsEnabled = false;
            }
            else if (comboselect.SelectedIndex == 1)
            {
                qTotal.IsEnabled = true;
                qRequired.IsEnabled = true;
            }
        }
        /// <summary>
        /// analyze data if OK is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKClick_Click(object sender, RoutedEventArgs e)
        {
            if (comboselect.SelectedIndex == 0)
            {
                // if selected index in series
                ConfigurationString = Items[comboselect.SelectedIndex];
                //MessageBox.Show(ConfigurationString);
                result = true;
                this.Close();
            }
            else if (comboselect.SelectedIndex == 1)
            {
                // if selected index in parallel
                int qT;
                int qR;
                bool TResult = int.TryParse(qTotal.Text, out qT);
                bool RResult = int.TryParse(qRequired.Text, out qR);
                if (TResult & RResult)
                {

                    ConfigurationString = Items[comboselect.SelectedIndex] + " " + qT.ToString() + ":" + qR.ToString();
                }
                else
                {
                    // return warning if input is invalid
                    MessageBox.Show("Warning: Invalid Configuration", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                //MessageBox.Show(ConfigurationString);
                result = true;
                this.Close();
            }
            else
            {
                // return warning if nothing is selected
                MessageBox.Show("Warning: Invalid Configuration", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        /// <summary>
        /// return nothing if cancel is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelClick_Click(object sender, RoutedEventArgs e)
        {
            // return nothing if cancel is selected
            result = false;
            this.Close();
        }
    }
}
