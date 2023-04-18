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
        Point startingPoint;
        Point endPoint;
        bool isMoving = false;
        bool rectDraw = false;
        bool lineDraw = false;
        bool draw = false;
        List<Rectangle> rectangleList;
        Graphics graphics;// = this.CreateGraphics();
        Rectangle currentRect;        
        public Form1()
        {
            InitializeComponent();
            graphics = this.CreateGraphics();
            graphics.Clear(Color.White);
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            if(!isMoving)
            {
                isMoving = true;
                startingPoint = e.Location;
            }
            
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
            //button2.Text = e.Data.GetData(DataFormats.Text).ToString();
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            if(isMoving)
            {
                button1.Top = button1.Top - startingPoint.Y + e.Location.Y;
                button1.Left = button1.Left - startingPoint.X + e.Location.X;
            }
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isMoving)
            {
                isMoving = false;
                startingPoint = Point.Empty;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //draw = true;            
            rectDraw = true;
            draw = false;
            currentRect = Rectangle.Empty;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (draw && rectDraw && currentRect != Rectangle.Empty)
            {
                //endPoint = e.Location;
                //currentRect = new Rectangle(Math.Min(startingPoint.X, endPoint.X), Math.Min(startingPoint.Y, endPoint.Y), Math.Abs(startingPoint.X - endPoint.X), Math.Abs(startingPoint.Y - endPoint.Y));
                Pen p = new Pen(Color.Red, 5);
                e.Graphics.DrawRectangle(p, currentRect);
                //graphics.DrawRectangle(p, currentRect);
                //Invalidate();
            }
            else if(draw && lineDraw)
            {
                Pen p = new Pen(Color.Green, 5);
                e.Graphics.DrawLine(p, startingPoint, endPoint);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left && rectDraw)
            {
                draw = true;
                startingPoint = e.Location;
            }
            if(e.Button == MouseButtons.Left && lineDraw)
            {
                draw = true;
                startingPoint = e.Location;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left && draw && rectDraw)
            {
                endPoint = e.Location;
                draw = false;
                rectDraw = false;
                currentRect = new Rectangle(Math.Min(startingPoint.X,endPoint.X),Math.Min(startingPoint.Y,endPoint.Y),Math.Abs(startingPoint.X-endPoint.X),Math.Abs(startingPoint.Y-endPoint.Y));
                Pen p = new Pen(Color.Red, 5);                
                graphics.DrawRectangle(p, currentRect);
                currentRect = Rectangle.Empty;
                //MessageBox.Show("Drawing");
            }
            else if (e.Button == MouseButtons.Left && draw && lineDraw)
            {
                draw = false;
                lineDraw = false;
                Pen p = new Pen(Color.Green, 5);
                graphics.DrawLine(p, startingPoint, endPoint);                
            }
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if(draw && rectDraw)
            {
                endPoint = e.Location;
                currentRect = new Rectangle(Math.Min(startingPoint.X, endPoint.X), Math.Min(startingPoint.Y, endPoint.Y), Math.Abs(startingPoint.X - endPoint.X), Math.Abs(startingPoint.Y - endPoint.Y));
                Invalidate();
            }
            else if (draw && lineDraw)
            {
                endPoint = e.Location;
                Invalidate();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rectangleList = new List<Rectangle>();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lineDraw = true;
            draw = false;
        }

    }
}
