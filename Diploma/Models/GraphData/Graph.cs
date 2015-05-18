using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Neo4jClient;

namespace Diploma.Models.GraphData
{
    public class Graph
    {
        /// <summary>
        /// Построение тестового графа (из Википедии)
        /// </summary>
        public static void BuildTestGraphFromWiki()
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
            Client.CreateRelationship(refSource, new GraphEdge(refTarget, lineData));
            Client.CreateRelationship(refTarget, new GraphEdge(refSource, lineData));

            p.ID = 1;
            refSource = CreateNode(p).Reference;
            p.ID = 3;
            refTarget = CreateNode(p).Reference;
            lineData = new LineData
            {
                Length = 9
            };
            Client.CreateRelationship(refSource, new GraphEdge(refTarget, lineData));
            Client.CreateRelationship(refTarget, new GraphEdge(refSource, lineData));

            p.ID = 1;
            refSource = CreateNode(p).Reference;
            p.ID = 6;
            refTarget = CreateNode(p).Reference;
            lineData = new LineData
            {
                Length = 14
            };
            Client.CreateRelationship(refSource, new GraphEdge(refTarget, lineData));
            Client.CreateRelationship(refTarget, new GraphEdge(refSource, lineData));

            p.ID = 2;
            refSource = CreateNode(p).Reference;
            p.ID = 4;
            refTarget = CreateNode(p).Reference;
            lineData = new LineData
            {
                Length = 15
            };
            Client.CreateRelationship(refSource, new GraphEdge(refTarget, lineData));
            Client.CreateRelationship(refTarget, new GraphEdge(refSource, lineData));

            p.ID = 2;
            refSource = CreateNode(p).Reference;
            p.ID = 3;
            refTarget = CreateNode(p).Reference;
            lineData = new LineData
            {
                Length = 10
            };
            Client.CreateRelationship(refSource, new GraphEdge(refTarget, lineData));
            Client.CreateRelationship(refTarget, new GraphEdge(refSource, lineData));

            p.ID = 3;
            refSource = CreateNode(p).Reference;
            p.ID = 6;
            refTarget = CreateNode(p).Reference;
            lineData = new LineData
            {
                Length = 2
            };
            Client.CreateRelationship(refSource, new GraphEdge(refTarget, lineData));
            Client.CreateRelationship(refTarget, new GraphEdge(refSource, lineData));

            p.ID = 3;
            refSource = CreateNode(p).Reference;
            p.ID = 4;
            refTarget = CreateNode(p).Reference;
            lineData = new LineData
            {
                Length = 11
            };
            Client.CreateRelationship(refSource, new GraphEdge(refTarget, lineData));
            Client.CreateRelationship(refTarget, new GraphEdge(refSource, lineData));

            p.ID = 6;
            refSource = CreateNode(p).Reference;
            p.ID = 5;
            refTarget = CreateNode(p).Reference;
            lineData = new LineData
            {
                Length = 9
            };
            Client.CreateRelationship(refSource, new GraphEdge(refTarget, lineData));
            Client.CreateRelationship(refTarget, new GraphEdge(refSource, lineData));

            p.ID = 4;
            refSource = CreateNode(p).Reference;
            p.ID = 5;
            refTarget = CreateNode(p).Reference;
            lineData = new LineData
            {
                Length = 6
            };
            Client.CreateRelationship(refSource, new GraphEdge(refTarget, lineData));
            Client.CreateRelationship(refTarget, new GraphEdge(refSource, lineData));
        }

