using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Neo4jClient;

namespace Diploma.Models.Graph
{
    public class Graph
    {
        public Graph()
        {
            _client = new GraphClient(new Uri("http://localhost:7474/db/data"));
            _client.Connect();
        }

        /// <summary>
        /// Построение тестового графа (из Википедии)
        /// </summary>
        public void BuildTestGraphFromWiki()
        {
            Point p = new Point();
            p.ID = 1;
            NodeReference<Point> refSource = CreateNode(p).Reference;
            p.ID = 2;
            NodeReference<Point> refTarget = CreateNode(p).Reference;
            LineData lineData = new LineData
            {
                Length = 7
            };
            _client.CreateRelationship(refSource, new GraphEdge(refTarget, lineData));
            _client.CreateRelationship(refTarget, new GraphEdge(refSource, lineData));

            p.ID = 1;
            refSource = CreateNode(p).Reference;
            p.ID = 3;
            refTarget = CreateNode(p).Reference;
            lineData = new LineData
            {
                Length = 9
            };
            _client.CreateRelationship(refSource, new GraphEdge(refTarget, lineData));
            _client.CreateRelationship(refTarget, new GraphEdge(refSource, lineData));

            p.ID = 1;
            refSource = CreateNode(p).Reference;
            p.ID = 6;
            refTarget = CreateNode(p).Reference;
            lineData = new LineData
            {
                Length = 14
            };
            _client.CreateRelationship(refSource, new GraphEdge(refTarget, lineData));
            _client.CreateRelationship(refTarget, new GraphEdge(refSource, lineData));

            p.ID = 2;
            refSource = CreateNode(p).Reference;
            p.ID = 4;
            refTarget = CreateNode(p).Reference;
            lineData = new LineData
            {
                Length = 15
            };
            _client.CreateRelationship(refSource, new GraphEdge(refTarget, lineData));
            _client.CreateRelationship(refTarget, new GraphEdge(refSource, lineData));

            p.ID = 2;
            refSource = CreateNode(p).Reference;
            p.ID = 3;
            refTarget = CreateNode(p).Reference;
            lineData = new LineData
            {
                Length = 10
            };
            _client.CreateRelationship(refSource, new GraphEdge(refTarget, lineData));
            _client.CreateRelationship(refTarget, new GraphEdge(refSource, lineData));

            p.ID = 3;
            refSource = CreateNode(p).Reference;
            p.ID = 6;
            refTarget = CreateNode(p).Reference;
            lineData = new LineData
            {
                Length = 2
            };
            _client.CreateRelationship(refSource, new GraphEdge(refTarget, lineData));
            _client.CreateRelationship(refTarget, new GraphEdge(refSource, lineData));

            p.ID = 3;
            refSource = CreateNode(p).Reference;
            p.ID = 4;
            refTarget = CreateNode(p).Reference;
            lineData = new LineData
            {
                Length = 11
            };
            _client.CreateRelationship(refSource, new GraphEdge(refTarget, lineData));
            _client.CreateRelationship(refTarget, new GraphEdge(refSource, lineData));

            p.ID = 6;
            refSource = CreateNode(p).Reference;
            p.ID = 5;
            refTarget = CreateNode(p).Reference;
            lineData = new LineData
            {
                Length = 9
            };
            _client.CreateRelationship(refSource, new GraphEdge(refTarget, lineData));
            _client.CreateRelationship(refTarget, new GraphEdge(refSource, lineData));

            p.ID = 4;
            refSource = CreateNode(p).Reference;
            p.ID = 5;
            refTarget = CreateNode(p).Reference;
            lineData = new LineData
            {
                Length = 6
            };
            _client.CreateRelationship(refSource, new GraphEdge(refTarget, lineData));
            _client.CreateRelationship(refTarget, new GraphEdge(refSource, lineData));
        }

