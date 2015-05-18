using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diploma.Algorithms
{
    public class NodeData
    {
        public long ID { get; set; }
        public double LengthFromSource { get; set; }
        public double LengthToTarget { get; set; }
        public long ParentID { get; set; }
    }
}