        /// <summary>
        /// Построение тестового графа
        /// </summary>
        public static void BuildTestGraph()
        {
            CreateRelationshipsOfWay(188511699);
            CreateRelationshipsOfWay(23964689);
            CreateRelationshipsOfWay(27658584);
            CreateRelationshipsOfWay(53577880);
            CreateRelationshipsOfWay(48739269);
            CreateRelationshipsOfWay(162195072);
            CreateRelationshipsOfWay(27658625);
            CreateRelationshipsOfWay(27658605);
            CreateRelationshipsOfWay(34332773);
            CreateRelationshipsOfWay(208675194);
            CreateRelationshipsOfWay(48739267);
            CreateRelationshipsOfWay(27658586);
            CreateRelationshipsOfWay(23964693);
            CreateRelationshipsOfWay(23964830);
            CreateRelationshipsOfWay(183548861);
            CreateRelationshipsOfWay(48533122);
            CreateRelationshipsOfWay(183548854);
            CreateRelationshipsOfWay(156665635);
            CreateRelationshipsOfWay(209304409);
            CreateRelationshipsOfWay(224783959);
            CreateRelationshipsOfWay(201227737);
            CreateRelationshipsOfWay(156665679);
            CreateRelationshipsOfWay(156665691);
            CreateRelationshipsOfWay(156665641);
            CreateRelationshipsOfWay(201227736);
            CreateRelationshipsOfWay(48739263);
            CreateRelationshipsOfWay(156665689);
            CreateRelationshipsOfWay(200772097);
            CreateRelationshipsOfWay(189372921);
            CreateRelationshipsOfWay(201735418);
            CreateRelationshipsOfWay(110892444);
            CreateRelationshipsOfWay(110891907);
            CreateRelationshipsOfWay(201735419);
            CreateRelationshipsOfWay(200772089);
            CreateRelationshipsOfWay(200772093);
            CreateRelationshipsOfWay(242047410);
            CreateRelationshipsOfWay(175108116);
            CreateRelationshipsOfWay(183544008);
            CreateRelationshipsOfWay(183550844);
            CreateRelationshipsOfWay(23964691);
            CreateRelationshipsOfWay(108058488);
            CreateRelationshipsOfWay(140827279);
            CreateRelationshipsOfWay(162194016);
            CreateRelationshipsOfWay(175147608);
            CreateRelationshipsOfWay(177365318);
            CreateRelationshipsOfWay(23964308);
            CreateRelationshipsOfWay(39093169);
            CreateRelationshipsOfWay(39093170);
            CreateRelationshipsOfWay(183548865);
            CreateRelationshipsOfWay(106260455);
            CreateRelationshipsOfWay(220440831);
            CreateRelationshipsOfWay(37853645);
            CreateRelationshipsOfWay(37938973);
            CreateRelationshipsOfWay(48739252);
            CreateRelationshipsOfWay(109742311);
            CreateRelationshipsOfWay(39580958);
            CreateRelationshipsOfWay(227681212);
            CreateRelationshipsOfWay(23964313);
            CreateRelationshipsOfWay(23964319);
            CreateRelationshipsOfWay(23964326);
            CreateRelationshipsOfWay(38532045);
            CreateRelationshipsOfWay(263482442);
            CreateRelationshipsOfWay(24951330);
            CreateRelationshipsOfWay(142236999);
            CreateRelationshipsOfWay(142239111);
            CreateRelationshipsOfWay(157102613);
            CreateRelationshipsOfWay(185310802);
            CreateRelationshipsOfWay(34332772);
            CreateRelationshipsOfWay(140827279);
            CreateRelationshipsOfWay(39093168);
            CreateRelationshipsOfWay(77737642);
            CreateRelationshipsOfWay(78185855);
            CreateRelationshipsOfWay(85632746);
            CreateRelationshipsOfWay(111192007);
            CreateRelationshipsOfWay(125126705);
            CreateRelationshipsOfWay(27658614);
            CreateRelationshipsOfWay(27658684);
            CreateRelationshipsOfWay(95403317);
            CreateRelationshipsOfWay(79112036);
            CreateRelationshipsOfWay(102185908);
        }

        /// <summary>
        /// Построение всего графа дорог
        /// </summary>
        public static void BuildGraph()
        {
            // 8942
            // 6174090
            List<long> allIDs = DBConnection.GetAllIntersectedWayID();
            allIDs.Sort();
            for (int i = 0; i < allIDs.Count; i++)
            {
                CreateRelationshipsOfWay(allIDs[i]);
            }
        }

