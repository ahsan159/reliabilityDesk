using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reliability_Desk
{
    public class globals
    {
        public enum fieldEnum
        {
            Name=0,
            cmID,
            Manufacturer,
            Category,
            Subcategory,
            Description
        }
        enum fieldes
        {
            Name = 0,
            cmID,
            mftr,
            cat,
            scat,
            desc
        }

        public static readonly string[] dataFields = {"Name",
                               "cmID",
                               "Manuafcturer",
                               "Category",
                               "Subcategory",
                               "Description"};
        public static readonly string name = dataFields[0];
        public static readonly string cmID = dataFields[1];
        public static readonly string mftr = dataFields[2];
        public static readonly string cat = dataFields[3];
        public static readonly string scat = dataFields[4];
        public static readonly string desc = dataFields[5];
        public static readonly string partlist = "PartList";
        public static readonly string part = "Part";
        public static readonly string assembly = "Assembly";
        public static readonly string project = "Project";
        public static readonly string[] shortDataFields = { "name", 
                                                              "cmID", 
                                                              "mftr",
                                                              "cat",
                                                              "scat",
                                                              "des" };
    }
}
