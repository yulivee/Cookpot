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

        private void SerializeListType(INode rdfSubject, Graph graph, PropertyInfo property, object objectInstance) {
            // propertyValues = List of Objects from a List Property
            dynamic propertyValues = property.GetValue(objectInstance, null);
            var newBlankNode = graph.CreateBlankNode();
            if (propertyValues == null) {
                return;
            }

            var rdfPredicate = graph.CreateUriNode(property.GetCustomAttribute<RdfNameAttribute>().Name);

            // propertyVal = One Object from a List Property
            foreach (var propertyVal in propertyValues) {
                Type propertyType = propertyVal.GetType();
                Console.WriteLine("Type of individual value: " + propertyType);
                if (propertyType.Namespace.Equals("System")) {
                    //This is a string,int,etc
                    SerializeProperty(rdfSubject, graph, property, propertyVal);
                    continue;
                }

                IEnumerable<PropertyInfo> propInfo = propertyType.GetProperties();

                Serialize2RDF(newBlankNode, graph, propertyVal);
            }
        }

        private (INode rdfSubject, string rdfPredicate, string rdfObject) SerializeType(INode rdfSubject, Graph graph, PropertyInfo property, object instance)
        {
            var rdfObject = property.GetValue(instance)?.ToString();
            if (rdfObject == null) { return (rdfSubject,"", ""); }
            var rdfPredicate = property.GetCustomAttribute<RdfNameAttribute>().Name;
            if (debug == true) { Console.WriteLine(rdfPredicate + " " + rdfObject); };

            return (rdfSubject, rdfPredicate, rdfObject);
        }

        private void SerializeNode(INode rdfSubject, string RDFpredicate, string RDFobject, Graph graph)
        {
            var rdfPredicate = graph.CreateUriNode(RDFpredicate);
            var rdfObject = graph.CreateLiteralNode(RDFobject);
            graph.Assert(new Triple(rdfSubject, rdfPredicate, rdfObject));
        }

        private void SerializeProperty(INode rdfSubject, Graph graph, PropertyInfo objectProperty, object objectInstance) {
            // If its from namespace System, its a List/Enumerable etc. Objects are from Cookpot namespace
            var isSystemProperty = objectProperty.GetType().Namespace.Contains("System");
            var isListProperty = objectProperty.PropertyType.IsGenericType && objectProperty.PropertyType.GetGenericTypeDefinition() == typeof(List<>);

            if (isListProperty && isSystemProperty) {
                SerializeListType(rdfSubject, graph, objectProperty, objectInstance);
            }
            else if (isListProperty && !isSystemProperty) {
            //    SerializeObjectType(rdfSubject, graph, objectProperty, objectInstance);
            }
            else if (!isListProperty) {
                SerializeType(rdfSubject, graph, objectProperty, objectInstance);
            }
        }

        private void Serialize2RDF( INode rdfSubject, Graph graph, object objectInstance ){
            var objectType = objectInstance.GetType();  
            var objectProperties = objectType.GetProperties().Where(property => property.GetCustomAttribute<RdfNameAttribute>() != null);
            foreach (var dishProperty in objectProperties)
            {
                SerializeProperty(rdfSubject, graph, dishProperty, objectInstance);
            }
        }

        public Dish Create(Dish dish)
        {
            var Graph = new Graph();
            Graph.BaseUri = new Uri(this._cpNamespace);
            Graph.NamespaceMap.AddNamespace("", new Uri(this._cpNamespace));
            Graph.NamespaceMap.AddNamespace("cpDishes", new Uri(this._cpDishNamespace));
            var NewDish = Graph.CreateUriNode("cpDishes:" + Guid.NewGuid().ToString());

            Serialize2RDF(NewDish, Graph, dish);

            // TODO: Just hand graph, First rdfSubject and Object into Serialize Property. Put the foreach loop into Serialize Property
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