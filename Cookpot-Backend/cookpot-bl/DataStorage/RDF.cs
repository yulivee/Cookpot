using System;
using System.Linq;
using System.Collections.Generic;
using cookpot.bl;
using cookpot.bl.DataModel;
using VDS.RDF;
using VDS.RDF.Query;
using VDS.RDF.Parsing;
using VDS.RDF.Storage;
using System.Text;
using VDS.RDF.Writing;
using System.Diagnostics;
using System.Reflection;

namespace cookpot.bl.DataStorage
{
    public class RDF : IManager<Dish>
    {

        private readonly string _fusekiURI = "https://fuseki.voiding-warranties.de/cookpot/data";
        private readonly string _cpNamespace = "http://voiding-warranties.de/cookpot/1.0#";
        private readonly string _cpDishNamespace = "http://voiding-warranties.de/cookpot/1.0/Dishes/";
        private readonly string _graphURI = "";

        public bool debug = false;

        private FusekiConnector _fuseki;

        public RDF(string uri, string graphURI = "")
        {
            this._fusekiURI = uri;
            this._graphURI = graphURI;
            this._fuseki = new FusekiConnector(_fusekiURI);
        }

        public Dish Create(Dish dish)
        {

            var Graph = new Graph();
            Graph.BaseUri = new Uri(this._cpNamespace);
            Graph.NamespaceMap.AddNamespace("", new Uri(this._cpNamespace));
            Graph.NamespaceMap.AddNamespace("cpDishes", new Uri(this._cpDishNamespace));
            var NewDish = Graph.CreateUriNode("cpDishes:"+Guid.NewGuid().ToString());
            var Title = Graph.CreateUriNode(":title");
            var NewTitle = Graph.CreateLiteralNode(dish.Title);


            var dishType = typeof(Dish);
            Console.WriteLine(dishType);           
            //x.Assembly.
            var sType = typeof(string);
            var nameAttr = typeof(RdfNameAttribute);
            var fancyProps = dishType.GetProperties()
            .Where(x => 
                    x.PropertyType == sType && 
                    x.CustomAttributes.Any(y => y.AttributeType == nameAttr)
            );
            //fancyProps.First().Attributes


            // ?s                   ?p       ?o
            // cpNS:BangBangChicken cp:title "Bang Bang Chicken: The Authentic Sichuan Version"@en;
            Graph.Assert(new Triple(NewDish, Title, NewTitle));


                if (this.debug == true) { 
                    CompressingTurtleWriter ttlWriter = new CompressingTurtleWriter();
                    ttlWriter.Save(Graph,"debug.ttl");
                    return dish; 
                }

                //this._fuseki.Update(SparqlUpdateStatement.ToString());

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