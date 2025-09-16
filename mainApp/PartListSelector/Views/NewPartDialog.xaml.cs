using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace PartListSelector.Views
{
    /// <summary>
    /// Interaction logic for NewPartDialog.xaml
    /// </summary>
    public partial class NewPartDialog : Window
    {
        public string[] category =
        {
            "CAPACITORS",
            "CONNECTORS",
            "PIEZO-ELECTRIC DEVICES",
            "DIODES",
            "FILTERS",
            "FUSES",
            "INDUCTORS",
            "MICROCIRCUITS",
            "RELAYS",
            "RESISTORS",
            "THERMISTORS",
            "TRANSISTORS",
            "WIRES AND CABLES",
            "TRANSFORMER",
            "SWITCHES",
            "OPTO ELECTRONICS",
            "THYRISTORS",
            "THERMOSTAT",
            "LAMP",
            "FIBEROPTIC COMPONENTS",
            "RF PASSIVE COMPONENTS",
            "BATTERY",
            "PYROTECHNICAL DEVICES",
            "HYBRIDS",
            "MISCELLANEOUS PARTS"
        };

        public string[] subcategory =
        {
            "MICA",
            "ALUMINUM SOLID",
            "TANTALUM NON-SOLID",
            "CERAMIC",
            "GLASS",
            "SEMICONDUCTOR",
            "PLASTIC METALLIZED",
            "FEEDTHROUGH",
            "TANTALUM SOLID",
            "CERAMIC CHIP",
            "RACK AND PANEL",
            "MICROMINIATURE",
            "GLASSFIBRE",
            "RECTANGULAR",
            "PRINTED CIRCUIT BOARD",
            "RF FILTER",
            "CIRCULAR",
            "RF COAXIAL",
            "D-TYPE",
            "CRYSTAL RESONATOR",
            "TUNNEL",
            "RECTIFIER",
            "TRANSIENT SUPPRESSION",
            "PIN",
            "HIGH VOLTAGE RECTIFIER",
            "RF/MICROWAVE SCHOTTKY (Si)",
            "MICROWAVE VRACTOR (GaAs)",
            "RF/MICROWAVE VRACTOR (Si)",
            "RF/MICROWAVE PIN",
            "STEP RECOVERY",
            "MICROWAVE SCHOTTKY (GaAs)",
            "ZENER DIODE",
            "CURRENT REGULATOR",
            "VOLTAGE REGULATOR",
            "HOT CARRIER",
            "MICROWAVE GUNN (GaAs)",
            "SWITCHING",
            "FEEDTHROUGH FILTER",
            "DIPLEXERS",
            "ALL",
            "CORES",
            "CHIP",
            "RF COIL",
            "LINEAR OPERATIONAL AMPLIFIER",
            "LINEAR VOLTAGE COMPARATOR",
            "LINEAR OTHER FUNCTIONS",
            "MICROWAVE MONOLITHIC INTEGRATED CIRCUITS (MMIC)",
            "MEMORY PROM",
            "MEMORY OTHER",
            "MEMORY EEPROM",
            "LOGIC FAMILIES",
            "MEMORY DRAM",
            "MEMORY EPROM",
            "LINEAR SAMPLE AND HOLD AMPLIFER",
            "LINEAR MULTIPLEXER/DEMULTIPLEXER",
            "ASIC TECHNOLOGIES DIGITAL",
            "LINEAR LINE RECEIVER",
            "LINEAR TIMMER",
            "LINEAR SWITCHES",
            "LINEAR LINE DRIVER",
            "LINEAR DAC",
            "ASIC TECHNOLOGIES LINEAR",
            "OTHER FUNCTIONS",
            "ASIC TECHNOLOGIES MIXED ANALOG/DIGITAL",
            "PROGRAMMABLE LOGIC",
            "LINEAR ADC",
            "LINEAR SWITCHING REGULATOR",
            "LINEAR MULTIPLIER",
            "MEMORY SRAM",
            "MICROPROCESSOR/MICROCONTROLLER/PERIPHERAL",
            "LINEAR VOLTAGE REGULATOR",
            "VOLTAGE REFERENCE",
            "LINEAR INSTRUMENTATION AMPLIFIER",
            "LINEAR LINE TRANSCEIVER",
            "LATCHING",
            "NON LATCHING",
            "WIREWOUND CHASSIS MOUNTED",
            "METAL OXIDE",
            "NETWORK (ALL)",
            "VARIABLE (TRIMMERS)",
            "METAL FILM",
            "SHUNT",
            "COMPOSITION",
            "CHIP (ALL)",
            "HEATER/FLEXIBLE",
            "WIREWOUND PRECISION (INCLUDING SURFACE MOUNT)",
            "TEMPERATURE COMPENSATING",
            "TEMPERATURE SENSOR",
            "TEMPERATURE MEASURING",
            "SWITCHING TRANSISTOR",
            "MICROWAVE POWER (GaAs)",
            "MULTIPLE",
            "RF/MICROWAVE FET N-CHANNEL/P-CHANNEL",
            "MICROWAVE LOW NOISE (GaAs)",
            "LOW POWER PNP (<2WATTS)",
            "RF/MICROWAVE NPN LOW POWER/LOW NOISE",
            "RF/MICROWAVE FET POWER (Si)",
            "CHOPPER",
            "FET N-CHANNEL",
            "RF/MICROWAVE PNP LOW POWER/LOW NOISE",
            "FET P-CHANNEL",
            "RF/MICROWAVE BIPLOAR POWER",
            "HIGH POWER PNP (>2WATTS)",
            "HIGH POWER NPN (>2WATTS)",
            "LOW POWER NPN (<2WATTS)",
            "COAXIAL",
            "LOW FREQUENCY",
            "FIBRE OPTIC",
            "SIGNAL ",
            "POWER",
            "CIRCUIT BREAKER",
            "RF-SWITCH",
            "REED SWITCH",
            "STANDARD DC/AC POWER TOGGLE",
            "MICROSWITCH",
            "LCD DISPLAY/SCREEN",
            "PHOTTRANSISTOR",
            "PHOTODIODE/SENSOR",
            "LASER DIODE",
            "LED",
            "CHARGE COUPLED DEVICE (CCD)",
            "OPTOCOUPLER",
            "ISOLATOR",
            "FIBER/CABLE",
            "CONNECTOR",
            "SWITCH ",
            "POWER DIVIDERS",
            "ATTENUATORS/LOADS",
            "ISOLATOR/CIRCULATOR",
            "WAVEGUIDE COMPONENTS",
            "COUPLERS",
            "CUTTERS",
            "INITIATORS",
            "CRYSTAL OSCILLATORS",
            "THICK FILM",
            "SCHOTTKY",
            "THIN FILM ",
            "MISC",
            "Carbon Film"
        };

        public string SelectedCategory = "";
        public string SelectedSubCategory = "";
        public string NameTxt = "";
        public string Manufacturer = "";
        public string Description = "";
        public NewPartDialog()
        {
            InitializeComponent();
            CategoryNP.ItemsSource = category as IEnumerable<string>;
            SubCategoryNP.ItemsSource = subcategory as IEnumerable<string>;
        }

        /// <summary>
        /// when OK will be clicked
        /// required data will be retrived from UI Controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //string data = "Ahsan";
            SelectedCategory = category[CategoryNP.SelectedIndex];
            SelectedSubCategory = subcategory[SubCategoryNP.SelectedIndex];
            TextRange n;
            n = new TextRange(NameText.Document.ContentStart, NameText.Document.ContentEnd);
            NameTxt = n.Text.Trim();
            n = new TextRange(ManufacturerText.Document.ContentStart, ManufacturerText.Document.ContentEnd);
            Manufacturer = n.Text.Trim();
            n = new TextRange(DescriptionText.Document.ContentStart, DescriptionText.Document.ContentEnd);
            Description = n.Text.Trim();
            this.Close();
        }

        /// <summary>
        /// when cancel will be clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
