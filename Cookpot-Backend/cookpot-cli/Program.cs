using System;
using cookpot.al;
using cookpot.bl;
using cookpot.bl.DataStorage;
using cookpot.bl.DataModel;
using System.Collections.Generic;

namespace cookpot.cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var sparqlEndpoint = new RDF("https://fuseki.voiding-warranties.de/cookpot/data");
            sparqlEndpoint.debug = true;

            var testIngredient = new Ingredient(){
                Name = "Magic Ingredient",
                Amount = 1,
            };

            var testIngredient2 = new Ingredient(){
                Name = "Second Magic Ingredient",
                Amount = 3,
            };

            var testStep = new Step(){ Description = "Do this", };
            var testStep2 = new Step(){ Description = "Do that", };
            var testStep3 = new Step(){ Description = "Now Do that", };

            var testRecipe = new Recipe() {
                DurationTime = 30,
                DurationUnit = "min",
                RecipeType = "Preparation",

                Steps = new List<Step>(){
                    testStep,
                    testStep2,
                    testStep3
                }
            };

            var testRecipe2 = new Recipe() {
                DurationTime = 45,
                DurationUnit = "min",
                RecipeType = "Baking-Time",
            };


            var testDish = new Dish() {
                Title = "Another Testrecipe",
                Description = "A very awesome Testdish which is very tasty",
                Author = "Sandra Schuhmacher",
                Source = "my testing cookbook",
                ServingSize = 3,

                Ingredients = new List<Ingredient>() {
                        testIngredient,
                        testIngredient2
                },

                Recipes = new List<Recipe>() {
                    testRecipe,
                    testRecipe2
                },

            };

            sparqlEndpoint.Create(testDish);
        }  
    }
}