        /// <summary>
        /// Создает узел графа из точки
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Node<Point> CreateNode(Point point)
        {
            Node<Point> pnt = GetNode(point.ID);
            if (pnt == null)
            {
                return Client.Cypher
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
        public static void CreateRelationshipsOfWay(long id)
        {
            List<Line> lstLines = GetAllLinesFromWay(id);
            for (int i = 0; i < lstLines.Count; i++)
            {
                var source = lstLines[i].Points.First();
                var target = lstLines[i].Points.Last();
                if (GetLineDataBetweenNodes(source.ID, target.ID) == null)
                {
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
                    Client.CreateRelationship(refSource, new GraphEdge(refTarget, lineData));
                    if (!lstLines[i].Oneway)
                        Client.CreateRelationship(refTarget, new GraphEdge(refSource, lineData));
                }
            }
        }

        /// <summary>
        /// Получает ID всех точек пересечения на пути, которые должны стать узлами
        /// </summary>
        /// <param name="intersections">Все пересечения пути</param>
        /// <returns>ID точек пересечения на пути</returns>
        public static List<long> GetAllNodesOfIntersectionsOfWay(List<AllGraphNode> intersections)
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
        public static List<Line> GetAllLinesFromWay(long id)
        {
            List<Line> lstLines = new List<Line>();
            OSMWay way = OSMWay.Create(id);
            List<long> allNodeRefsOnIntersections = GetAllNodesOfIntersectionsOfWay(DBConnection.GetAllIntersectionsOfWay(id));
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
        public static List<Node<Point>> GetAllAdjacentNodes(long id)
        {
            try
            {
                return Client.Cypher.Match("(n)-[r]->(m)")
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
        /// Получает ID смежных узлов и расстояние до них
        /// </summary>
        /// <param name="id">ID узла</param>
        /// <returns>Id and length</returns>
        public static List<NodeDist> GetAllAdjacentNodesInfo(long id)
        {
            try
            {
                return Client.Cypher.Match("(n)-[r]->(m)")
                            .Where((Point n) => n.ID == id)
                            .Return((m, r) => new NodeDist
                            {
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
        /// Получает узел графа по его ID
        /// </summary>
        /// <param name="id">ID узла</param>
        /// <returns>Узел или null, если узел не найден</returns>
        public static Node<Point> GetNode(long id)
        {
            try
            {
                return Client.Cypher.Match("(point:Point)")
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
        /// Получает данные об узле графа в виде Point
        /// </summary>
        /// <param name="id">ID узла</param>
        /// <returns>Данные об узле</returns>
        public static Point GetPoint(long id)
        {
            try
            {
                return Client.Cypher.Match("(point:Point)")
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

        /// <summary>
        /// Получает данные о ребре между двумя узлами
        /// </summary>
        /// <param name="source">Узел-источник</param>
        /// <param name="target">Конечный узел</param>
        /// <returns>Данные о ребре</returns>
        public static LineData GetLineDataBetweenNodes(long source, long target)
        {
            try
            {
                return Client.Cypher.Match("(n)-[r]->(m)")
                    .Where((Point n) => n.ID == source)
                    .AndWhere((Point m) => m.ID == target)
                    .Return(r => r.As<LineData>())
                    .Results
                    .Single();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Находит ближайшую точку к заданной
        /// </summary>
        /// <param name="point">Заданная точка</param>
        /// <returns>Найденная точка</returns>
        public static Point GetNearest(Point point)
        {
            try
            {
                double latLowBound = point.Latitude - 0.002;
                double latHighBound = point.Latitude + 0.002;
                double lonLowBound = point.Longitude - 0.002;
                double lonHighBound = point.Longitude + 0.002;
                return Client.Cypher.Match("(n)")
                    .Where((Point n) => n.Latitude > latLowBound)
                    .AndWhere((Point n) => n.Latitude < latHighBound)
                    .AndWhere((Point n) => n.Longitude > lonLowBound)
                    .AndWhere((Point n) => n.Longitude < lonHighBound)
                    .Return(n => n.As<Point>())
                    .Results
                    .First();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Создает индекс для атрибута узла - ID
        /// </summary>
        public static void CreateIndex()
        {
            Client.Cypher
                .Create("INDEX ON :Point(ID)")
                .ExecuteWithoutResults();
        }

        /// <summary>
        /// Создает ограничение на уникальность атрибута узла - ID
        /// </summary>
        public static void CreateUniqueConstraint()
        {
            Client.Cypher
                .Create("CONSTRAINT ON (point:Point) ASSERT point.ID IS UNIQUE")
                .ExecuteWithoutResults();
        }

        public static GraphClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new GraphClient(new Uri("http://localhost:7474/db/data"));
                    _client.Connect();
                    return _client;
                }
                else
                    return _client;
            }
        }

        private static GraphClient _client;
    }
}