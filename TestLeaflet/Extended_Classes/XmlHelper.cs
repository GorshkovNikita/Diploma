using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;
using TestLeaflet.Models;

namespace TestLeaflet.Extended_Classes
{
    public class XmlHelper
    {
        /// <summary>
        /// Поиск в xml-файле всех точек, соответсвующих светофорам
        /// </summary>
        /// <returns>
        /// Массив точек, соответсвующих светофорам
        /// </returns>
        public static List<Point> GetAllTrafficSignals()
        {
            List<Point> allTrafficSignals = new List<Point>();
            using (XmlReader reader = XmlReader.Create(_filename))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "node")
                    {
                        XDocument doc = XDocument.Load(reader.ReadSubtree());
                        XElement el = doc.Root;
                        if (el.HasElements)
                        {
                            List<XElement> children = el.Elements().ToList();
                            for (Int32 i = 0; i < children.Count; i++)
                            {
                                if ((children[i].Attribute("k").Value == "highway") && (children[i].Attribute("v").Value == "traffic_signals"))
                                {
                                    Point point = XmlHelper.MakePointFromXmlElement(el);
                                    allTrafficSignals.Add(point);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return allTrafficSignals;
        }

        public static Line FindStreet(String name)
        {
            Line street = new Line();
            using (XmlReader reader = XmlReader.Create(_filename))
            {
               
                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "way")
                    {
                        XDocument doc = XDocument.Load(reader.ReadSubtree());
                        XElement el = doc.Root;
                        if (el.Attribute("id").Value == "134430698")
                        {
                            List<XElement> children = el.Elements().ToList();
                            for (Int32 i = 0; i < children.Count; i++)
                            {
                                if (children[i].Name == "nd")
                                {
                                    Point point = XmlHelper.FindPoint(children[i].Attribute("ref").Value);
                                    street.AddPoint(point);
                                }
                            }
                            break;    
                        }
                    }
                }
            }
            return street;
        }

        /// <summary>
        /// Создает xml файл со всеми точками (nodes)
        /// </summary>
        public static void CreateNewXmlWithNodes()
        {
            using (XmlReader reader = XmlReader.Create(_filename))
            {
                using (XmlWriter writer = XmlWriter.Create(_nodesFilename))
                {
                    reader.MoveToContent();
                    writer.WriteStartElement("nodes");
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "node")
                        {
                            writer.WriteNode(reader, true);
                        }
                        else if (reader.NodeType == XmlNodeType.Element && reader.Name == "way")
                        {
                            writer.WriteEndElement();
                            break;
                        }
                    }
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// Создает xml файл со всеми линиями (ways)
        /// </summary>
        public static void CreateNewXmlWithWays()
        {
            using (XmlReader reader = XmlReader.Create(_filename))
            {
                using (XmlWriter writer = XmlWriter.Create(_waysFilename))
                {
                    reader.MoveToContent();
                    writer.WriteStartElement("ways");
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "way")
                        {
                            writer.WriteNode(reader, true);
                        }
                        else if (reader.NodeType == XmlNodeType.Element && reader.Name == "relation")
                        {
                            writer.WriteEndElement();
                            break;
                        }
                    }
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// Создание точки из XML узла
        /// </summary>
        /// <param name="el">XML узел</param>
        /// <returns>Полученная точка</returns>
        public static Point MakePointFromXmlElement(XElement el)
        {
            Double lat, lon;
            lat = Double.Parse(el.Attribute("lat").Value, CultureInfo.InvariantCulture);
            lon = Convert.ToDouble(el.Attribute("lon").Value, CultureInfo.InvariantCulture);
            Point point = new Point(lat, lon);
            return point;
        }

        /// <summary>
        /// Поиск и создание точки по значению id
        /// </summary>
        /// <param name="id">id искомой точки</param>
        /// <returns>Искомая точка</returns>
        public static Point FindPoint(String id)
        {
            using (XmlReader reader = XmlReader.Create(_nodesFilename))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "node")
                    {
                        XDocument doc = XDocument.Load(reader.ReadSubtree());
                        XElement el = doc.Root;
                        if (el.Attribute("id").Value == id)
                        {
                            Point point = XmlHelper.MakePointFromXmlElement(el);
                            return point;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Добавление всех дорог в базу данных из XML
        /// </summary>
        public static void AddWaysInDBFromXml()
        {
            using (XmlReader reader = XmlReader.Create(_waysFilename))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "way")
                    {
                        OSMWay way = OSMWay.Create(reader);
                        if (way.HasTag("highway"))
                        {
                            DBConnection.OSMDB.ExecuteCommand("INSERT INTO dbo.Ways VALUES ({0})", way.ID);
                            if (way.Tags.Count > 0)
                            {
                                for (int i = 0; i < way.Tags.Count; i++)
                                {
                                    DBConnection.OSMDB.ExecuteCommand("INSERT INTO dbo.WayTags VALUES ({0}, {1}, {2})", way.ID, way.Tags.ElementAt(i).Key, way.Tags.ElementAt(i).Value);
                                }
                            }
                            if (way.RefNodes.Count > 0)
                            {
                                for (int i = 0; i < way.RefNodes.Count; i++)
                                {
                                    DBConnection.OSMDB.ExecuteCommand("INSERT INTO dbo.WayNodes VALUES ({0}, {1})", way.ID, way.RefNodes[i]);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Добавление всех узлов в базу данных из XML
        /// </summary>
        public static void AddNodesInDBFromXml()
        {
            using (XmlReader reader = XmlReader.Create(_nodesFilename))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "node")
                    {
                        OSMNode node = OSMNode.Create(reader);
                        DBConnection.OSMDB.ExecuteCommand("INSERT INTO dbo.Nodes VALUES ({0}, {1}, {2})", node.ID, node.Latitude, node.Longitude);
                        if (node.Tags.Count > 0)
                        {
                            for (int i = 0; i < node.Tags.Count; i++)
                            {
                                DBConnection.OSMDB.ExecuteCommand("INSERT INTO dbo.NodeTags VALUES ({0}, {1}, {2})", node.ID, node.Tags.ElementAt(i).Key, node.Tags.ElementAt(i).Value);
                            }
                        }
                    }
                }
            }
        }

        private static String _filename = "D:\\Diploma\\TestLeaflet\\moscow_russia.osm";
        private static String _nodesFilename = "D:\\Diploma\\TestLeaflet\\nodes.xml";
        private static String _waysFilename = "D:\\Diploma\\TestLeaflet\\ways.xml";
    }
}