using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace TestLeaflet.Models
{
    public abstract class OSMElement
    {
        public OSMElement(Int64 id, OSMType type)
        {
            ID = id;
            Type = type;
            Tags = new Dictionary<string, string>();
        }

        /// <summary>
        /// Читает и записывает все теги OSM элемента из XML
        /// </summary>
        /// <param name="reader">XML-документ элемента</param>
        protected void ReadTags(XmlReader reader)
        {
            if (!reader.IsEmptyElement || reader.Name == "tag")
            {
                do
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "tag")
                    {
                        Tags.Add(reader.GetAttribute("k"), reader.GetAttribute("v"));
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement || reader.Name == "nd")
                    {
                        break;
                    }
                } while (reader.Read());
            }
        }

        /// <summary>
        /// Проверяет есть ли тег с данным именем в OSM элементе
        /// </summary>
        /// <param name="tagName">Имя тега</param>
        /// <returns>true - содержит тег, false - не содержит</returns>
        public bool HasTag(String tagName)
        {
            return Tags.ContainsKey(tagName);
        }

        /// <summary>
        /// Получает значение тега по его имени
        /// </summary>
        /// <param name="tagName">Имя тега</param>
        /// <returns>Значение тега</returns>
        public String GetTag(String tagName)
        {
            String tagValue;
            Tags.TryGetValue(tagName, out tagValue);
            return tagValue;
        }

        public Int64 ID { get; protected set; }
        public Dictionary<String, String> Tags { get; protected set; }
        public OSMType Type { get; protected set; }
    }

    public enum OSMType { NODE, WAY, RELATION }
}