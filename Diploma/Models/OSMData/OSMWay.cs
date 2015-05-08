using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Diploma.Models
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
            List<WayNode> wn = DBConnection.OSMDB.WayNodes.Where(waynode => waynode.WayID == way.ID).ToList();
            foreach (var node in wn)
            {
                way.RefNodes.Add(node.NodeID);
            }
            List<WayTag> wt = DBConnection.OSMDB.WayTags.Where(waytag => waytag.WayID == way.ID).ToList();
            foreach (var tag in wt)
            {
                way.Tags.Add(tag.TagName, tag.TagValue);
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