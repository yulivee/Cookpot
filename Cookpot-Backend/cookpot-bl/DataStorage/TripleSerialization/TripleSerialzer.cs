using System;
using System.Linq;
using System.Collections.Generic;
using cookpot.bl.DataModel;
using VDS.RDF;
using System.Reflection;
using VDS.RDF.Writing;

namespace cookpot.bl.DataStorage.TripleSerialization
{
    public class TripleSerializer
    {
        private IGraph _graph;
        private INodeFactory _nodeFactory;
   //     private readonly string _fusekiURI = "https://fuseki.voiding-warranties.de/cookpot-owl/data";
   //     private readonly string _cpNamespace = "http://voiding-warranties.de/cookpot/1.0#";
   //     private readonly string _cpDishNamespace = "http://voiding-warranties.de/cookpot/1.0/Dishes/";
   //     private readonly string _graphURI = "";
        public TripleSerializer(IGraph graph, INodeFactory nodeFactory)
        {
            this._graph = graph;
            this._nodeFactory = nodeFactory;
        }

        /*
                    _fuseki.UpdateGraph("", Graph.Triples, Enumerable.Empty<Triple>());
         */

        public void WriteToGraph()
        {
            CompressingTurtleWriter ttlWriter = new CompressingTurtleWriter(){PrettyPrintMode = true};
            ttlWriter.Save(_graph, "debug.ttl");
            ttlWriter.ToString();
        }

        public void Serialize2RDF(object objectInstance)
        {
            var rdfSubject = _graph.CreateUriNode("cpDishes:" + Guid.NewGuid().ToString());
            SerializeType(rdfSubject, objectInstance);
            WriteToGraph();
        }

        public void SerializeType(INode NewEntry, object objectInstance)
        {
            var objectType = objectInstance.GetType();
            var objectProperties = objectType.GetProperties().Where(property => property.GetCustomAttribute<RdfNameAttribute>() != null);

            foreach (var objectProperty in objectProperties)
            {
                var propertyInstance = objectProperty.GetValue(objectInstance);
                if (propertyInstance != null)
                {
                    SerializeProperty(NewEntry, objectProperty, objectInstance);
                }
            }
        }

        public void SerializeProperty(INode rdfSubject, PropertyInfo objectProperty, object objectInstance)
        {
            var objectType = objectInstance.GetType();
            var isListProperty = objectProperty.PropertyType.IsGenericType && objectProperty.PropertyType.GetGenericTypeDefinition() == typeof(List<>);
            var isPrimitiveType = objectType.IsPrimitive || objectType.IsValueType || (objectType == typeof(string));
            #if DEBUG
            Console.WriteLine(nameof(SerializeProperty)+": "+objectProperty.Name + " " + objectInstance.GetType());
            #endif

            if (isListProperty && !isPrimitiveType) {
                SerializeList(rdfSubject, objectProperty, objectInstance);
            }
            else if (!isListProperty && !isPrimitiveType) {
                var (RdfSubject, RdfPredicate, RdfObject) = SerializeScalar(rdfSubject, objectProperty, objectInstance);
                AppendToGraph(RdfSubject, RdfPredicate, RdfObject);
            }
            else if ( isPrimitiveType ){
                var (RdfSubject, RdfPredicate, RdfObject) = SerializePrimitive(rdfSubject, objectProperty, objectInstance);
                AppendToGraph(RdfSubject, RdfPredicate, RdfObject);
            }
        }
        public void SerializeList(INode rdfSubject, PropertyInfo property, object objectInstance)
        {
            // propertyValues = List of Objects from a List Property
            dynamic propertyValues = property.GetValue(objectInstance, null);
            var newBlankNode = _graph.CreateBlankNode();
            if (propertyValues == null)
            {
                return;
            }

            var rdfPredicate = _graph.CreateUriNode(property.GetCustomAttribute<RdfNameAttribute>().Name);

            // propertyVal = One Object from a List Property
            foreach (var propertyVal in propertyValues)
            {
                Type propertyType = propertyVal.GetType();

#if DEBUG
                Console.WriteLine(nameof(SerializeList)+": "+"Type of individual value: " + propertyType);
#endif
                if (propertyType.Namespace.Equals("System")) {
                //if (Convert.GetTypeCode(propertyVal) != TypeCode.Object){
                    //This is a string,int,etc
                    SerializeProperty(rdfSubject, property, propertyVal);
                    continue;

                }

                SerializeType(newBlankNode, propertyVal);
            }
        }

        public (INode rdfSubject, string rdfPredicate, string rdfObject) SerializePrimitive(INode rdfSubject, PropertyInfo property, object instance)
        {
            var rdfObject = instance?.ToString();
            if (rdfObject == null) { return (null, null, null); }
            var rdfPredicate = property.GetCustomAttribute<RdfNameAttribute>().Name;

#if DEBUG
            Console.WriteLine(nameof(SerializePrimitive)+": " + rdfSubject + " " + rdfPredicate + " " + rdfObject);
#endif

            return (rdfSubject, rdfPredicate, rdfObject);
        }
        public (INode rdfSubject, string rdfPredicate, string rdfObject) SerializeScalar(INode rdfSubject, PropertyInfo property, object instance)
        {
            var rdfObject = property.GetValue(instance)?.ToString();
            if (rdfObject == null) { return (null, null, null); }
            var rdfPredicate = property.GetCustomAttribute<RdfNameAttribute>().Name;

#if DEBUG
            Console.WriteLine(nameof(SerializeScalar)+": " + rdfSubject + " " + rdfPredicate + " " + rdfObject);
#endif

            return (rdfSubject, rdfPredicate, rdfObject);
        }

        public void AppendToGraph(INode rdfSubject, string RDFpredicate, string RDFobject)
        {
            if (rdfSubject == null || RDFpredicate == null || RDFobject == null) { return; }
            var rdfPredicate = _graph.CreateUriNode(RDFpredicate);
            // https://github.com/dotnetrdf/dotnetrdf/wiki/UserGuide-Typed-Values-And-Lists
            var rdfObject = RDFobject.ToLiteral(_graph);
            _graph.Assert(new Triple(rdfSubject, rdfPredicate, rdfObject));
        }


    }
}