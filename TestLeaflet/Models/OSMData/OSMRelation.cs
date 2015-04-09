using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestLeaflet.Models
{
    public class OSMRelation : OSMElement
    {
        public OSMRelation(Int64 id)
            : base(id, OSMType.RELATION)
        {

        }
    }
}