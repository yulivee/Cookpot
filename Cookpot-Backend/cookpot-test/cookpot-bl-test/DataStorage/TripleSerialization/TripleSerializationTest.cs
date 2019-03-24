using System;
using Xunit;
using cookpot.bl;
using cookpot.bl.DataStorage.TripleSerialization;
using cookpot.bl.DataModel;
using System.Collections.Generic;
using VDS.RDF;
using VDS.RDF.Writing;
using VDS.RDF.Parsing;

namespace cookpot.bl.DataStorage.TripleSerialization
{
    public class TripleSerializationTest
    {
        private string _cpNamespace = "http://voiding-warranties.de/cookpot/1.0#";
        private string _cpDishNamespace = "http://voiding-warranties.de/cookpot/1.0/Dishes/";

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

        private IGraph SerializeToGraph(object serializableEntity, string rdfSubject)
        {
            var graph = new Graph();
            graph.BaseUri = new Uri(_cpNamespace);
            graph.NamespaceMap.AddNamespace("", new Uri(_cpNamespace));
            graph.NamespaceMap.AddNamespace("cpDishes", new Uri(_cpDishNamespace));
            var serializer = new TripleSerializer(graph, graph);

            serializer.Serialize2RDF(serializableEntity, rdfSubject);
            return graph;

        }

        [Fact]
        public void AtomicDatatypes()
        {
            var AtomicTestInstance = new NuclearDatatypes()
            {
                TInteger = 65532,
                TString = "FooBar",
                TFloat = 3.14F,
                TDouble = 2.76,
                TNullableInt = null,
                TNullableFloat = null,
                TNullableDouble = null
            };

            var initialSubject = "cpDishes:TestInstance";
            var graph = SerializeToGraph(AtomicTestInstance, initialSubject);
            var rdfTestSubject = graph.CreateUriNode(initialSubject);

            Triple intTriple = new Triple(
                rdfTestSubject,
                graph.CreateUriNode(":TInteger"),
                AtomicTestInstance.TInteger.ToLiteral(graph)
            );
            Assert.True(graph.ContainsTriple(intTriple), "Integer Triple contained in graph");

            Triple stringTriple = new Triple(
                rdfTestSubject,
                graph.CreateUriNode(":TString"),
                AtomicTestInstance.TString.ToLiteral(graph)
            );
            Assert.True(graph.ContainsTriple(stringTriple), "String Triple contained in graph");

            Triple floatTriple = new Triple(
                rdfTestSubject,
                graph.CreateUriNode(":TFloat"),
                AtomicTestInstance.TFloat.ToLiteral(graph)
            );
            Assert.True(graph.ContainsTriple(floatTriple), "Float Triple contained in graph");

            Triple doubleTriple = new Triple(
                rdfTestSubject,
                graph.CreateUriNode(":TDouble"),
                AtomicTestInstance.TDouble.ToLiteral(graph)
            );
            Assert.True(graph.ContainsTriple(doubleTriple), "Double Triple contained in graph");
        }
                /*
                TIntegerList = new List<int>();
                TStringList = new List<string>();
                TFloatList = new List<float>();
                TDoubleList = new List<double>();
                 */

        [Fact]
        public void DoubleListDatatype()
        {
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
            Assert.True(graph.ContainsTriple(doubleTriple1), "Double Triple 1 contained in graph");
            Assert.True(graph.ContainsTriple(doubleTriple2), "Double Triple 2 contained in graph");
        }

        [Fact(Skip = "we need this later")]
        public void FloatListDatatype()
        {
            var ListTestInstance = new ListDatatypes()
            {
                TFloatList = new List<float>() { 12.34F, 56.78F },
            };

            var initialSubject = "cpDishes:FloatListTestInstance";
            var graph = SerializeToGraph(ListTestInstance, initialSubject);
            var rdfTestSubject = graph.CreateUriNode(initialSubject);

            var float1 = ListTestInstance.TFloatList[0];
            var float2 = ListTestInstance.TFloatList[1];
            Triple floatTriple1 = new Triple(
                rdfTestSubject,
                graph.CreateUriNode(":TFloatList"),
                float1.ToLiteral(graph)
            );
            Triple floatTriple2 = new Triple(
                rdfTestSubject,
                graph.CreateUriNode(":TFloatList"),
                float2.ToLiteral(graph)
            );
            Assert.True(graph.ContainsTriple(floatTriple1), "Float Triple 1 contained in graph");
            Assert.True(graph.ContainsTriple(floatTriple2), "Float Triple 2 contained in graph");
        }

