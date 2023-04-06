using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reliability_Desk
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ////Application.Run(new Form1());
            //selectPart frm = new selectPart("../../partListTest1.xml");
            //selectPart frm = new selectPart();
            relDesk frm = new relDesk();
            Application.Run(frm);
            ////MessageBox.Show(frm.selectedPart.ToString());
            //frm.Dispose();
            //Action act1 = new Action(go);
            //var t1 = new Task(act1);
            //t1.Start();
            //t1.Wait();
            //Action act2 = new Action(go1);
            //var t2 = new Task(act2);
            //t2.Start();
            //t1.Wait();
            //Task.WaitAll(t1, t2);
            //t1.Dispose();
            //MessageBox.Show("Done");
            
        }
        static void go()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            selectPart frm = new selectPart("../../partListTest1.xml");
            //selectPart frm = new selectPart();
            //relDesk frm = new relDesk();
            Application.Run(frm);
            //MessageBox.Show(frm.selectedPart.ToString());
            frm.Dispose();
        }
        static void go1()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new main());
            //selectPart frm = new selectPart("../../partListTest1.xml");
            //selectPart frm = new selectPart();
            //Application.Run(frm);
            //MessageBox.Show(frm.selectedPart.ToString());
            //frm.Dispose();
        }
    }
}
