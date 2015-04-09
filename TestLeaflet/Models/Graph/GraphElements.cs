using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Neo4jClient;

namespace TestLeaflet.Models
{
    public class GraphNode
    {
        public GraphNode()
        {

        }
    }

    public class GraphEdge : Relationship, IRelationshipAllowingSourceNode<GraphNode>,
    IRelationshipAllowingTargetNode<GraphNode>
    {
        public static readonly string TypeKey = "road";

        public GraphEdge(NodeReference targetNode, GraphEdgeData data): base(targetNode, data)
        { }
  
        public override string RelationshipTypeKey
        {
            get { return TypeKey; }
        }
    }

    public class GraphEdgeData
    {
        public GraphEdgeData(Line line)
        {
            Line = line;
        }

        public Line Line { get; private set; }
    }
}