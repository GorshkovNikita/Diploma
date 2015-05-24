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
        public static Path RunAlgo(EClosestIterator graph, long source, long target, int E)
        {
            graph.SetCurrentNode(source);
            graph.Source = source;
            graph.Target = target;
            graph.OpenedNodes.Add(source, new List<NodeData>());
            graph.OpenedNodes[source].Add(new NodeData()
            {
                ID = graph.Current.ID,
                LengthFromSource = 0,
                ParentID = 0
            });
            //while (graph.Current.ID != target)
            while (true)// || (graph.ClosedNodes[target].Max(n => n.LengthFromSource) < E))
            //while (graph.ClosedNodes.Count != 5)// Graph.GetCountNodes())
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
                                graph.OpenedNodes[graph.AdjacentNodes[i].ID].Add(new NodeData()
                                {
                                    ID = graph.AdjacentNodes[i].ID,
                                    LengthFromSource = graph.AdjacentNodes[i].Length + graph.Current.LengthFromSource,
                                    ParentID = graph.Current.ID
                                });
                            }
                            else
                            {
                                //graph.UpdateLength(graph.AdjacentNodes[i]);
                                //if (!(graph.OpenedNodes[graph.AdjacentNodes[i].ID].Where(n => n.ParentID == graph.Current.ID).Any()))
                                //{
                                    graph.OpenedNodes[graph.AdjacentNodes[i].ID].Add(new NodeData()
                                    {
                                        ID = graph.AdjacentNodes[i].ID,
                                        LengthFromSource = graph.AdjacentNodes[i].Length + graph.Current.LengthFromSource,
                                        ParentID = graph.Current.ID
                                    });
                                    graph.OpenedNodes[graph.AdjacentNodes[i].ID] = graph.OpenedNodes[graph.AdjacentNodes[i].ID].OrderBy(n => n.LengthFromSource).ToList();
                                //}
                            }
                        }
                    }
                }
                graph.RemoveCurrentFromOpenedNodesAndAddInClosedNodes();
                try
                {
                    graph.SetCurrentNode();
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
            //for (int i = 0; i < _alternatives.Count; i++)
                //_alternatives[i].CalculateLength();
            return path;
            //return new List<Path>();
        }

        public static void GetAllAlternatives(long nodeID, Path path, double length, int e, EClosestIterator graph)
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