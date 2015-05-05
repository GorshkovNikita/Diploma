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
            //CreateRelationship(GetAllLinesFromWay(316207298));
            //CreateRelationship(GetAllLinesFromWay(316207298));
            //CreateIndex();
            //CreateUniqueConstraint();
            //CreateRelationship(GetAllLinesFromWay(316207298));
            //CreateRelationship(GetAllLinesFromWay(316207297));
            //CreateRelationship(GetAllLinesFromWay(161506551));
        }

        public NodeReference<Point> CreateNode(Point point)
        {
            NodeReference<Point> refPoint = this.GetPoint(point);
            if (refPoint == null)
            {
                return client.Cypher
                    .Create("(p:Point {point})")
                    .WithParam("point", point)
                    .Return(p => p.As<Node<Point>>())
                    .Results
                    .Single()
                    .Reference;
            }
            else return refPoint;
        }

        public void CreateRelationship(List<Line> lstLines)
        {
            for (int i = 0; i < lstLines.Count; i++)
            {
                var source = lstLines[i].Points.First();
                var target = lstLines[i].Points.Last();
                NodeReference<Point> refSource = CreateNode(source);
                NodeReference<Point> refTarget = CreateNode(target);
                client.CreateRelationship(refSource, new GraphEdge(refTarget, 
                    new LineData 
                    {
                        Length = lstLines[i].Length,
                        Lanes = lstLines[i].Lanes,
                        Name = lstLines[i].Name,
                        Oneway = lstLines[i].Oneway,
                        RoadType = lstLines[i].RoadType
                    }
                ));
            }
        }

        public List<long> GetAllNodesOfIntersectionsOfWay(List<AllGraphNode> intersections)
        {
            List<long> nodeIDs = new List<long>();
            for (int i = 0; i < intersections.Count; i++)
            {
                if (!nodeIDs.Contains(intersections[i].NodeID))
                    nodeIDs.Add(intersections[i].NodeID);
            }
            nodeIDs.Sort((n1, n2) => n1.CompareTo(n2));
            return nodeIDs;
        }

        public List<Line> GetAllLinesFromWay(long id)
        {
            List<Line> lstLines = new List<Line>();
            OSMWay way = OSMWay.Create(id);
            List<long> allNodeRefsOnIntersections = this.GetAllNodesOfIntersectionsOfWay(DBConnection.GetAllIntersectionsOfWay(id));
            Line line = null;
            for (int i = 0; i < way.RefNodes.Count; i++)
            {
                if (allNodeRefsOnIntersections.Contains(way.RefNodes[i]))
                {
                    if (line == null)
                    {
                        line = new Line();
                        line.AddPoint(new Point(OSMNode.Create(way.RefNodes[i])));
                    }
                    else
                    {
                        line.AddPoint(new Point(OSMNode.Create(way.RefNodes[i])));
                        line.AddTags(way);
                        lstLines.Add(line);
                        if ((i - 1) != way.RefNodes.Count)
                        {
                            line = new Line();
                            line.AddPoint(new Point(OSMNode.Create(way.RefNodes[i])));
                        }
                    }
                }
                else if (line != null)
                {
                    line.AddPoint(new Point(OSMNode.Create(way.RefNodes[i])));
                }
            }
            return lstLines;
        }

        public NodeReference<Point> GetPoint(Point p)
        {
            try
            {
                return client.Cypher.Match("(point:Point)")
                            .Where((Point point) => point.ID == p.ID)
                            .Return(point => point.As<Node<Point>>())
                            .Results
                            .Single()
                            .Reference;
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