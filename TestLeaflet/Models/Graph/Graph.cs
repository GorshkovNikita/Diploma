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
            //CreateIndex();
            //CreateUniqueConstraint();
            CreateNodes();
        }

        public void CreateNodes()
        {
            //Int32 rowNum = 0;
            for (Int32 i = 2; i <= 20; i += 2)
            {
                Int64 nodeID = DBConnection.OSMDB.AllGraphNodes.Where(node => node.RowNum == i).First().NodeID;
                if (this.GetPoint(nodeID) == null)
                {
                    var point = new Point(OSMNode.Create(nodeID));
                    client.Cypher
                        .Create("(point:Point {point})")
                        .WithParam("point", point)
                        .ExecuteWithoutResults();
                }
            }
        }

        public Point GetPoint(Int64 id)
        {
            try
            {
                return client.Cypher.Match("(point:Point)")
                            .Where((Point point) => point.ID == id)
                            .Return(point => point.As<Point>())
                            .Results
                            .Single();
            }
            catch
            {
                return null;
            }
        }

        public void CreateIndex()
        {
            client.Cypher
                .Create("INDEX ON :Point(ID)")
                .ExecuteWithoutResults();
        }

        public void CreateUniqueConstraint()
        {
            client.Cypher
                .Create("CONSTRAINT ON (point:Point) ASSERT point.ID IS UNIQUE")
                .ExecuteWithoutResults();
        }

        private GraphClient client;
    }
}