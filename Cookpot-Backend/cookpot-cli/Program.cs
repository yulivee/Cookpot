using System;
using cookpot.al;
using cookpot.bl;
using cookpot.bl.DataStorage;
using cookpot.bl.DataModel;
using System.Collections.Generic;
using VDS.RDF;
using VDS.RDF.Writing;
using VDS.RDF.Parsing;
using cookpot.bl.DataStorage.TripleSerialization;

namespace cookpot.cli
{
    class Program
    {
        static string _cpNamespace = "http://voiding-warranties.de/cookpot/1.0#";
        static string _cpDishNamespace = "http://voiding-warranties.de/cookpot/1.0/Dishes/";

        private class ListDatatypes
        {
            [RdfName(":TIntegerList")]
            public List<int> TIntegerList { get; set; }= new List<int>();
            [RdfName(":TStringList")]
            public List<string> TStringList { get; set; } = new List<string>();
            [RdfName(":TFloatList")]
            public List<float> TFloatList { get; set; } = new List<float>();
            [RdfName(":TDoubleList")]
            public List<double> TDoubleList { get; set; } = new List<double>();
            [RdfName(":TNuclearDatatypeList")]
            public List<NuclearDatatypes> TNuclearDatatypeList { get; set; } = new List<NuclearDatatypes>();
        }

        private class NuclearDatatypes
        {
            [RdfName(":TInteger")]
            public int TInteger { get; set; }
            [RdfName(":TString")]
            public string TString { get; set; }
            [RdfName(":TFloat")]
            public float TFloat { get; set; }
            [RdfName(":TDouble")]
            public double TDouble { get; set; }
            [RdfName(":TNullableInt")]
            public Nullable<int> TNullableInt { get; set; }
            [RdfName(":TNullableFloat")]
            public Nullable<float> TNullableFloat { get; set; }
            [RdfName(":TNullableDouble")]
            public Nullable<double> TNullableDouble { get; set; }
        }
        static IGraph SerializeToGraph(object serializableEntity, string rdfSubject)
        {
            var graph = new Graph();
            graph.BaseUri = new Uri(_cpNamespace);
            graph.NamespaceMap.AddNamespace("", new Uri(_cpNamespace));
            graph.NamespaceMap.AddNamespace("cpDishes", new Uri(_cpDishNamespace));
            var serializer = new TripleSerializer(graph, graph);

            serializer.Serialize2RDF(serializableEntity, rdfSubject);
            return graph;

        }

        static void Main(string[] args)
        {


/*
            var ListTestInstance = new ListDatatypes()
            {
                TDoubleList = new List<double>(){ 10.11, 12.13 },
            };

            var initialSubject = "cpDishes:DoubleListTestInstance";
            var graph = SerializeToGraph(ListTestInstance, initialSubject);
            var rdfTestSubject = graph.CreateUriNode(initialSubject);

            var double1 = ListTestInstance.TDoubleList[0];
            var double2 = ListTestInstance.TDoubleList[1];
            Triple doubleTriple1 = new Triple(
                rdfTestSubject,
                graph.CreateUriNode(":TDoubleList"),
                double1.ToLiteral(graph)
            );
            Triple doubleTriple2 = new Triple(
                rdfTestSubject,
                graph.CreateUriNode(":TDoubleList"),
                double2.ToLiteral(graph)
            );
            var hasTriple1 = graph.ContainsTriple(doubleTriple1);
            var hasTriple2 = graph.ContainsTriple(doubleTriple2);
 */

            var AtomicTestInstance1 = new NuclearDatatypes()
            {
                TInteger = 65532,
                TString = "FooBar",
                TFloat = 3.14F,
                TDouble = 2.76,
                TNullableInt = null,
                TNullableFloat = null,
                TNullableDouble = null
            };
            var AtomicTestInstance2 = new NuclearDatatypes()
            {
                TInteger = 67890,
                TString = "FooBar",
                TFloat = 1.23F,
                TDouble = 4.56,
            };

            var ListTestInstance2 = new ListDatatypes()
            {
                TNuclearDatatypeList =
                new List<NuclearDatatypes> (){
                    AtomicTestInstance1,
                    AtomicTestInstance2
                },
            };

            var initialSubject2 = "cpDishes:NuclearDatatypeListTestInstance2";
            var graph2 = SerializeToGraph(ListTestInstance2, initialSubject2);
            var rdfTestSubject2 = graph2.CreateUriNode(initialSubject2);


        }  
    }
}