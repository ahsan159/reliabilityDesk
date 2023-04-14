using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RBDEngine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("d");
            button1.DoDragDrop(button1.Text, DragDropEffects.Copy | DragDropEffects.Move);
            button2.AllowDrop = true;
            
        }

        private void button2_DragEnter(object sender, DragEventArgs e)
        {
            //if(e.Data.GetDataPresent(DataFormats.Text))
            //{
            //    e.Effect = DragDropEffects.Copy;
            //}
            //else
            //{
            //    e.Effect = DragDropEffects.None;
            //}
            //MessageBox.Show("p");
        }

        private void button2_DragDrop(object sender, DragEventArgs e)
        {
            button2.Text = e.Data.GetData(DataFormats.Text).ToString();
        }
    }
}
