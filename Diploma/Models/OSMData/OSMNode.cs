using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Globalization;

namespace Diploma.Models
{
    public class OSMNode : OSMElement
    {
        private OSMNode(Int64 id)
            : base(id, OSMType.NODE)
        {

        }

        public OSMNode(Int64 id, Double lat, Double lon) 
            : base(id, OSMType.NODE)
        {
            Latitude = lat;
            Longitude = lon;
        }

        /// <summary>
        /// Создание OSM элемента way из XmlReader
        /// </summary>
        /// <param name="reader">XmlReader</param>
        /// <returns>Созданный элемент node</returns>
        public static OSMNode Create(XmlReader reader)
        {
            OSMNode node = new OSMNode(Convert.ToInt64(reader.GetAttribute("id")));
            node.Latitude = Convert.ToDouble(reader.GetAttribute("lat"), CultureInfo.InvariantCulture);
            node.Longitude = Convert.ToDouble(reader.GetAttribute("lon"), CultureInfo.InvariantCulture);
            node.ReadTags(reader);
            return node;
        }

        /// <summary>
        /// Создание OSM элемента node по NodeID из базы данных
        /// </summary>
        /// <param name="id">NodeID</param>
        /// <returns>Созданный элемент node</returns>
        public static OSMNode Create(Int64 id)
        {
            OSMNode node = new OSMNode(id);
            Node n = DBConnection.OSMDB.Nodes.Where(nd => nd.NodeID == node.ID).First();
            node.Latitude = n.Latitude;
            node.Longitude = n.Longitude;
            List<NodeTag> nt = DBConnection.OSMDB.NodeTags.Where(ndtag => ndtag.NodeID == node.ID).ToList();
            foreach (var tag in nt)
            {
                node.Tags.Add(tag.TagName, tag.TagValue);
            }
            return node;
        }

        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
    }
}