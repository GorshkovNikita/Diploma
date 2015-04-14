using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestLeaflet.Models
{
    public class DBConnection
    {
        public static OSMDataClassesDataContext OSMDB 
        {
            get
            {
                if (_db != null)
                    return _db;
                else
                {
                    _db = new OSMDataClassesDataContext();
                    return _db;
                }
            }
        }

        private static OSMDataClassesDataContext _db;
    }
}