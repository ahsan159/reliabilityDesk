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
using TNXMLUtility;

namespace Reliability_Desk
{
    class assembly
    {
        private string mainFile;
        private string createUser;
        private DateTime created;
        private string lastUser;
        private DateTime modified;
        private TreeNode tree;
        private int isTemporary;
        private string filePath;
        int NodeCount;
        IList<assembly> childAssemblies;
        List<part> childParts;
        IList<assembly> parent;

        assembly()
        {
            parent = null;
            mainFile = string.Empty;

        }
    }
}
