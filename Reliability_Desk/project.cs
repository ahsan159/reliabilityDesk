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
    class project
    {
        private string mainFile;
        private string createUser;
        private DateTime created;
        private string lastUser;
        private DateTime modified;
        private TreeNode projectTree;
        private int isTemporary;
        private string filePath;
        int NodeCount;

        public TreeNode getNode(string type, string value)
        {
            return projectTree;
        }

        public TreeNode getProject()
        {
            return projectTree;
        }
        public TreeNode getAssembly()
        {
            return projectTree;
        }
        public TreeNode getPart()
        {
            return projectTree;
        }
        public TreeNode getConnection() 
        {
            return projectTree;
        }
    }
}
