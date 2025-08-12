using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
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
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;

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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //WordDocument doc = new WordDocument();
            //WSection section = doc.AddSection() as WSection;
            //section.PageSetup.Margins.All = 72;
            //section.PageSetup.PageSize = PageSize.A4;

            //WParagraph style = doc.AddParagraphStyle("Normal") as WParagraphStyle;
            ////style.CharacterFormat.FontName = "Calibri";
            ////style.CharacterFormat.FontSize = 11f;
            //style.ParagraphFormat.BeforeSpacing = 0;
            //style.ParagraphFormat.AfterSpacing = 8;
            //style.ParagraphFormat.LineSpacing = 13.8f;

            //style = doc.AddParagraphStyle("Heading 1") as WParagraphStyle;
            ////style.ApplyBaseStyle("Normal");
            ////style.CharacterFormat.FontName = "Calibri Light";
            ////style.CharacterFormat.FontSize = 16f;
            ////style.CharacterFormat.TextColor = Color.FromArgb(46, 116, 181);
            //style.ParagraphFormat.BeforeSpacing = 12;
            //style.ParagraphFormat.AfterSpacing = 0;
            //style.ParagraphFormat.Keep = true;
            //style.ParagraphFormat.KeepFollow = true;
            //style.ParagraphFormat.OutlineLevel = OutlineLevel.Level1;
        }
    }
}