        /// <summary>
        /// Построение тестового графа
        /// </summary>
        public void BuildTestGraph()
        {
            this.CreateRelationshipsOfWay(188511699);
            this.CreateRelationshipsOfWay(23964689);
            this.CreateRelationshipsOfWay(27658584);
            this.CreateRelationshipsOfWay(53577880);
            this.CreateRelationshipsOfWay(48739269);
            this.CreateRelationshipsOfWay(162195072);
            this.CreateRelationshipsOfWay(27658625);
            this.CreateRelationshipsOfWay(27658605);
            this.CreateRelationshipsOfWay(34332773);
            this.CreateRelationshipsOfWay(208675194);
            this.CreateRelationshipsOfWay(48739267);
            this.CreateRelationshipsOfWay(27658586);
            this.CreateRelationshipsOfWay(23964693);
            this.CreateRelationshipsOfWay(23964830);
            this.CreateRelationshipsOfWay(183548861);
            this.CreateRelationshipsOfWay(48533122);
            this.CreateRelationshipsOfWay(183548854);
            this.CreateRelationshipsOfWay(156665635);
            this.CreateRelationshipsOfWay(209304409);
            this.CreateRelationshipsOfWay(224783959);
            this.CreateRelationshipsOfWay(201227737);
            this.CreateRelationshipsOfWay(156665679);
            this.CreateRelationshipsOfWay(156665691);
            this.CreateRelationshipsOfWay(156665641);
            this.CreateRelationshipsOfWay(201227736);
            this.CreateRelationshipsOfWay(48739263);
            this.CreateRelationshipsOfWay(156665689);
            this.CreateRelationshipsOfWay(200772097);
            this.CreateRelationshipsOfWay(189372921);
            this.CreateRelationshipsOfWay(201735418);
            this.CreateRelationshipsOfWay(110892444);
            this.CreateRelationshipsOfWay(110891907);
            this.CreateRelationshipsOfWay(201735419);
            this.CreateRelationshipsOfWay(200772089);
            this.CreateRelationshipsOfWay(200772093);
            this.CreateRelationshipsOfWay(242047410);
            this.CreateRelationshipsOfWay(175108116);
            this.CreateRelationshipsOfWay(183544008);
            this.CreateRelationshipsOfWay(183550844);
            this.CreateRelationshipsOfWay(23964691);
            this.CreateRelationshipsOfWay(108058488);
            this.CreateRelationshipsOfWay(140827279);
            this.CreateRelationshipsOfWay(162194016);
            this.CreateRelationshipsOfWay(175147608);
            this.CreateRelationshipsOfWay(177365318);
            this.CreateRelationshipsOfWay(23964308);
            this.CreateRelationshipsOfWay(39093169);
            this.CreateRelationshipsOfWay(39093170);
            this.CreateRelationshipsOfWay(183548865);
            this.CreateRelationshipsOfWay(106260455);
            this.CreateRelationshipsOfWay(220440831);
            this.CreateRelationshipsOfWay(37853645);
            this.CreateRelationshipsOfWay(37938973);
            this.CreateRelationshipsOfWay(48739252);
            this.CreateRelationshipsOfWay(109742311);
            this.CreateRelationshipsOfWay(39580958);
            this.CreateRelationshipsOfWay(227681212);
            this.CreateRelationshipsOfWay(23964313);
            this.CreateRelationshipsOfWay(23964319);
            this.CreateRelationshipsOfWay(23964326);
            this.CreateRelationshipsOfWay(38532045);
            this.CreateRelationshipsOfWay(263482442);
            this.CreateRelationshipsOfWay(24951330);
            this.CreateRelationshipsOfWay(142236999);
            this.CreateRelationshipsOfWay(142239111);
            this.CreateRelationshipsOfWay(157102613);
            this.CreateRelationshipsOfWay(185310802);
            this.CreateRelationshipsOfWay(34332772);
            this.CreateRelationshipsOfWay(140827279);
            this.CreateRelationshipsOfWay(39093168);
            this.CreateRelationshipsOfWay(77737642);
            this.CreateRelationshipsOfWay(78185855);
            this.CreateRelationshipsOfWay(85632746);
            this.CreateRelationshipsOfWay(111192007);
            this.CreateRelationshipsOfWay(125126705);
            this.CreateRelationshipsOfWay(27658614);
            this.CreateRelationshipsOfWay(27658684);
            this.CreateRelationshipsOfWay(95403317);
            this.CreateRelationshipsOfWay(79112036);
            this.CreateRelationshipsOfWay(102185908);
        }

        /// <summary>
        /// Построение всего графа дорог
        /// </summary>
        public void BuildGraph()
        {
            List<long> allIDs = DBConnection.GetAllIntersectedWayID();
            for (int i = 0; i < allIDs.Count; i++)
            {
                this.CreateRelationshipsOfWay(allIDs[i]);
            }
        }

