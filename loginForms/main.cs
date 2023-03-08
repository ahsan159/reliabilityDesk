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
using System.Threading;

namespace loginForms
{
    public partial class main : Form
    {
        private Thread[] nThread;
        public main()
        {
            InitializeComponent();
            // set initial folders
            this.currentFolder = Environment.CurrentDirectory.ToString();
            this.appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString();
            this.datasetLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString();
            this.userFolder = Environment.GetFolderPath(Environment.SpecialFolder.AdminTools);
            this.currentUser = string.Empty;
            //MessageBox.Show(appDataFolder);
            //MessageBox.Show(userFolder);
            //MessageBox.Show(datasetLocation);
            //MessageBox.Show(DateTime.Now.Ticks.ToString()); 
            datasetName = "userData.xml"; // default database
            userTableName = "usersInfo"; // default table in database
            statusStripLabel.Text = "Please, provide valid username and password!!!";
            statusStrip.Refresh();

            // start 10 threads to handle 10 requests from child forms
            int maxThreads = 10;
            nThread = new Thread[maxThreads];
            for (int i = 0; i < maxThreads; i++)
            {
                nThread[i] = new Thread(listenToClients2);
                nThread[i].Start();
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            Environment.Exit(Environment.ExitCode); // this will end all the threads
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {            
            if (usernametxtbox.Text == "" || pwdtxtbox.Text == "")
            {
                // check if any or both fields are empty of not
                statusStripLabel.Text = "Please, provide valid username and password!!!";
                statusStrip.Refresh();
            }
            else
            {
                // read data set of users
                DataSet ds = new DataSet();
                ds.ReadXml(datasetName);
                DataTable userTable = ds.Tables[userTableName];
                // check user 
                if (userTable.AsEnumerable().Where(row => row.Field<string>("username") == usernametxtbox.Text).Count() > 0)
                {
                    DataRow dr = userTable.AsEnumerable().First(row => row.Field<string>("password") == pwdtxtbox.Text);
                    // check password
                    if (dr["password"].ToString() == pwdtxtbox.Text)
                    {
                        // update user variables
                        currentUser = dr["username"].ToString();
                        currentUserLevel = dr["level"].ToString();
                        statusStripLabel.Text = "Login Successfull '" + currentUser + "' as " + currentUserLevel;
                        loginBtn.Enabled = false;
                        statusStrip.Refresh();
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
                // click or enter in password field will login
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
            MessageBox.Show("Client Connected Connections left: " + NamedPipeServerStream.MaxAllowedServerInstances, "server");
            StreamWriter sw = new StreamWriter(pipe);
            sw.AutoFlush = true;
            StreamReader sr = new StreamReader(pipe);
            while (true)
            {
                string received = sr.ReadLine();
                MessageBox.Show(received);
                if (received.Equals("user"))
                {
                    sw.Write(received.Reverse());
                    sw.Flush();
                }
                if (received.Equals("done"))
                {
                    break;
                }
            }
            //pipe.Close();
            //pipe.Dispose();
            return;
        }

        private void listenToClients2()
        {
            // this will all called by thread
            // create pipe to listen to clients
            NamedPipeServerStream pipe1 = new NamedPipeServerStream("testpipe", PipeDirection.InOut, 10);
            //statusStripLabel.Text = "Waiting for connection";
            //statusStrip.Refresh();
            // wait for connection
            pipe1.WaitForConnection();
            if (!pipe1.IsConnected)
            {
                MessageBox.Show("Cannot Create User Server", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // writer stream
            StreamWriter sw = new StreamWriter(pipe1);
            sw.AutoFlush = true;
            // reader stream
            StreamReader sr = new StreamReader(pipe1);
            while (true)
            {
                try
                {
                    string received = sr.ReadLine();
                    // if client app has requested user details send level and user name
                    if (received.Equals("user???"))
                    {
                        sw.WriteLine("user," + this.currentUser + ",level," + this.currentUserLevel);
                        sw.Flush();
                    }
                    if (received.Equals("done")) 
                        // signal to exit when client has succesfully logged.
                    {
                        pipe1.Close();
                        pipe1.Dispose();
                        //MessageBox.Show("breaking", "server message2");
                        break;
                    }
                }
                catch (Exception exp)
                {
                    // exception while communication
                    MessageBox.Show(exp.Message + " " + Thread.CurrentThread.ManagedThreadId.ToString(), "Server Message");
                    break;
                }
            }
            // end thread
            Thread.CurrentThread.Abort();
            //MessageBox.Show("Aborting", "Login");
            //pipe.Close();
            //pipe.Dispose();
            return;
        }
    }
}
