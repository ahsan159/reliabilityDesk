using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for RBDResults.xaml
    /// </summary>
    public partial class RBDResults : Window
    {

        #region Members
        public DataTable ResultsTable;
        #endregion
        #region constructors
        public RBDResults()
        {
            InitializeComponent();
        }
        /// <summary>
        /// only this constructor will be used
        /// </summary>
        /// <param name="resultsTable"></param>
        public RBDResults(DataTable resultsTable)
        {
            InitializeComponent();
            ResultsTable = resultsTable;            
            dataTableDisplay.DataContext = resultsTable.DefaultView;
        }
        #endregion

        #region button functions
        /// <summary>
        /// close the results window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        } 
        #endregion
    }
}