        /// <summary>
        /// Создает узел графа из точки
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Node<Point> CreateNode(Point point)
        {
            Node<Point> pnt = this.GetNode(point.ID);
            if (pnt == null)
            {
                return _client.Cypher
                    .Create("(p:Point {point})")
                    .WithParam("point", point)
                    .Return(p => p.As<Node<Point>>())
                    .Results
                    .Single();
            }
            else return pnt;
        }

        /// <summary>
        /// Создает связи и узлы на пути
        /// </summary>
        /// <param name="id">ID пути</param>
        public void CreateRelationshipsOfWay(long id)
        {
            List<Line> lstLines = this.GetAllLinesFromWay(id);
            for (int i = 0; i < lstLines.Count; i++)
            {
                var source = lstLines[i].Points.First();
                var target = lstLines[i].Points.Last();
                NodeReference<Point> refSource = CreateNode(source).Reference;
                NodeReference<Point> refTarget = CreateNode(target).Reference;
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
                _client.CreateRelationship(refSource, new GraphEdge(refTarget, lineData));
                if (!lstLines[i].Oneway)
                    _client.CreateRelationship(refTarget, new GraphEdge(refSource, lineData));
            }
        }

        /// <summary>
        /// Получает ID всех точек пересечения на пути, которые должны стать узлами
        /// </summary>
        /// <param name="intersections">Все пересечения пути</param>
        /// <returns>ID точек пересечения на пути</returns>
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

        /// <summary>
        /// Получает все линии, образующие путь с идентификатором id
        /// </summary>
        /// <param name="id">ID пути</param>
        /// <returns>Список линий, образующих путь</returns>
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

        /// <summary>
        /// Получает все узлы, смежные с данным
        /// </summary>
        /// <param name="id">ID вершины</param>
        /// <returns>Узлы графа, смежные с данной</returns>
        public List<Node<Point>> GetAllAdjacentNodes(long id)
        {
            try
            {
                return _client.Cypher.Match("(n)-[r]->(m)")
                            .Where((Point n) => n.ID == id)
                            .Return(m => m.As<Node<Point>>())
                            .Results
                            .ToList();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Получает ID всех узлов, смежных с данным
        /// </summary>
        /// <param name="id">ID узла</param>
        /// <returns>Список ID узлов</returns>
        public List<long> GetAllAdjacentNodeIDs(long id)
        {
            try
            {
                
                return _client.Cypher.Match("(n)-[r]->(m)")
                            .Where((Point n) => n.ID == id)
                            .Return(m => m.As<Node<Point>>().Data.ID)
                            .Results
                            .ToList();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Полуяает узел графа по его ID
        /// </summary>
        /// <param name="id">ID узла</param>
        /// <returns>Узел или null, если узел не найден</returns>
        public Node<Point> GetNode(long id)
        {
            try
            {
                return _client.Cypher.Match("(point:Point)")
                            .Where((Point point) => point.ID == id)
                            .Return(point => point.As<Node<Point>>())
                            .Results
                            .Single();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Получает ID смежных узлов и расстояние до них
        /// </summary>
        /// <param name="id">ID узла</param>
        /// <returns>Id and length</returns>
        public List<NodeDist> GetAllAdjacentNodesInfo(long id)
        {
            try
            {
                return _client.Cypher.Match("(n)-[r]->(m)")
                            .Where((Point n) => n.ID == id)
                            .Return((m, r) => new NodeDist {
                                ID = m.As<Point>().ID,
                                Length = r.As<Line>().Length
                            })
                            .Results
                            .ToList();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Создает индекс для атрибута узла - ID
        /// </summary>
        public void CreateIndex()
        {
            _client.Cypher
                .Create("INDEX ON :Point(ID)")
                .ExecuteWithoutResults();
        }

        /// <summary>
        /// Создает ограничение на уникальность атрибута узла - ID
        /// </summary>
        public void CreateUniqueConstraint()
        {
            _client.Cypher
                .Create("CONSTRAINT ON (point:Point) ASSERT point.ID IS UNIQUE")
                .ExecuteWithoutResults();
        }

        private GraphClient _client;
    }
}