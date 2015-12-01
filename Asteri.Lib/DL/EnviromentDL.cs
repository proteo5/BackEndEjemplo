using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteri.Lib.DL
{


    public class EnviromentDL
    {
        public static LiteDatabase 
        db = null;

        public 
        EnviromentDL()
        {
            if (!Directory.Exists(@"C:\Temp"))
                Directory.CreateDirectory(@"C:\Temp");

            if (db == null)
                db = new LiteDatabase(@"C:\Temp\MyData.db");
        }
    }
}
