using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Neo4jClient;

namespace TestLeaflet.Models
{
    public class Graph
    {
        public Graph()
        {
            client = new GraphClient(new Uri("http://localhost:7474/db/data"));
            client.Connect();
            //CreateNodesRelationshipsIndexes();
            CreateNodes();
        }

        public void CreateNodes()
        {
            //Int32 rowNum = 0;
            for (Int32 i = 2; i <= 20; i += 2)
            {
                Int64 nodeID = DBConnection.OSMDB.AllGraphNodes.Where(node => node.RowNum == i).First().NodeID;
                client.Create(new Point(OSMNode.Create(nodeID)));
            }
        }

        private GraphClient client;
    }
}