        [Fact(Skip = "we need this later")]
        public void StringListDatatype()
        {
            var ListTestInstance = new ListDatatypes()
            {
                TStringList = new List<string>() { "String1", "String2" },
            };

            var initialSubject = "cpDishes:StringListTestInstance";
            var graph = SerializeToGraph(ListTestInstance, initialSubject);
            var rdfTestSubject = graph.CreateUriNode(initialSubject);

            var string1 = ListTestInstance.TStringList[0];
            var string2 = ListTestInstance.TStringList[1];
            Triple stringTriple1 = new Triple(
                rdfTestSubject,
                graph.CreateUriNode(":TStringList"),
                string1.ToLiteral(graph)
            );
            Triple stringTriple2 = new Triple(
                rdfTestSubject,
                graph.CreateUriNode(":TStringList"),
                string2.ToLiteral(graph)
            );
            Assert.True(graph.ContainsTriple(stringTriple1), "String Triple 1 contained in graph");
            Assert.True(graph.ContainsTriple(stringTriple2), "String Triple 2 contained in graph");
        }

        [Fact]

        public void IntegerListDatatype()
        {
            var ListTestInstance = new ListDatatypes()
            {
                TIntegerList = new List<int>() { 123, 456 },
            };

            var initialSubject = "cpDishes:IntegerListTestInstance";
            var graph = SerializeToGraph(ListTestInstance, initialSubject);
            var rdfTestSubject = graph.CreateUriNode(initialSubject);

            var int1 = ListTestInstance.TIntegerList[0];
            var int2 = ListTestInstance.TIntegerList[1];
            Triple intTriple1 = new Triple(
                rdfTestSubject,
                graph.CreateUriNode(":TIntegerList"),
                int1.ToLiteral(graph)
            );
            Triple intTriple2 = new Triple(
                rdfTestSubject,
                graph.CreateUriNode(":TIntegerList"),
                int2.ToLiteral(graph)
            );
            Triple ListTriple = new Triple(
                rdfTestSubject,
                graph.CreateUriNode("rdf:type"),
                ("List").ToLiteral(graph)
            );
            Assert.True(graph.ContainsTriple(intTriple1), "Integer Triple 1 contained in graph");
            Assert.True(graph.ContainsTriple(intTriple2), "Integer Triple 2 contained in graph");
            Assert.True(graph.ContainsTriple(ListTriple), "List Triple contained in graph");

            Console.WriteLine("All triples from IGraph.Triples:");
            foreach (var triple in graph.Triples) {
                Console.WriteLine("- S: " + string.Format("{0} ({1})", triple.Subject, triple.Subject.GetType().Name));
                Console.WriteLine("  P: " + string.Format("{0} ({1})", triple.Predicate, triple.Predicate.GetType().Name));
                Console.WriteLine("  O: " + string.Format("{0} ({1})", triple.Subject, triple.Subject.GetType().Name));
            }
            Console.WriteLine();
        }
/*
        [Fact(Skip = "we need this later")]
        public void NuclearDatatypeListDatatype()
        {
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

            var ListTestInstance = new ListDatatypes()
            {
                TNuclearDatatypeList =
                new List<NuclearDatatypes> (){
                    AtomicTestInstance1,
                    AtomicTestInstance2
                },
            };

            var initialSubject = "cpDishes:NuclearDatatypeListTestInstance";
            var graph = SerializeToGraph(ListTestInstance, initialSubject);
            var rdfTestSubject = graph.CreateUriNode(initialSubject);

            Triple objectTriple1 = new Triple(
                rdfTestSubject,
                graph.CreateUriNode(":TNuclearDatatypeList"),
                ListTestInstance.TNuclearDatatypeList.ToLiteral(graph)
            );
            Triple objectTriple2 = new Triple(
                rdfTestSubject,
                graph.CreateUriNode(":TNuclearDatatypeList"),
                ListTestInstance.TNuclearDatatypeList.ToLiteral(graph)
            );
            Assert.True(graph.ContainsTriple(objectTriple1), "NuclearDatatype Triple 1 contained in graph");
            Assert.True(graph.ContainsTriple(objectTriple2), "NuclearDatatype Triple 2 contained in graph");
        }




*/
        [Fact(Skip = "we need this later")]
        public void SerializeDish()
        {
            string _cpNamespace = "http://voiding-warranties.de/cookpot/1.0#";
            string _cpDishNamespace = "http://voiding-warranties.de/cookpot/1.0/Dishes/";

            var dish = CreateTestDish();

            var graph = new Graph();
            graph.BaseUri = new Uri(_cpNamespace);
            graph.NamespaceMap.AddNamespace("", new Uri(_cpNamespace));
            graph.NamespaceMap.AddNamespace("cpDishes", new Uri(_cpDishNamespace));
            var serializer = new TripleSerializer(graph, graph);

            serializer.Serialize2RDF(dish);

            //            var triples = graph.

            //Assert.Equal(graph, graphFromFile);

        }

