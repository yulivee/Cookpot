using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using cookpot.bl;
using cookpot.bl.DataModel;
using cookpot.bl.DataStorage.TripleSerialization;
using VDS.RDF;
using VDS.RDF.Query;
using VDS.RDF.Parsing;
using VDS.RDF.Storage;
using System.Text;
using VDS.RDF.Writing;
using System.Diagnostics;
using System.Reflection;
using Microsoft.CSharp;

namespace cookpot.bl.DataStorage
{
    public class RDF : IManager<Dish>
    {

        private readonly string _fusekiURI = "https://fuseki.voiding-warranties.de/cookpot-owl/data";
        private readonly string _cpNamespace = "http://voiding-warranties.de/cookpot/1.0#";
        private readonly string _cpDishNamespace = "http://voiding-warranties.de/cookpot/1.0/Dishes/";
        private readonly string _graphURI = "";

        public bool debug = false;

        private FusekiConnector _fuseki;

        public RDF(string uri, string graphURI = "")
        {
            var uriFromFile = ConfigurationManager.AppSettings["fusekiURI"];
            //Console.WriteLine(">>> URI from Config: {0}",uriFromFile);
            this._fusekiURI = uri;
            this._graphURI = graphURI;
            this._fuseki = new FusekiConnector(_fusekiURI);
        }


        public Dish Create(Dish dish)
        {
            var graph = new Graph();
            graph.BaseUri = new Uri(this._cpNamespace);
            graph.NamespaceMap.AddNamespace("", new Uri(this._cpNamespace));
            graph.NamespaceMap.AddNamespace("cpDishes", new Uri(this._cpDishNamespace));
            var NewEntry = graph.CreateUriNode("cpDishes:" + Guid.NewGuid().ToString());
			var serializer = new TripleSerializer(graph, graph);

			serializer.Serialize2RDF(dish);

            return dish;

        }

        public IEnumerable<Dish> Create(IEnumerable<Dish> objs)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Dish obj)
        {
            throw new NotImplementedException();
        }

        public Dish Read(Dish obj)
        {
            return obj;
            //Execute a raw SPARQL Query
            //Should get a SparqlResultSet back from a SELECT query
            /*
            Object results = this._fuseki.ExecuteQuery("SELECT * WHERE { { ?s ?p ?o } LIMIT 10 }");
            if (results is SparqlResultSet)
            {
                //Print out the Results
                SparqlResultSet rset = (SparqlResultSet)results;
                string rawQueryResult = "";
                foreach (SparqlResult result in rset)
                {
                    rawQueryResult = rawQueryResult + result.ToString() + "\n";
                }

                return rawQueryResult;
            }
            else
            {
                throw new NotImplementedException();
            }

            */

        }

        public IEnumerable<Dish> Read(IEnumerable<Dish> objs)
        {
            throw new NotImplementedException();
        }

        public Dish Update(Dish obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dish> Update(IEnumerable<Dish> objs)
        {
            throw new NotImplementedException();
        }
    }
}