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

namespace reliabilityEngine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    class blocks
    {
        private List<Button> rectList = new List<Button>();
        private int i = 0;
        private static blocks instance = null;
        public static blocks Instance()
        {
            if (instance == null)
            {
                instance = new blocks();
            }
            return instance;
        }
        public void AddRect()
        {
            i++;
        }
        public void AddRect(Button r)
        {
            i++;
            rectList.Add(r);
        }
        public List<Button> getBlocks()
        {
            return rectList;
        }
        public int id()
        {
            return i;
        }
        public int Count()
        {
            return rectList.Count;
        }

    }    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainTree.Items.Add("Item1");
            mainTree.Items.Add("Item2");
            mainTree.Items.Add("Item3");
            draw();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("This will add a Block");
            //Rectangle r = new Rectangle();            
            blocks b = blocks.Instance();
            Button r = new Button()
            {
                Name = "BTN_" + b.id().ToString(),
                Content = "Blocks" + b.id().ToString(),
                Width = 80,
                Height = 50,
                Margin = new Thickness(0, 10, 0, 0),
                Background = Brushes.Aqua
            };
            r.Click += R_Click;            
            //rbdPanel.Children.Add(r);
            b.AddRect(r);
            draw();
        }

        private void R_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            MessageBox.Show(b.Name);
            //throw new NotImplementedException();                        
        }
        private void draw()
        {
            rbdPanel.Children.Clear();
            Rectangle rStart = new Rectangle()
            {
                Width = 20,
                Height = 20,
                Margin = new Thickness(0, 5, 0, 0),
                Stroke = Brushes.Blue,
                Fill = Brushes.Blue
            };
            rbdPanel.Children.Add(rStart);
            //Line nLineStart = new Line()
            //{
            //    X1 = 0,
            //    Y1 = 0,
            //    X2 = 20,
            //    Y2 = 0,
            //    Margin = new Thickness(0, 25, 0, 0),
            //    Stroke = Brushes.Black
            //};
            //rbdPanel.Children.Add(nLineStart);

            blocks _blocks = blocks.Instance();
            List<Button> btn = _blocks.getBlocks();
            foreach(Button b in btn)
            {
                Line nLine1 = new Line()
                {
                    X1 = 0,
                    Y1 = 0,
                    X2 = 50,
                    Y2 = 0,
                    Margin = new Thickness(0, 25+5, 0, 0),
                    Stroke = Brushes.Black
                };
                rbdPanel.Children.Add(nLine1);

                rbdPanel.Children.Add(b);

                Line nLine2 = new Line()
                {
                    X1 = 0,
                    Y1 = 0,
                    X2 = 50,
                    Y2 = 0,
                    Margin = new Thickness(0, 25+5, 0, 0),
                    Stroke = Brushes.Black
                };
                rbdPanel.Children.Add(nLine2);
            }


            //Line nLineStop = new Line()
            //{
            //    X1 = 0,
            //    Y1 = 0,
            //    X2 = 20,
            //    Y2 = 0,
            //    Margin = new Thickness(0, 25, 0, 0),
            //    Stroke = Brushes.Black
            //};
            //rbdPanel.Children.Add(nLineStop);
            Rectangle rStop = new Rectangle()
            {
                Width = 20,
                Height = 20,
                Margin = new Thickness(0, 5, 0, 0),
                Stroke = Brushes.Red,
                Fill = Brushes.Red
            };
            rbdPanel.Children.Add(rStop);


        }
    }
}
