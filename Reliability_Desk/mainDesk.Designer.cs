using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Xml;
using System.IO;
using System.Xml.Linq;

namespace Reliability_Desk
{
    partial class relDesk
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(relDesk));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statuslabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.loadPartlistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListProjectTree = new System.Windows.Forms.ImageList(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeNodeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewPartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.saveProperties = new System.Windows.Forms.Button();
            this.closeProperties = new System.Windows.Forms.Button();
            this.propertiesTable = new System.Windows.Forms.DataGridView();
            this.panelProperties = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox = new System.Windows.Forms.RichTextBox();
            this.asmnprt = new System.Windows.Forms.TabControl();
            this.assemblies = new System.Windows.Forms.TabPage();
            this.assemblyTable = new System.Windows.Forms.DataGridView();
            this.parts = new System.Windows.Forms.TabPage();
            this.partTable = new System.Windows.Forms.DataGridView();
            this.projectTree = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.openstripBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.savestripBtn = new System.Windows.Forms.ToolStripButton();
            this.statusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.treeNodeMenu.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertiesTable)).BeginInit();
            this.panelProperties.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.asmnprt.SuspendLayout();
            this.assemblies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.assemblyTable)).BeginInit();
            this.parts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.partTable)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statuslabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 610);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1044, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statuslabel
            // 
            this.statuslabel.Name = "statuslabel";
            this.statuslabel.Size = new System.Drawing.Size(57, 17);
            this.statuslabel.Text = "statustext";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.projectToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1044, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveProjectToolStripMenuItem,
            this.closeProjectToolStripMenuItem,
            this.toolStripSeparator2,
            this.loadPartlistToolStripMenuItem,
            this.toolStripSeparator5,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveProjectToolStripMenuItem
            // 
            this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.saveProjectToolStripMenuItem.Text = "Save Project";
            this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
            // 
            // closeProjectToolStripMenuItem
            // 
            this.closeProjectToolStripMenuItem.Name = "closeProjectToolStripMenuItem";
            this.closeProjectToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.closeProjectToolStripMenuItem.Text = "Close Project";
            this.closeProjectToolStripMenuItem.Click += new System.EventHandler(this.closeProjectToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(140, 6);
            // 
            // loadPartlistToolStripMenuItem
            // 
            this.loadPartlistToolStripMenuItem.Name = "loadPartlistToolStripMenuItem";
            this.loadPartlistToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.loadPartlistToolStripMenuItem.Text = "Load Partlist";
            this.loadPartlistToolStripMenuItem.Click += new System.EventHandler(this.loadPartlistToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(140, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createProjectToolStripMenuItem,
            this.openProjectToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator6,
            this.optionsToolStripMenuItem});
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.projectToolStripMenuItem.Text = "Project";
            // 
            // createProjectToolStripMenuItem
            // 
            this.createProjectToolStripMenuItem.Name = "createProjectToolStripMenuItem";
            this.createProjectToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.createProjectToolStripMenuItem.Text = "Create Project";
            // 
            // openProjectToolStripMenuItem
            // 
            this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.openProjectToolStripMenuItem.Text = "Open Project";
            this.openProjectToolStripMenuItem.Click += new System.EventHandler(this.openProjectToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.saveAsToolStripMenuItem.Text = "Save Project As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(151, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.howToToolStripMenuItem,
            this.toolStripSeparator1,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // howToToolStripMenuItem
            // 
            this.howToToolStripMenuItem.Name = "howToToolStripMenuItem";
            this.howToToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.howToToolStripMenuItem.Text = "How to";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(110, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // imageListProjectTree
            // 
            this.imageListProjectTree.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListProjectTree.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListProjectTree.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(173, 22);
            this.toolStripMenuItem1.Text = "Add Sub Assembly";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.addSubassemblyContextMenu_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(173, 22);
            this.toolStripMenuItem2.Text = "Add Part";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(170, 6);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(173, 22);
            this.toolStripMenuItem3.Text = "Delete";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(170, 6);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.propertiesToolStripMenuItem.Text = "Properties";
            // 
            // treeNodeMenu
            // 
            this.treeNodeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripMenuItem1,
            this.addNewPartToolStripMenuItem,
            this.toolStripMenuItem2,
            this.toolStripSeparator3,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripMenuItem3,
            this.renameToolStripMenuItem,
            this.toolStripSeparator7,
            this.expandAllToolStripMenuItem,
            this.collapseAllToolStripMenuItem,
            this.toolStripSeparator4,
            this.propertiesToolStripMenuItem});
            this.treeNodeMenu.Margin = new System.Windows.Forms.Padding(100, 0, 1000, 0);
            this.treeNodeMenu.Name = "treeNodeMenu";
            this.treeNodeMenu.Size = new System.Drawing.Size(174, 264);
            // 
            // addNewPartToolStripMenuItem
            // 
            this.addNewPartToolStripMenuItem.Name = "addNewPartToolStripMenuItem";
            this.addNewPartToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.addNewPartToolStripMenuItem.Text = "Add New Part";
            this.addNewPartToolStripMenuItem.Click += new System.EventHandler(this.addNewPartToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(170, 6);
            // 
            // expandAllToolStripMenuItem
            // 
            this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.expandAllToolStripMenuItem.Text = "Expand All";
            this.expandAllToolStripMenuItem.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
            // 
            // collapseAllToolStripMenuItem
            // 
            this.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
            this.collapseAllToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.collapseAllToolStripMenuItem.Text = "Collapse All";
            this.collapseAllToolStripMenuItem.Click += new System.EventHandler(this.collapseAllToolStripMenuItem_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.propertiesTable, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(7, 7);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.44186F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.558139F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(255, 516);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.saveProperties, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.closeProperties, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 480);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(249, 33);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // saveProperties
            // 
            this.saveProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveProperties.Location = new System.Drawing.Point(3, 6);
            this.saveProperties.Name = "saveProperties";
            this.saveProperties.Size = new System.Drawing.Size(118, 24);
            this.saveProperties.TabIndex = 0;
            this.saveProperties.Text = "Save";
            this.saveProperties.UseVisualStyleBackColor = true;
            this.saveProperties.Click += new System.EventHandler(this.button1_Click);
            // 
            // closeProperties
            // 
            this.closeProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.closeProperties.Location = new System.Drawing.Point(127, 6);
            this.closeProperties.Name = "closeProperties";
            this.closeProperties.Size = new System.Drawing.Size(119, 24);
            this.closeProperties.TabIndex = 1;
            this.closeProperties.Text = "Close";
            this.closeProperties.UseVisualStyleBackColor = true;
            this.closeProperties.Click += new System.EventHandler(this.closeProperties_Click);
            // 
            // propertiesTable
            // 
            this.propertiesTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertiesTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.propertiesTable.Location = new System.Drawing.Point(3, 3);
            this.propertiesTable.Name = "propertiesTable";
            this.propertiesTable.Size = new System.Drawing.Size(249, 471);
            this.propertiesTable.TabIndex = 1;
            // 
            // panelProperties
            // 
            this.panelProperties.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelProperties.Controls.Add(this.tableLayoutPanel3);
            this.panelProperties.Location = new System.Drawing.Point(781, 86);
            this.panelProperties.Name = "panelProperties";
            this.panelProperties.Size = new System.Drawing.Size(263, 524);
            this.panelProperties.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.24113F));
            this.tableLayoutPanel2.Controls.Add(this.textBox, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.asmnprt, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(211, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 390F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(830, 552);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.Location = new System.Drawing.Point(3, 448);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(824, 101);
            this.textBox.TabIndex = 3;
            this.textBox.Text = "";
            // 
            // asmnprt
            // 
            this.asmnprt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.asmnprt.Controls.Add(this.assemblies);
            this.asmnprt.Controls.Add(this.parts);
            this.asmnprt.Location = new System.Drawing.Point(3, 3);
            this.asmnprt.Name = "asmnprt";
            this.asmnprt.SelectedIndex = 0;
            this.asmnprt.Size = new System.Drawing.Size(824, 439);
            this.asmnprt.TabIndex = 4;
            // 
            // assemblies
            // 
            this.assemblies.Controls.Add(this.assemblyTable);
            this.assemblies.Location = new System.Drawing.Point(4, 22);
            this.assemblies.Name = "assemblies";
            this.assemblies.Padding = new System.Windows.Forms.Padding(3);
            this.assemblies.Size = new System.Drawing.Size(816, 413);
            this.assemblies.TabIndex = 0;
            this.assemblies.Text = "Assemblies";
            this.assemblies.UseVisualStyleBackColor = true;
            // 
            // assemblyTable
            // 
            this.assemblyTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.assemblyTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.assemblyTable.Location = new System.Drawing.Point(3, 3);
            this.assemblyTable.Name = "assemblyTable";
            this.assemblyTable.Size = new System.Drawing.Size(807, 404);
            this.assemblyTable.TabIndex = 0;
            this.assemblyTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.assemblyTable_CellContentClick);
            // 
            // parts
            // 
            this.parts.Controls.Add(this.partTable);
            this.parts.Location = new System.Drawing.Point(4, 22);
            this.parts.Name = "parts";
            this.parts.Padding = new System.Windows.Forms.Padding(3);
            this.parts.Size = new System.Drawing.Size(816, 413);
            this.parts.TabIndex = 1;
            this.parts.Text = "Parts";
            this.parts.UseVisualStyleBackColor = true;
            // 
            // partTable
            // 
            this.partTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.partTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.partTable.Location = new System.Drawing.Point(6, 3);
            this.partTable.Name = "partTable";
            this.partTable.Size = new System.Drawing.Size(807, 379);
            this.partTable.TabIndex = 0;
            // 
            // projectTree
            // 
            this.projectTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.projectTree.Location = new System.Drawing.Point(3, 3);
            this.projectTree.Name = "projectTree";
            this.projectTree.Size = new System.Drawing.Size(202, 552);
            this.projectTree.TabIndex = 0;
            this.projectTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.projectTree_NodeMouseClick);
            this.projectTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.projectTree_NodeMouseDoubleClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.Controls.Add(this.projectTree, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 52);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1044, 558);
            this.tableLayoutPanel1.TabIndex = 2;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openstripBtn,
            this.toolStripSeparator8,
            this.savestripBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1044, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // openstripBtn
            // 
            this.openstripBtn.Image = ((System.Drawing.Image)(resources.GetObject("openstripBtn.Image")));
            this.openstripBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openstripBtn.Name = "openstripBtn";
            this.openstripBtn.Size = new System.Drawing.Size(56, 22);
            this.openstripBtn.Text = "Open";
            this.openstripBtn.Click += new System.EventHandler(this.openstripBtn_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // savestripBtn
            // 
            this.savestripBtn.Image = ((System.Drawing.Image)(resources.GetObject("savestripBtn.Image")));
            this.savestripBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.savestripBtn.Name = "savestripBtn";
            this.savestripBtn.Size = new System.Drawing.Size(51, 22);
            this.savestripBtn.Text = "Save";
            this.savestripBtn.Click += new System.EventHandler(this.savestripBtn_Click);
            // 
            // relDesk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 632);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panelProperties);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip);
            this.Name = "relDesk";
            this.Text = "Reliability Desk";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.treeNodeMenu.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertiesTable)).EndInit();
            this.panelProperties.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.asmnprt.ResumeLayout(false);
            this.assemblies.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.assemblyTable)).EndInit();
            this.parts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.partTable)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem howToToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel statuslabel;
        private System.Windows.Forms.ImageList imageListProjectTree;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;

        private TreeNode copyNode;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem renameToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem propertiesToolStripMenuItem;
        private ContextMenuStrip treeNodeMenu;
        private TableLayoutPanel tableLayoutPanel3;
        private DataGridView propertiesTable;
        private TableLayoutPanel tableLayoutPanel4;
        private Button saveProperties;
        private Button closeProperties;
        private Panel panelProperties;
        private ToolStripMenuItem loadPartlistToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem addNewPartToolStripMenuItem;
        private ToolStripMenuItem createProjectToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem openProjectToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem expandAllToolStripMenuItem;
        private ToolStripMenuItem collapseAllToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanel2;
        private RichTextBox textBox;
        private TabControl asmnprt;
        private TabPage assemblies;
        private DataGridView assemblyTable;
        private TabPage parts;
        private DataGridView partTable;
        private TreeView projectTree;
        private TableLayoutPanel tableLayoutPanel1;
        private ToolStrip toolStrip1;
        private ToolStripButton openstripBtn;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripButton savestripBtn;
    }
}

