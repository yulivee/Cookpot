using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
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

        private (string rdfPredicate, string rdfObject) SerializeType<T>(PropertyInfo property, T instance)
        //private (string rdfPredicate, string rdfObject) SerializeType(PropertyInfo property, object instance)
        {
            var rdfObject = property.GetValue(instance)?.ToString();
            if (rdfObject == null) { return ("", ""); }
            var rdfPredicate = property.GetCustomAttribute<RdfNameAttribute>().Name;
            if ( debug == true ) { Console.WriteLine(rdfPredicate +" "+ rdfObject); };

            return (rdfPredicate, rdfObject);
        }

        private void SerializeListType(IUriNode rdfSubject, Graph graph, PropertyInfo property, Dish dish)
        {

            // propertyValues = List of Objects from a List Property
            dynamic propertyValues = property.GetValue(dish, null);
            if (propertyValues == null)
            {
                return;
            }

            var rdfPredicate = graph.CreateUriNode(property.GetCustomAttribute<RdfNameAttribute>().Name);
            var newBlankNode = graph.CreateBlankNode();

            Console.WriteLine(
            rdfSubject.ToString() + " " +
            rdfPredicate.ToString() + " " +
            newBlankNode.ToString() + "."
            );

            // propertyVal = One Object from a List Property
            foreach (var propertyVal in propertyValues)
            {
                Type propertyType = propertyVal.GetType();
                Console.WriteLine("Type of individual value: " + propertyType);
                if (propertyType.Namespace.Equals("System"))
                {
                    //This is a string,int,etc
                    Console.WriteLine("\nWe caught a string!");
                    Console.WriteLine(rdfSubject.ToString() + " " +property.GetCustomAttribute<RdfNameAttribute>().Name +" " + propertyVal.ToString());
                    Console.WriteLine();
                    continue;
                }
                IEnumerable<PropertyInfo> propInfo = propertyType.GetProperties();

                foreach (var info in propInfo)
                {
                    var propertyValue = info.GetValue(propertyVal)?.ToString();
                    if (propertyValue == null) { continue; }
                    Console.WriteLine(
                    newBlankNode.ToString() + " " +
                    info.GetCustomAttribute<RdfNameAttribute>().Name + " " +
                    propertyValue);
                }
            }
        }

        private void SerializeNode(IUriNode rdfSubject, string RDFpredicate, string RDFobject, Graph graph)
        {
            var rdfPredicate = graph.CreateUriNode(RDFpredicate);
            var rdfObject = graph.CreateLiteralNode(RDFobject);
            graph.Assert(new Triple(rdfSubject, rdfPredicate, rdfObject));
        }

        private void SerializeProperty(IUriNode rdfSubject, Graph graph, PropertyInfo dishProperty, Dish dish)
        {

            var isListProperty = dishProperty.PropertyType.IsGenericType && dishProperty.PropertyType.GetGenericTypeDefinition() == typeof(List<>);
            if (isListProperty)
            {
                //IEnumerable<Triple> blankNode = SerializeListType(dishProperty, dish);
                SerializeListType(rdfSubject, graph, dishProperty, dish);
            }
            else if (!isListProperty)
            {
                var uriNode = SerializeType<Dish>(dishProperty, dish);
                //var uriNode = SerializeType(dishProperty, dish);
                SerializeNode(rdfSubject, uriNode.rdfPredicate, uriNode.rdfObject, graph);
            }

        }
        public Dish Create(Dish dish)
        {

            var Graph = new Graph();
            Graph.BaseUri = new Uri(this._cpNamespace);
            Graph.NamespaceMap.AddNamespace("", new Uri(this._cpNamespace));
            Graph.NamespaceMap.AddNamespace("cpDishes", new Uri(this._cpDishNamespace));
            var NewDish = Graph.CreateUriNode("cpDishes:" + Guid.NewGuid().ToString());

            var dishType = typeof(Dish);
            var dishProperties = dishType.GetProperties().Where(property => property.GetCustomAttribute<RdfNameAttribute>() != null);

            foreach (var dishProperty in dishProperties)
            {
                SerializeProperty(NewDish, Graph, dishProperty, dish);
            }

            if (this.debug == true)
            {
                CompressingTurtleWriter ttlWriter = new CompressingTurtleWriter();
                ttlWriter.Save(Graph, "debug.ttl");
                return dish;
            }

            _fuseki.UpdateGraph("", Graph.Triples, Enumerable.Empty<Triple>());

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