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
    sealed class  store
    {
        List<assembly> assemblies;
        List<part> parts;
        static store cInstance = null;

        public static store instance()
        {
            if (cInstance == null)
            {
                cInstance = new store();
            }
            return cInstance;
        }
        public store()
        {
            assemblies = new List<assembly>();
            parts = new List<part>();
        }
        public  void add(assembly a)
        {
            assemblies.Add(a);
        }
        public  void add(part p)
        {
            parts.Add(p);
        }
        public assembly findAssembly(string s)
        {
            foreach(assembly a in assemblies)
            {
                if (a.getFullPath() == s)
                {
                    //MessageBox.Show(a.getName() + "," + a.getFullPath(), "FOUND");
                    return a;
                }
            }
            return null;
        }
        public List<assembly> getAssemblies()
        {
            return assemblies;
        }
        public string count()
        {
            return parts.Count.ToString() + "," + assemblies.Count.ToString();
        }
    }
}
