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
        }

        public void BuildTestGraph()
        {
            /*this.CreateRelationshipsOfWay(GetAllLinesFromWay(188511699));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(23964689));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(27658584));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(53577880));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(48739269));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(162195072));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(27658625));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(27658605));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(34332773));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(208675194));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(48739267));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(27658586));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(23964693));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(23964830));

            this.CreateRelationshipsOfWay(GetAllLinesFromWay(183548861));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(48533122));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(183548854));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(156665635));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(209304409));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(224783959));

            this.CreateRelationshipsOfWay(GetAllLinesFromWay(201227737));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(156665679));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(156665691));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(156665641));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(201227736));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(48739263));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(156665689));

            this.CreateRelationshipsOfWay(GetAllLinesFromWay(200772097));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(189372921));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(201735418));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(110892444));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(110891907));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(201735419));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(200772089));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(200772093));

            this.CreateRelationshipsOfWay(GetAllLinesFromWay(242047410));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(175108116));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(183544008));
            this.CreateRelationshipsOfWay(GetAllLinesFromWay(183550844));*/
        }

        public void BuildGraph()
        {
            List<long> allIDs = DBConnection.GetAllIntersectedWayID();
            for (int i = 0; i < 50; i++)
            {
                this.CreateRelationshipsOfWay(GetAllLinesFromWay(allIDs[i]));
            }
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

        public void CreateRelationshipsOfWay(List<Line> lstLines)
        {
            for (int i = 0; i < lstLines.Count; i++)
            {
                var source = lstLines[i].Points.First();
                var target = lstLines[i].Points.Last();
                NodeReference<Point> refSource = CreateNode(source);
                NodeReference<Point> refTarget = CreateNode(target);
                LineData lineData = new LineData
                {
                    WayID = lstLines[i].WayID,
                    Length = lstLines[i].Length,
                    Lanes = lstLines[i].Lanes,
                    Name = lstLines[i].Name,
                    Oneway = lstLines[i].Oneway,
                    RoadType = lstLines[i].RoadType,
                    MaxSpeed = lstLines[i].MaxSpeed,
                    Surface = lstLines[i].Surface
                };
                client.CreateRelationship(refSource, new GraphEdge(refTarget, lineData));
                if (!lstLines[i].Oneway)
                    client.CreateRelationship(refTarget, new GraphEdge(refSource, lineData));
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