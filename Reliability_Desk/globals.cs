using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reliability_Desk
{
    public class globals
    {
        public enum fieldParts
        {
            name = 0,
            cmID,
            manu,
            cat,
            scat,
            des,
            package,
            mtbf,
            heritage,
            rad,
            rel,
            oug,
            user,
            added,
            path
        }
        public enum fieldEnum
        {
            Name=0,
            cmID,
            Manufacturer,
            Category,
            Subcategory,
            Description,
            Package,
            Grade,
            Temperature,
            MTBF,
            Radiation,
            Outgassing
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
                               "Description",
                               "Package",
                               "Grade",
                               "Temperature",
                               "MTBF",
                               "Radiation",
                               "Outgassing"
                                          };
        public static readonly string name = dataFields[0];
        public static readonly string cmID = dataFields[1];
        public static readonly string mftr = dataFields[2];
        public static readonly string cat = dataFields[3];
        public static readonly string scat = dataFields[4];
        public static readonly string desc = dataFields[5];
        public static readonly string pack = dataFields[6];
        public static readonly string grade = dataFields[7];
        public static readonly string temp = dataFields[8];
        public static readonly string mtbf = dataFields[9];
        public static readonly string rad = dataFields[10];
        public static readonly string outg = dataFields[11];
        public static readonly string partlist = "PartList";
        public static readonly string part = "Part";
        public static readonly string assembly = "Assembly";
        public static readonly string project = "Project";
        public static readonly string[] shortDataFields = { "name", 
                                                              "cmID", 
                                                              "mftr",
                                                              "cat",
                                                              "scat",
                                                              "des",
                                                              "pack",
                                                              "grade",
                                                              "temp",
                                                              "mtbf",
                                                              "rad",
                                                              "out"                                                              
                                                          };
    }
}
