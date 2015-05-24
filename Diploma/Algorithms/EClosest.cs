using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diploma.Models;
using Diploma.Models.GraphData;
using Diploma.Extended_Classes;

namespace Diploma.Algorithms
{
    public class EClosest
    {
        public static List<Path> RunAlgo(EClosestIterator graph, long source, long target, double E, string shortAlgo)
        {
            graph.SetCurrentNode(source);
            graph.Source = source;
            graph.Target = target;
            graph.OpenedNodes.Add(source, new List<NodeData>());
            if (shortAlgo == "dijkstra")
            {
                graph.OpenedNodes[source].Add(new NodeData()
                {
                    ID = graph.Current.ID,
                    LengthFromSource = 0,
                    ParentID = 0
                });
            }
            else
            {
                graph.OpenedNodes[source].Add(new NodeData()
                {
                    ID = graph.Current.ID,
                    LengthFromSource = 0,
                    LengthToTarget = Distance.Calc(Graph.GetPoint(graph.Current.ID), Graph.GetPoint(target)),
                    ParentID = 0
                });
            }
            while (true)
            {
                for (int i = 0; i < graph.AdjacentNodes.Count; i++)
                {
                    if (!graph.IsDeletedEdgeContainsEdge(graph.Current.ID, graph.AdjacentNodes[i].ID))
                    {
                        if (!graph.IsClosedNodeListContainsNode(graph.AdjacentNodes[i].ID))
                        {
                            if (!graph.IsOpenedNodeListContainsNode(graph.AdjacentNodes[i].ID))
                            {
                                graph.OpenedNodes.Add(graph.AdjacentNodes[i].ID, new List<NodeData>());
                                if (shortAlgo == "dijkstra")
                                {
                                    graph.OpenedNodes[graph.AdjacentNodes[i].ID].Add(new NodeData()
                                    {
                                        ID = graph.AdjacentNodes[i].ID,
                                        LengthFromSource = graph.AdjacentNodes[i].Length + graph.Current.LengthFromSource,
                                        ParentID = graph.Current.ID
                                    });
                                }
                                else
                                {
                                    graph.OpenedNodes[graph.AdjacentNodes[i].ID].Add(new NodeData()
                                    {
                                        ID = graph.AdjacentNodes[i].ID,
                                        LengthFromSource = graph.AdjacentNodes[i].Length + graph.Current.LengthFromSource,
                                        LengthToTarget = Distance.Calc(Graph.GetPoint(graph.AdjacentNodes[i].ID), Graph.GetPoint(target)),
                                        ParentID = graph.Current.ID
                                    });
                                }
                            }
                            else
                            {
                                if (shortAlgo == "dijkstra")
                                {
                                    graph.OpenedNodes[graph.AdjacentNodes[i].ID].Add(new NodeData()
                                    {
                                        ID = graph.AdjacentNodes[i].ID,
                                        LengthFromSource = graph.AdjacentNodes[i].Length + graph.Current.LengthFromSource,
                                        ParentID = graph.Current.ID
                                    });
                                }
                                else
                                {
                                    graph.OpenedNodes[graph.AdjacentNodes[i].ID].Add(new NodeData()
                                    {
                                        ID = graph.AdjacentNodes[i].ID,
                                        LengthFromSource = graph.AdjacentNodes[i].Length + graph.Current.LengthFromSource,
                                        LengthToTarget = Distance.Calc(Graph.GetPoint(graph.AdjacentNodes[i].ID), Graph.GetPoint(target)),
                                        ParentID = graph.Current.ID
                                    });
                                }
                                graph.OpenedNodes[graph.AdjacentNodes[i].ID] = graph.OpenedNodes[graph.AdjacentNodes[i].ID].OrderBy(n => n.LengthFromSource).ToList();
                            }
                        }
                    }
                }
                graph.RemoveCurrentFromOpenedNodesAndAddInClosedNodes();
                try
                {
                    if (shortAlgo == "dijkstra")
                        graph.SetCurrentNode();
                    else
                        graph.SetCurrentNodeForAStar();
                }
                catch
                {
                    break;
                }
            }
            graph.RemoveCurrentFromOpenedNodesAndAddInClosedNodes();
            Path path = graph.CreatePath();
            _minLength = path.Length;
            _alternatives = new List<Path>();
            GetAllAlternatives(target, new Path(), 0, E, graph);
            return _alternatives;
        }

        public static void GetAllAlternatives(long nodeID, Path path, double length, double e, EClosestIterator graph)
        {
            if (length > e + _minLength)
                return;
            path.Points.Insert(0, Graph.GetPoint(nodeID));
            path.Length = length;
            for (int i = 0; i < graph.OpenedNodes[nodeID].Count; i++)
            {
                if (!(path.Points.Where(p => p.ID == graph.OpenedNodes[nodeID][i].ParentID).Any()))
                {
                    if (graph.OpenedNodes[nodeID][i].ParentID != 0)
                    {
                        GetAllAlternatives(graph.OpenedNodes[nodeID][i].ParentID, path,
                            // магия
                            path.Length + graph.OpenedNodes[nodeID][i].LengthFromSource - graph.OpenedNodes[graph.OpenedNodes[nodeID][i].ParentID][0].LengthFromSource,
                            e, graph);
                    }
                    else
                    {
                        path.CalculateFactors();
                        Path p = (Path)path.Clone();
                        _alternatives.Add(p);
                    }
                }
            }
            try
            {
                path.Length -= Graph.GetLineDataBetweenNodes(path.Points[0].ID, path.Points[1].ID).Length;
            }
            catch
            {
                path.Length = 0;
            }
            path.Points.RemoveAt(0);
        }

        public static List<Path> _alternatives;
        public static double _minLength;
    }
}