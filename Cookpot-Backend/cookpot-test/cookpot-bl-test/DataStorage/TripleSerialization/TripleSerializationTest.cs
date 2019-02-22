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
        [Fact]
        public void SerializeDish()
        {
        string _fusekiURI = "https://fuseki.voiding-warranties.de/cookpot-owl/data";
        string _cpNamespace = "http://voiding-warranties.de/cookpot/1.0#";
        string _cpDishNamespace = "http://voiding-warranties.de/cookpot/1.0/Dishes/";
        string _graphURI = "";

            var dish = CreateTestDish();

            var graph = new Graph();
            graph.BaseUri = new Uri(_cpNamespace);
            graph.NamespaceMap.AddNamespace("", new Uri(_cpNamespace));
            graph.NamespaceMap.AddNamespace("cpDishes", new Uri(_cpDishNamespace));
			var serializer = new TripleSerializer(graph, graph);

			serializer.Serialize2RDF(dish);

            var triples = graph

            Assert.Equal(graph, graphFromFile);


        }

        public Dish CreateTestDish () {

            var China = new OriginCountry() { Name = "China", };
            var Sichuan = new OriginRegion() { Name = "Sichuan", };
            var Chinese = new Cuisine() { Name = "chinese", };
            var Pounds = new UnitofMeasurement() { Name = "lb", };
            var Tablespoon = new UnitofMeasurement() { Name = "tbsp", };
            var Teaspoon = new UnitofMeasurement() { Name = "tsp", };
            var Cup = new UnitofMeasurement() { Name = "cup", };
            var Slice = new UnitofMeasurement() { Name = "slice", };
            var Ingredient1 = new Ingredient(){ Name = "large chicken breast", //@en Amount = 1,
                Unit = Pounds, Measure = 0.5, };
            var Ingredient2 = new Ingredient(){ Name = "toasted sesame seed", Unit = Tablespoon, Measure = 2, };

            var Ingredient3 = new Ingredient(){ Name = "chili oil", Unit = Tablespoon, Measure = 1, };
            var Ingredient4 = new Ingredient(){ Name = "sesame oil", Unit = Tablespoon, Measure = 1.5, };
            var Ingredient5 = new Ingredient(){ Name = "sugar", Unit = Tablespoon, Measure = 2, };
            var Ingredient6 = new Ingredient(){ Name = "chinese dark vinegar", Unit = Tablespoon, Measure = 4, };
            var Ingredient7 = new Ingredient(){ Name = "light soy sauce", Unit = Tablespoon, Measure = 2, };
            var Ingredient8 = new Ingredient(){ Name = "chicken stock", Unit = Cup, Measure = 0.5, };
            var Ingredient9 = new Ingredient(){ Name = "seedless cucumber, julienned", Amount = 0.5, };
            var Ingredient10 = new Ingredient(){ Name = "scallion", Unit = Tablespoon, Measure = 2, };
            var Ingredient11 = new Ingredient(){ Name = "ginger", Unit = Slice, Measure = 3, }; 
            var Ingredient12 = new Ingredient(){ Name = "ground sichuan pepper", Unit = Teaspoon, Measure = 1, };
            var Ingredient13 = new Ingredient(){ Name = "salt", Unit = Teaspoon, Measure = 0.5, };
            var prepStep = new Step(){ Description = "First, poach the chicken. In a small pot, add 2 cups water, 3 slices ginger and 1 scallion. Bring it to a boil, then add in the chicken breast. Once the water boils again, put the lid on and turn the heat to the lowest setting. Cook for 10-12 minutes. The chicken breast is done if the juice comes out clear when you poke the middle with a chopstick. Transfer the chicken breast to an ice bath to stop the cooking process and keep the chicken moist. Don’t discard the cooking water, as we’ll be using it later in the recipe.", };
            var secondStep = new Step(){ Description = "Second, assemble the plate. Julienne the cucumber and spread it in an even layer on a shallow plate. Now, hammer the chicken with a rolling pin to flatten the meat and break it up into shreds. Layer the chicken on top of the cucumber.", };
            var thirdStep = new Step(){ Description = "Third, prepare the sauce. Mix together the following: ½ cup chicken stock (i.e., the cooking water from the chicken), 2 tablespoons light soy sauce, 4 teaspoons Chinese dark vinegar, 2 tablespoons sugar, 1½ tablespoons sesame oil, 1 tablespoon chili oil (or to taste), 2 tablespoons toasted sesame seeds, 1 teaspoon ground Sichuan peppercorn, ½ teaspoon salt, and 2 tablespoons finely chopped scallions.", };
            var finalStep = new Step(){ Description = "Finally, pour the sauce over the chicken and cucumber, and serve. Toss the chicken and cucumber to coat with the sauce just before you’re ready to dig in!", };
            var preparationRecipe = new Recipe() { DurationTime = 15, DurationUnit = "min", RecipeName = "Preparation",
                Steps = new List<Step>(){ prepStep, } };

            var mainRecipe = new Recipe() { DurationTime = 20, DurationUnit = "min",
                Steps = new List<Step>(){ secondStep, thirdStep, finalStep } };


            var BangBangChicken = new Dish() {
                Title = "Bang Bang Chicken: The Authentic Sichuan Version",
                Description = "This Bang Bang Chicken recipe is for the authentic Sichuan version of the dish, rather than the Americanized fried version, tossed in a spicy, tangy sauce.",
                Author = "Judy",
                Source = "https://thewoksoflife.com/2018/08/bang-bang-chicken-sichuan/",
                ServingSize = 3,
                ServingSizeMin = 2,
                ServingSizeMax = 4,

                Recipes = new List<Recipe>() { preparationRecipe, mainRecipe },

                Ingredients = new List<Ingredient>() { Ingredient1, Ingredient2, Ingredient3, Ingredient4, Ingredient5, Ingredient6, Ingredient7, Ingredient8, Ingredient9, Ingredient10, Ingredient11, Ingredient12, Ingredient13, },

                Origins = new List<Origin>(){ China, Sichuan },

                RecipeTypes = new List<string>(){ "Chicken", "Poultry" },

                Cuisines = new List<Cuisine>() { Chinese },
            };

            return BangBangChicken;

        }


    }
}
