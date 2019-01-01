using System;
using System.Linq;
using System.Collections.Generic;
using cookpot.bl.DataModel;
using VDS.RDF;
using System.Reflection;


namespace cookpot.bl.DataStorage.TripleSerialization
{
	public class TripleSerializer {
		private IGraph _graph;
		private INodeFactory _nodeFactory;
        private readonly string _fusekiURI = "https://fuseki.voiding-warranties.de/cookpot-owl/data";
        private readonly string _cpNamespace = "http://voiding-warranties.de/cookpot/1.0#";
        private readonly string _cpDishNamespace = "http://voiding-warranties.de/cookpot/1.0/Dishes/";
        private readonly string _graphURI = "";
		public TripleSerializer(IGraph graph, INodeFactory nodeFactory) {
			this._graph = graph;
			this._nodeFactory = nodeFactory;
		}

/*
            if (this.debug == true)
            {
                CompressingTurtleWriter ttlWriter = new CompressingTurtleWriter();
                ttlWriter.Save(Graph, "debug.ttl");
                return dish;
            }

            _fuseki.UpdateGraph("", Graph.Triples, Enumerable.Empty<Triple>());
 */

        public void Serialize2RDF( object objectInstance ){
            var objectType = objectInstance.GetType();
            var objectProperties = objectType.GetProperties().Where(property => property.GetCustomAttribute<RdfNameAttribute>() != null);
            foreach (var objectProperty in objectProperties)
            {
                SerializeProperty(rdfSubject, objectProperty, objectInstance);
            }
        }
        public void SerializeProperty(INode rdfSubject, PropertyInfo objectProperty, object objectInstance) {
            // If its from namespace System, its a List/Enumerable etc. Objects are from Cookpot namespace
            var isSystemProperty = objectProperty.GetType().Namespace.Contains("System");
            var isListProperty = objectProperty.PropertyType.IsGenericType && objectProperty.PropertyType.GetGenericTypeDefinition() == typeof(List<>);

            if (isListProperty && isSystemProperty) {
                SerializeListType(rdfSubject, objectProperty, objectInstance);
            }
            else if (isListProperty && !isSystemProperty) {
            //    SerializeObjectType(rdfSubject, graph, objectProperty, objectInstance);
            }
            else if (!isListProperty) {
                SerializeType(rdfSubject, objectProperty, objectInstance);
            }
        }
        public void SerializeListType(INode rdfSubject, PropertyInfo property, object objectInstance) {
            // propertyValues = List of Objects from a List Property
            dynamic propertyValues = property.GetValue(objectInstance, null);
            var newBlankNode = _graph.CreateBlankNode();
            if (propertyValues == null) {
                return;
            }

            var rdfPredicate = _graph.CreateUriNode(property.GetCustomAttribute<RdfNameAttribute>().Name);

            // propertyVal = One Object from a List Property
            foreach (var propertyVal in propertyValues) {
                Type propertyType = propertyVal.GetType();
                #if DEBUG
                Console.WriteLine("Type of individual value: " + propertyType);
                #endif
                if (propertyType.Namespace.Equals("System")) {
                    //This is a string,int,etc
^                    SerializeProperty(rdfSubject, property, propertyVal);
                    continue;
                }

                IEnumerable<PropertyInfo> propInfo = propertyType.GetProperties();

                Serialize2RDF(propertyVal);
            }
        }

        public (INode rdfSubject, string rdfPredicate, string rdfObject) SerializeType(INode rdfSubject, PropertyInfo property, object instance)
        {
            var rdfObject = property.GetValue(instance)?.ToString();
            if (rdfObject == null) { return (rdfSubject,"", ""); }
            var rdfPredicate = property.GetCustomAttribute<RdfNameAttribute>().Name;

            #if DEBUG
            Console.WriteLine(rdfPredicate + " " + rdfObject);
            #endif

            return (rdfSubject, rdfPredicate, rdfObject);
        }

        public void SerializeNode(INode rdfSubject, string RDFpredicate, string RDFobject )
        {
            var rdfPredicate = _graph.CreateUriNode(RDFpredicate);
            var rdfObject = _graph.CreateLiteralNode(RDFobject);
            _graph.Assert(new Triple(rdfSubject, rdfPredicate, rdfObject));
        }


	}
}