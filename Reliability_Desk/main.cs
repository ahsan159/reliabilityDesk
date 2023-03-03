using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Pipes;

namespace loginForms
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
            //this.appDataFolder = string.Empty;
            this.currentFolder = Environment.CurrentDirectory.ToString();
            this.appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString();
            this.datasetLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString();
            this.userFolder = Environment.GetFolderPath(Environment.SpecialFolder.AdminTools);
            this.currentUser = string.Empty;
            //MessageBox.Show(appDataFolder);
            //MessageBox.Show(userFolder);
            //MessageBox.Show(datasetLocation);
            //MessageBox.Show(DateTime.Now.Ticks.ToString()); 
            datasetName = "userData.xml";
            userTableName = "usersInfo";
            //DataSet ds = new DataSet("reliabilityDeskUsers");
            //DataTable dt = new DataTable(userTableName);
            //dt.Columns.Add("username",Type.GetType("System.String"));
            //dt.Columns.Add("password",Type.GetType("System.String"));
            //dt.Columns.Add("level", Type.GetType("System.String"));
            //dt.Rows.Add("ahsan", "ahsan","admin");
            //dt.Rows.Add("ubaid", "ubaid","admin");
            //dt.Rows.Add("hammad", "hammad", "user");
            //ds.Tables.Add(dt);
            //if (File.Exists(datasetName))
            //{
            //    File.Delete(datasetName);
            //}
            //ds.WriteXml(datasetName);
            statusStripLabel.Text = "Please, provide valid username and password!!!";
            statusStrip.Refresh();
            //Action act = new Action(this.listenToClients);
            //Task task = new Task(act);
            //task.Start();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            if(usernametxtbox.Text=="" || pwdtxtbox.Text=="")
            {
                //DataSet1.loginTableDataTable         
                statusStripLabel.Text = "Please, provide valid username and password!!!";
                statusStrip.Refresh();
            }
            else
            {
                DataSet ds = new DataSet();
                ds.ReadXml(datasetName);
                DataTable userTable = ds.Tables[userTableName];
                //MessageBox.Show(userTable.Rows.Count.ToString());
                if(userTable.AsEnumerable().Where(row => row.Field<string>("username")==usernametxtbox.Text).Count() > 0)
                {
                    DataRow dr = userTable.AsEnumerable().First(row => row.Field<string>("password") == pwdtxtbox.Text);
                    //MessageBox.Show(dr["password"].ToString());
                    if(dr["password"].ToString()==pwdtxtbox.Text)
                    {
                        currentUser = dr["username"].ToString();
                        currentUserLevel = dr["level"].ToString();
                        statusStripLabel.Text = "Login Successfull '" + currentUser + "' as " + currentUserLevel;
                        statusStrip.Refresh();
                        Action action = new Action(listenToClients);
                        Task task = new Task(action);
                        task.Start();
                    }
                    else
                    {
                        statusStripLabel.Text = "Password Incorrect";
                        statusStrip.Refresh();
                    }
                }
                else
                {
                    statusStripLabel.Text = "username not found";
                    statusStrip.Refresh();
                }                
                
            }
        }

        private void pwdtxtbox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void pwdtxtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                //MessageBox.Show(usernametxtbox.Text + "," + pwdtxtbox.Text);
                loginBtn_Click(sender, e);
            }
        }
        private void listenToClients()
        {            
            pipe = new NamedPipeServerStream("testpipe", PipeDirection.InOut);            
            //statusStripLabel.Text = "Waiting for connection";
            //statusStrip.Refresh();
            pipe.WaitForConnection();
            if (!pipe.IsConnected)
            {
                MessageBox.Show("Cannot Create User Server", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Client Connected", "info");
            StreamWriter sw = new StreamWriter(pipe);
            sw.AutoFlush = true;
            StreamReader sr = new StreamReader(pipe);
            while(true)
            {
                string received = sr.ReadLine();
                MessageBox.Show("login: " + received);
                sw.Write(received.Reverse());
                sw.Flush();
                if (received.Equals("done"))
                {
                    break;                    
                }
            }
            return;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
