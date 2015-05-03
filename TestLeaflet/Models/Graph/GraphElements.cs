using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Neo4jClient;

namespace TestLeaflet.Models
{
    public class GraphEdge : Relationship, IRelationshipAllowingSourceNode<Point>,
    IRelationshipAllowingTargetNode<Point>
    {
        public static readonly string TypeKey = "road";

        public GraphEdge(NodeReference targetNode, Line data): base(targetNode, data)
        { }
  
        public override string RelationshipTypeKey
        {
            get { return TypeKey; }
        }
    }

}