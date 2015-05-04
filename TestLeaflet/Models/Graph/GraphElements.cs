﻿using System;
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

        public GraphEdge(NodeReference targetNode, LineData data): base(targetNode, data)
        { }
  
        public override string RelationshipTypeKey
        {
            get { return TypeKey; }
        }
    }

    public class LineData
    {
        public double Length { get; set; }
        public String RoadType { get; set; }
        public Int32 Lanes { get; set; }
        public Boolean Oneway { get; set; }
        public String Name { get; set; }
    }

}