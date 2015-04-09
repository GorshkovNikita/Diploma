using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Neo4jClient;

namespace TestLeaflet.Models
{
    public class Graph
    {
        public Graph()
        {
            client = new GraphClient(new Uri("http://localhost:7474/db/data"));
            client.Connect();
            //CreateNodesRelationshipsIndexes();
        }

        private GraphClient client;
    }
}