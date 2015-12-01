using LiteDB;
using System;
using System.Collections.Generic;
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
            if (db == null)
                db = new LiteDatabase(@"C:\Temp\MyData.db");
        }
    }
}