        public Dish CreateTestDish()
        {

            var China = new OriginCountry() { Name = "China", };
            var Sichuan = new OriginRegion() { Name = "Sichuan", };
            var Chinese = new Cuisine() { Name = "chinese", };
            var Pounds = new UnitofMeasurement() { Name = "lb", };
            var Tablespoon = new UnitofMeasurement() { Name = "tbsp", };
            var Teaspoon = new UnitofMeasurement() { Name = "tsp", };
            var Cup = new UnitofMeasurement() { Name = "cup", };
            var Slice = new UnitofMeasurement() { Name = "slice", };
            var Ingredient1 = new Ingredient()
            {
                Name = "large chicken breast", //@en Amount = 1,
                Unit = Pounds,
                Measure = 0.5,
            };
            var Ingredient2 = new Ingredient() { Name = "toasted sesame seed", Unit = Tablespoon, Measure = 2, };

            var Ingredient3 = new Ingredient() { Name = "chili oil", Unit = Tablespoon, Measure = 1, };
            var Ingredient4 = new Ingredient() { Name = "sesame oil", Unit = Tablespoon, Measure = 1.5, };
            var Ingredient5 = new Ingredient() { Name = "sugar", Unit = Tablespoon, Measure = 2, };
            var Ingredient6 = new Ingredient() { Name = "chinese dark vinegar", Unit = Tablespoon, Measure = 4, };
            var Ingredient7 = new Ingredient() { Name = "light soy sauce", Unit = Tablespoon, Measure = 2, };
            var Ingredient8 = new Ingredient() { Name = "chicken stock", Unit = Cup, Measure = 0.5, };
            var Ingredient9 = new Ingredient() { Name = "seedless cucumber, julienned", Amount = 0.5, };
            var Ingredient10 = new Ingredient() { Name = "scallion", Unit = Tablespoon, Measure = 2, };
            var Ingredient11 = new Ingredient() { Name = "ginger", Unit = Slice, Measure = 3, };
            var Ingredient12 = new Ingredient() { Name = "ground sichuan pepper", Unit = Teaspoon, Measure = 1, };
            var Ingredient13 = new Ingredient() { Name = "salt", Unit = Teaspoon, Measure = 0.5, };
            var prepStep = new Step() { Description = "First, poach the chicken. In a small pot, add 2 cups water, 3 slices ginger and 1 scallion. Bring it to a boil, then add in the chicken breast. Once the water boils again, put the lid on and turn the heat to the lowest setting. Cook for 10-12 minutes. The chicken breast is done if the juice comes out clear when you poke the middle with a chopstick. Transfer the chicken breast to an ice bath to stop the cooking process and keep the chicken moist. Don’t discard the cooking water, as we’ll be using it later in the recipe.", };
            var secondStep = new Step() { Description = "Second, assemble the plate. Julienne the cucumber and spread it in an even layer on a shallow plate. Now, hammer the chicken with a rolling pin to flatten the meat and break it up into shreds. Layer the chicken on top of the cucumber.", };
            var thirdStep = new Step() { Description = "Third, prepare the sauce. Mix together the following: ½ cup chicken stock (i.e., the cooking water from the chicken), 2 tablespoons light soy sauce, 4 teaspoons Chinese dark vinegar, 2 tablespoons sugar, 1½ tablespoons sesame oil, 1 tablespoon chili oil (or to taste), 2 tablespoons toasted sesame seeds, 1 teaspoon ground Sichuan peppercorn, ½ teaspoon salt, and 2 tablespoons finely chopped scallions.", };
            var finalStep = new Step() { Description = "Finally, pour the sauce over the chicken and cucumber, and serve. Toss the chicken and cucumber to coat with the sauce just before you’re ready to dig in!", };
            var preparationRecipe = new Recipe()
            {
                DurationTime = 15,
                DurationUnit = "min",
                RecipeName = "Preparation",
                Steps = new List<Step>() { prepStep, }
            };

            var mainRecipe = new Recipe()
            {
                DurationTime = 20,
                DurationUnit = "min",
                Steps = new List<Step>() { secondStep, thirdStep, finalStep }
            };


            var BangBangChicken = new Dish()
            {
                Title = "Bang Bang Chicken: The Authentic Sichuan Version",
                Description = "This Bang Bang Chicken recipe is for the authentic Sichuan version of the dish, rather than the Americanized fried version, tossed in a spicy, tangy sauce.",
                Author = "Judy",
                Source = "https://thewoksoflife.com/2018/08/bang-bang-chicken-sichuan/",
                ServingSize = 3,
                ServingSizeMin = 2,
                ServingSizeMax = 4,

                Recipes = new List<Recipe>() { preparationRecipe, mainRecipe },

                Ingredients = new List<Ingredient>() { Ingredient1, Ingredient2, Ingredient3, Ingredient4, Ingredient5, Ingredient6, Ingredient7, Ingredient8, Ingredient9, Ingredient10, Ingredient11, Ingredient12, Ingredient13, },

                Origins = new List<Origin>() { China, Sichuan },

                RecipeTypes = new List<string>() { "Chicken", "Poultry" },

                Cuisines = new List<Cuisine>() { Chinese },
            };

            return BangBangChicken;

        }


    }
}
