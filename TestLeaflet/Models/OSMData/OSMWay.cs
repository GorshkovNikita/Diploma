using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace TestLeaflet.Models
{
    public class OSMWay : OSMElement
    {
        private OSMWay(Int64 id) 
            : base(id, OSMType.WAY)
        {
            RefNodes = new List<Int64>();
        }

        /// <summary>
        /// Создание OSM элемента way из XmlReader
        /// </summary>
        /// <param name="reader">XmlReader</param>
        /// <returns>Созданный элемент way</returns>
        public static OSMWay Create(XmlReader reader)
        {
            OSMWay way = new OSMWay(Convert.ToInt64(reader.GetAttribute("id")));
            way.ReadRefNodes(reader); 
            way.ReadTags(reader);
            return way;
        }

        /// <summary>
        /// Создание OSM элемента way по WayID из базы данных
        /// </summary>
        /// <param name="id">WayID</param>
        /// <returns>Созданный элемент way</returns>
        public static OSMWay Create(Int64 id)
        {
            OSMWay way = new OSMWay(id);
            var wayTags = from wayTag in DBConnection.OSMDB.WayTags
                          where wayTag.WayID == way.ID
                          select new { wayTag.TagName, wayTag.TagValue };
            foreach (var wt in wayTags)
            {
                way.Tags.Add(wt.TagName, wt.TagValue);
            }
            var wayNodes = from wayNode in DBConnection.OSMDB.WayNodes
                           where wayNode.WayID == way.ID
                           select wayNode.NodeID;
            foreach (long wn in wayNodes)
            {
                way.RefNodes.Add(wn);
            }
            return way;
        }

        /// <summary>
        /// Чтение ссылок на OSM элементы node, из которых создан путь
        /// </summary>
        /// <param name="reader">XmlReader</param>
        protected void ReadRefNodes(XmlReader reader)
        {
            RefNodes = new List<Int64>();
            do
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "nd")
                {
                    RefNodes.Add(Convert.ToInt64(reader.GetAttribute("ref")));
                }
                else if (reader.NodeType == XmlNodeType.EndElement || reader.Name == "tag")
                {
                    break;
                }
            } while (reader.Read());
        }

        /// <summary>
        /// Список NodeID, из которых состоит путь
        /// </summary>
        public List<Int64> RefNodes { get; private set; }
    }
}