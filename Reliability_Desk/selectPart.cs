﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TNXMLUtility;
using System.IO;
using System.IO.Pipes;

namespace Reliability_Desk
{
    public partial class selectPart : Form
    {
        public selectPart()
        {
            InitializeComponent();
            // basic initialize size of tablelayout to display list only
            statusLabel.Text = "Select a file to load!!!";
            statusStrip.Refresh();
            selectedPart = null;
            partTable.AllowUserToAddRows = false;
            indPartDataTable.AllowUserToAddRows = false;
            partList = new List<part>();
            try
            {
                cDataset = new DataTable();
                foreach (string field in globals.dataFields)
                {
                    cDataset.Columns.Add(field);
                }
                partTable.DataSource = cDataset;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            tableLayoutPanel1.ColumnStyles[1].SizeType = SizeType.Percent;
            tableLayoutPanel1.ColumnStyles[1].Width = 0;
            tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Percent;
            tableLayoutPanel1.ColumnStyles[0].Width = 100;

        }
        public selectPart(string fn)
        {
            InitializeComponent();
            // Initialize form with default part list to be loaded to form
            partTable.AllowUserToAddRows = false;
            indPartDataTable.AllowUserToAddRows = false;
            if (File.Exists(fn)) // file validation check
            {
                partTable.DataSource = null;
                fileName = Path.GetFullPath(fn);
                try
                {
                    partList = TNXMLUtility.nodeUtilities.readPartsfromXML(fileName); // load partList from file
                    if (partList.Count == 0) //file data validation
                    {
                        throw new FileLoadException("Incorrect File provided", fileName);
                    }
                    // adding columns
                    cDataset = new DataTable();
                    foreach (string field in globals.dataFields)
                    {
                        cDataset.Columns.Add(field);
                    }
                    foreach (part p in partList)
                    {
                        // add paths 
                        p.setPath(fileName);
                        cDataset.Rows.Add(p.getData());
                    }
                    //cDataset.WriteXml("partListNew.xml");
                    partTable.DataSource = cDataset;
                    statusLabel.Text = "File load Successfull, \'" + partList.Count + "\' parts loaded from \'" + fileName + "\'";
                    statusStrip.Refresh();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString(), "Stop", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    statusLabel.Text = "Select a valid file to load!!!";
                    statusStrip.Refresh();
                }
            }
            else
            {
                MessageBox.Show("File Not Found: " + fn, "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                statusLabel.Text = "Select a valid file to load!!!";
                statusStrip.Refresh();
            }
            //MessageBox.Show("Making Pipe");
            //NamedPipeClientStream clientPipe = new NamedPipeClientStream(".", "testpipe", PipeDirection.InOut);
            ////MessageBox.Show("Made Pipe");
            //try
            //{
            //    clientPipe.Connect();
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.ToString());
            //}
            ////MessageBox.Show("Reliability Desk: Pipe Connected");
            //statusLabel.Text = "Searching for available users";
            //statusStrip.Refresh();
            //StreamReader sr = new StreamReader(clientPipe);
            //StreamWriter sw = new StreamWriter(clientPipe);
            ////MessageBox.Show("Sending data");
            //string currentUser = "";
            //string currentUserLevel = "";
            //while (currentUser == "")
            //{
            //    sw.WriteLine("user???");
            //    sw.Flush();
            //    string s = sr.ReadLine();
            //    string[] str = s.Split(',');
            //    currentUser = str[1].Trim();
            //    currentUserLevel = str[3].Trim();
            //    //MessageBox.Show(s, "Client");
            //    loginLabel.Text = currentUser;
            //    statusLabel.Text = currentUser + " logged in as " + currentUserLevel;
            //    statusStrip.Refresh();
            //}
            //sw.WriteLine("done");
            //sw.Flush();
            //clientPipe.Close();
            //clientPipe.Dispose();
            // set parts list display only            
            tableLayoutPanel1.ColumnStyles[1].SizeType = SizeType.Percent;
            tableLayoutPanel1.ColumnStyles[1].Width = 0;
            tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Percent;
            tableLayoutPanel1.ColumnStyles[0].Width = 100;

        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedPart = null;
            selectedPart = null;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            selectedPart = null;
            selectedPart = null;
            this.Close();
        }

        private void btnBlank_Click(object sender, EventArgs e)
        {
            // not used function 
            selectedPart = new part();
        }

        private void partTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // return this double clicked part as selected part to main reliability desk or anyother application
            try
            {
                if (partList.Count == 0) // part list validation
                {
                    MessageBox.Show("No Active Part Lists Loaded", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //selected row data extraction and searching part from partlist to select and return
                DataGridViewRow selectedRow = partTable.Rows[e.RowIndex];
                string name = selectedRow.Cells[globals.name].Value.ToString();
                string cmID = selectedRow.Cells[globals.cmID].Value.ToString();
                string mftr = selectedRow.Cells[globals.mftr].Value.ToString();
                string desc = selectedRow.Cells[globals.desc].Value.ToString();                
                //MessageBox.Show(name + mftr + cmID + desc);
                int selectedIndex = partList.FindIndex(x =>
                    x.getData()[0].Equals(name) && // predicate 1 to search by name
                    x.getData()[1].Equals(cmID)    // predicate 2 to search by cmID
                    );
                //MessageBox.Show(partList.FindIndex(x => x.getData()[0].Equals(name) && x.getData()[1].Equals(cmID)).ToString());
                // select part and close
                selectedPart = partList.ElementAt<part>(selectedIndex);
                this.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void savePartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Part List File (*.prtl)|*.prj|XML File (*.xml)|*.xml|All Files (*.*)|*.*";
            dlg.FilterIndex = 2;
            dlg.RestoreDirectory = true;
            dlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                TNXMLUtility.TNXMLUtility.writePartListXML(dlg.FileName, partList);
            }
        }

        private void loadPartListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // load new part list file
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Part List File (*.prtl)|*.prj|XML File (*.xml)|*.xml|All Files (*.*)|*.*";
            dlg.FilterIndex = 2;
            dlg.RestoreDirectory = true;
            dlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                partTable.DataSource = null;
                fileName = dlg.FileName;
                //fileName = "../../partListTest1.xml";
                fileName = Path.GetFullPath(fileName);
                //MessageBox.Show(fileName);
                try
                {
                    partList = TNXMLUtility.nodeUtilities.readPartsfromXML(fileName);
                    if (partList.Count == 0)
                    {
                        throw new FileLoadException("Incorrect File provided", fileName);
                    }
                    cDataset = new DataTable();
                    // this list must be same everywhere should get by some static class field
                    foreach (string field in globals.dataFields)
                    {
                        cDataset.Columns.Add(field);
                    }
                    foreach (part p in partList)
                    {
                        p.setPath(fileName);
                        cDataset.Rows.Add(p.getData());
                    }
                    partTable.DataSource = cDataset;
                    statusLabel.Text = "File load Successfull, \'" + partList.Count + "\' parts loaded from \'" + fileName + "\'";
                    statusStrip.Refresh();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString(), "Stop", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    statusLabel.Text = "Select a valid file to load!!!";
                    statusStrip.Refresh();
                }
                //partTable.ColumnCount = 6;            
                //partTable.RowCount = pl.Count;
            }
            dlg.Dispose();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripBtnAdd_Click(object sender, EventArgs e)
        {
            // Add new part data update screen
            tableLayoutPanel1.ColumnStyles[1].SizeType = SizeType.Percent;
            tableLayoutPanel1.ColumnStyles[1].Width = 50;
            tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Percent;
            tableLayoutPanel1.ColumnStyles[0].Width = 50;
            // populate individual part data in individual part form
            indPartDataTable.Columns.Clear();
            if (indPartDataTable.Columns.Count == 0)
            {
                indPartDataTable.Columns.Add("Field", "Field");
                indPartDataTable.Columns.Add("Value", "Value");
                foreach(string field in globals.dataFields)
                {
                    indPartDataTable.Rows.Add(field);
                }
            }
        }

        private void toolStripButtonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // search part data by name and refresh table
                string filterExp = toolStripTextSearch.Text;
                DataRow[] rows = cDataset.Select("Name LIKE '%" + filterExp + "%'"); // query to search in Name column           
                DataTable dt = rows.CopyToDataTable();
                partTable.DataSource = dt;
                partTable.Refresh();
                statusLabel.Text = "Found '" + rows.Count().ToString() + "' from search term";
                statusStrip.Refresh();
            }
            catch (Exception exp)
            {

            }

        }

