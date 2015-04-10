using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Globalization;

namespace TestLeaflet.Models
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
            var nodeCoords = from nodeCoord in DBConnection.OSMDB.Nodes
                             where nodeCoord.NodeID == node.ID
                             select new { nodeCoord.Latitude, nodeCoord.Longitude };
            foreach (var nc in nodeCoords)
            {
                node.Latitude = nc.Latitude;
                node.Longitude = nc.Longitude;
            }
            var nodeTags = from nodeTag in DBConnection.OSMDB.NodeTags
                           where nodeTag.NodeID == node.ID
                           select new { nodeTag.TagName, nodeTag.TagValue };
            foreach (var wt in nodeTags)
            {
                node.Tags.Add(wt.TagName, wt.TagValue);
            }
            return node;
        }

        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
    }
}