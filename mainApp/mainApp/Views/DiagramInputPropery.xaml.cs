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
        public string UnitName;
        public string[] Items = {"Series", "Parallel"};
        public string SelectedValue = "";
        public string Quantity;
        public string QuantityRequired;
        public DiagramInputPropery()
        {
            InitializeComponent();
        }
        public DiagramInputPropery(string _name)
        {
            InitializeComponent();
            UnitName = _name;
            this.Title = "Properties: " + UnitName; 
        }
    }
}
