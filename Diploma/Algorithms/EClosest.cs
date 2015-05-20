using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diploma.Models;
using Diploma.Models.GraphData;

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
            //while (graph.OpenedNodes[target].Max(n => n.LengthFromSource) < E)
            while (graph.ClosedNodes.Count != 150)// Graph.GetCountNodes())
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
                                graph.UpdateLength(graph.AdjacentNodes[i]);
                                graph.OpenedNodes[graph.AdjacentNodes[i].ID].Add(new NodeData()
                                {
                                    ID = graph.AdjacentNodes[i].ID,
                                    LengthFromSource = graph.AdjacentNodes[i].Length + graph.Current.LengthFromSource,
                                    ParentID = graph.Current.ID
                                });
                            }
                        }
                    }
                }
                graph.RemoveCurrentFromOpenedNodesAndAddInClosedNodes();
                graph.SetCurrentNode();
            }
            graph.RemoveCurrentFromOpenedNodesAndAddInClosedNodes();
            Path path = graph.CreatePath();
            return path;
            //return new List<Path>();
        }
    }
}