        private void toolStripTextSearch_Click(object sender, EventArgs e)
        {

        }

        private void partTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // display clicked row part in half of the screen
            tableLayoutPanel1.ColumnStyles[1].SizeType = SizeType.Percent;
            tableLayoutPanel1.ColumnStyles[1].Width = 50;
            tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Percent;
            tableLayoutPanel1.ColumnStyles[0].Width = 50;
            string name = "";
            string cmID = "";
            string mftr = "";
            string desc = "";
            string cat = "";
            string scat = "";
            string pack = "";
            string grade = "";
            string temp = "";
            string mtbf = "";
            string rad = "";
            string outg = "";
            try
            {
                if (partList.Count == 0)
                {
                    MessageBox.Show("No Active Part Lists Loaded", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                DataGridViewRow selectedRow = partTable.Rows[e.RowIndex];
                name = selectedRow.Cells[globals.name].Value.ToString();
                cmID = selectedRow.Cells[globals.cmID].Value.ToString();
                mftr = selectedRow.Cells[globals.mftr].Value.ToString();
                desc = selectedRow.Cells[globals.desc].Value.ToString();
                cat = selectedRow.Cells[globals.cat].Value.ToString();
                scat = selectedRow.Cells[globals.scat].Value.ToString();
                pack = selectedRow.Cells[globals.pack].Value.ToString();
                grade = selectedRow.Cells[globals.grade].Value.ToString();
                temp = selectedRow.Cells[globals.temp].Value.ToString();
                mtbf = selectedRow.Cells[globals.mtbf].Value.ToString();
                rad = selectedRow.Cells[globals.rad].Value.ToString();
                outg = selectedRow.Cells[globals.outg].Value.ToString();
                //MessageBox.Show(name + mftr + cmID + desc);
                int selectedIndex = partList.FindIndex(x =>
                    x.getData()[0].Equals(name) && // predicate 1 to search by name
                    x.getData()[1].Equals(cmID)    // predicate 2 to search by cmID
                    );
                //MessageBox.Show(partList.FindIndex(x => x.getData()[0].Equals(name) && x.getData()[1].Equals(cmID)).ToString());
                //selectedPart = partList.ElementAt<part>(selectedIndex);
                //this.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }

            // set part data after previous validation
            if (indPartDataTable.Columns.Count == 0)
            {
                indPartDataTable.Columns.Add("Field", "Field");
                indPartDataTable.Columns.Add("Value", "Value");
                foreach(string field in globals.dataFields)
                {
                    indPartDataTable.Rows.Add(field);
                }                
            }
            indPartDataTable.Rows[0].Cells[1].Value = name;
            indPartDataTable.Rows[1].Cells[1].Value = cmID;
            indPartDataTable.Rows[2].Cells[1].Value = mftr;
            indPartDataTable.Rows[3].Cells[1].Value = desc;
            indPartDataTable.Rows[4].Cells[1].Value = cat;
            indPartDataTable.Rows[5].Cells[1].Value = scat;
            indPartDataTable.Rows[6].Cells[1].Value = pack;
            indPartDataTable.Rows[7].Cells[1].Value = grade;
            indPartDataTable.Rows[8].Cells[1].Value = temp;
            indPartDataTable.Rows[9].Cells[1].Value = mtbf;
            indPartDataTable.Rows[10].Cells[1].Value = rad;
            indPartDataTable.Rows[11].Cells[1].Value = outg;
            

        }

        private void closePart_Click_1(object sender, EventArgs e)
        {
            // close form clear all screen for part list
            tableLayoutPanel1.ColumnStyles[1].SizeType = SizeType.Percent;
            tableLayoutPanel1.ColumnStyles[1].Width = 0;
            tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Percent;
            tableLayoutPanel1.ColumnStyles[0].Width = 100;
        }

        private void addPart_Click(object sender, EventArgs e)
        {

        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            // clear part data for data entry
            indPartDataTable.Columns.Clear();
            if (indPartDataTable.Columns.Count == 0)
            {
                indPartDataTable.Columns.Add("Field", "Field");
                indPartDataTable.Columns.Add("Value", "Value");
                foreach (string field in globals.dataFields)
                {
                    indPartDataTable.Rows.Add(field);                        
                }
            }
        }

        private void addPart_Click_1(object sender, EventArgs e)
        {
            // add updated part data to data set and update table
            tableLayoutPanel1.ColumnStyles[1].SizeType = SizeType.Percent;
            tableLayoutPanel1.ColumnStyles[1].Width = 0;
            tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Percent;
            tableLayoutPanel1.ColumnStyles[0].Width = 100;
            string name = "";
            string cmID = "";
            string mftr = "";
            string desc = "";
            string cat = "";
            string scat = "";
            // retried part data
            name = indPartDataTable["Value", 0].Value.ToString();
            mftr = indPartDataTable["Value", 2].Value.ToString();
            desc = indPartDataTable["Value", 5].Value.ToString();
            cmID = indPartDataTable["Value", 1].Value.ToString();
            scat = indPartDataTable["Value", 3].Value.ToString();
            cat = indPartDataTable["Value", 4].Value.ToString();
            // create new part
            part newpart = new part(cat, scat, name, mftr, desc);
            newpart.setcmID(cmID);
            // add new part to part list
            partList.Add(newpart);
            // add new part to datasource
            cDataset.Rows.Add(newpart.getData());
            // update
            partTable.DataSource = cDataset;
            partTable.Refresh();
            // update label
            statusLabel.Text = "New part '" + name + "' added";
            statusStrip.Refresh();

        }

        private void toolStripBtnSave_Click(object sender, EventArgs e)
        {
            // save file as xml 
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Part List File (*.prtl)|*.prj|XML File (*.xml)|*.xml|All Files (*.*)|*.*";
            dlg.FilterIndex = 2;
            dlg.RestoreDirectory = true;
            dlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                TNXMLUtility.TNXMLUtility.writePartListXML(dlg.FileName, partList);
            }
        }
    }
}
