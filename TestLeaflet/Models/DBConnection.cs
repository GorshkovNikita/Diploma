using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestLeaflet.Models
{
    public class DBConnection
    {
        /// <summary>
        /// Получает все пересечения пути
        /// </summary>
        /// <param name="id">Id пути (WayID)</param>
        /// <returns>Все пересечения пути</returns>
        public static List<AllGraphNode> GetAllIntersectionsOfWay(Int64 id)
        {
            return OSMDB.AllGraphNodes.Where(way => (way.Way1 == id || way.Way2 == id)).ToList();
        }

        /// <summary>
        /// Получает все унивкальные пути, которые пересекаются с другими
        /// </summary>
        /// <returns>ID всеч унивкальныч путей</returns>
        public static List<long> GetAllIntersectedWayID()
        {
            return OSMDB.AllGraphNodes.Select(n => n.Way1).Union(OSMDB.AllGraphNodes.Select(n => n.Way2)).ToList();
        }

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