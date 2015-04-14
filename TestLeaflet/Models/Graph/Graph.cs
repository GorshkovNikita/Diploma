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
            for (Int32 i = 2; i <= 104740; i += 2)
            {
                Int64 nodeID = DBConnection.OSMDB.ExecuteQuery<Int64>("SELECT TOP 1 [Way1], [Way2], [NodeID], [RowNum] FROM [OSM_Data_DB].[dbo].[AllGraphNodes] WHERE RowNum = {0}", i).First();
                client.Create(new Point(OSMNode.Create(nodeID)));
            }
        }

        private GraphClient client;
    }
}