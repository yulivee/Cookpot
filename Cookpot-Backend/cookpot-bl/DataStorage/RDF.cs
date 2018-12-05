using System;
using System.Collections.Generic;
using cookpot.bl;
using cookpot.bl.DataModel;
using VDS.RDF;
using VDS.RDF.Query;
using VDS.RDF.Parsing;
using VDS.RDF.Storage;
using System.Text;

namespace cookpot.bl.DataStorage
{
    public class RDF : IManager<Dish>
    {

        private readonly string _fusekiURI = "https://fuseki.voiding-warranties.de/cookpot/data";
        private readonly string _graphURI = "";

        private FusekiConnector _fuseki;

        public RDF(string uri, string graphURI = "")
        {
            this._fusekiURI = uri;
            this._graphURI = graphURI;
            this._fuseki = new FusekiConnector(_fusekiURI);
        } 

        public Dish Create(Dish dish)
        {
            Object x = null;
            
            var SparqlUpdateStatement = new StringBuilder();
            SparqlUpdateStatement.Append("PREFIX cp: <http://voiding-warranties.de/cookpot/1.0#>"); //   .Append(obj?.Title??"");
            SparqlUpdateStatement.Append("INSERT DATA {"); 
            SparqlUpdateStatement.Append("a cp:Dish;"); 
            SparqlUpdateStatement.Append("cp:title "+dish.Title?.ToString()+";" ?? null);
            SparqlUpdateStatement.Append("cp:description "+dish.Description?.ToString()+";" ?? null);
            SparqlUpdateStatement.Append("cp:source "+dish.Source?.ToString()+";" ?? null);
            SparqlUpdateStatement.Append("cp:author "+dish.Author?.ToString()+";" ?? null);
            SparqlUpdateStatement.Append("cp:servings "+dish.ServingSize?.ToString()+";" ?? null);

/*
PREFIX cp: <http://voiding-warranties.de/cookpot/1.0#>

INSERT DATA 
{ 
       cp:testin cp:name "Testing"@en.
       cp:serious_testing cp:name "Very Serious Testing"@en.
      
      _:TestRecipe001
      a cp:Dish;
        cp:title "Fancy Testrecipe"@en;
        cp:description "This is a Testrecipe to get fuseki sweating."@en;
        cp:origin cp:testin, cp:serious_testing;
}
 */


    /*        
    _:BangBangChicken 
        # a cp:Dish;
        # cp:title "Bang Bang Chicken: The Authentic Sichuan Version"@en;
        # cp:description "This Bang Bang Chicken recipe is for the authentic Sichuan version of the dish, rather than the Americanized fried version, tossed in a spicy, tangy sauce."@en;
        # cp:source "https://thewoksoflife.com/2018/08/bang-bang-chicken-sichuan/"@en;
        cp:origin cp:china, cp:sichuan;
        cp:cuisine cp:chinese;
        cp:recipeType "Chicken","Poultry";
        # cp:author "Judy";
        # cp:servings 3;
        cp:servingsMin 2;
        cp:servingsMax 4;
        cp:ingredient [
            a rdf:Ingredient;
            cp:ingredientName "large chicken breast"@en;
            cp:ingredientAmount 1;
            cp:ingredientUnit cp:lb;
            cp:ingredientMeasure 0.5 
        ], 
        [ 
            a rdf:Ingredient;
            cp:ingredientName "ginger"@en;
            cp:ingredientUnit cp:slice;
            cp:ingredientMeasure 3
        ],
        [ 
            a rdf:Ingredient;
            cp:ingredientName "scallion";
            cp:ingredientAmount 2
        ],
        [ 
            a rdf:Ingredient;
            cp:ingredientName "seedless cucumber, julienned";
            cp:ingredientAmount 0.5
        ],
        [ 
            a rdf:Ingredient;
            cp:ingredientName "chicken stock";
            cp:ingredientUnit cp:cup;
            cp:ingredientMeasure 0.5
        ],
        [ 
            a rdf:Ingredient;
            cp:ingredientName "light soy sauce";
            cp:ingredientUnit cp:tbsp;
            cp:ingredientMeasure 2
        ],
        [ 
            a rdf:Ingredient;
            cp:ingredientName "chinese dark vinegar";
            cp:ingredientUnit cp:tbsp;
            cp:ingredientMeasure 4
        ],
        [ 
            a rdf:Ingredient;
            cp:ingredientName "sugar";
            cp:ingredientUnit cp:tbsp;
            cp:ingredientMeasure 2
        ],
        [ 
            a rdf:Ingredient;
            cp:ingredientName "sesame oil";
            cp:ingredientUnit cp:tbsp;
            cp:ingredientMeasure 1.5
        ],
        [ 
            a rdf:Ingredient;
            cp:ingredientName "chili oil";
            cp:ingredientUnit cp:tbsp;
            cp:ingredientMeasure 1
        ],
        [ 
            a rdf:Ingredient;
            cp:ingredientName "toasted sesame seeds";
            cp:ingredientUnit cp:tbsp; 
            cp:ingredientMeasure 2
        ],
        [ 
            a rdf:Ingredient;
            cp:ingredientName "ground sichuan pepper";
            cp:ingredientUnit cp:tsp;
            cp:ingredientMeasure 1
        ],
        [ 
            a rdf:Ingredient;
            cp:ingredientName "salt";
            cp:ingredientUnit cp:tsp;
            cp:ingredientMeasure 0.5
        ];
        cp:recipe [
            a rdf:Seq;
            rdf:_1 [
                a cp:Recipe;
                cp:durationTime 15; 
                cp:durationUnit "min";
                cp:recipeType "Preparation";
                cp:step [
                    a rdf:Seq;
                    rdf:_1 [
                        a cp:Step;
                        cp:stepDescription "First, poach the chicken. In a small pot, add 2 cups water, 3 slices ginger and 1 scallion. Bring it to a boil, then add in the chicken breast. Once the water boils again, put the lid on and turn the heat to the lowest setting. Cook for 10-12 minutes. The chicken breast is done if the juice comes out clear when you poke the middle with a chopstick. Transfer the chicken breast to an ice bath to stop the cooking process and keep the chicken moist. Don’t discard the cooking water, as we’ll be using it later in the recipe."
                    ]
                ]
            ];
            rdf:_2 [
                a cp:Recipe;
                cp:durationTime 20;
                cp:durationUnit "min";
                cp:step [
                    a rdf:Seq;
                    rdf:_1 [
                    a cp:Step;
                    cp:stepDescription "Second, assemble the plate. Julienne the cucumber and spread it in an even layer on a shallow plate. Now, hammer the chicken with a rolling pin to flatten the meat and break it up into shreds. Layer the chicken on top of the cucumber."
                    ];
                    rdf:_2 [
                        a cp:Step;
                        cp:stepDescription "Third, prepare the sauce. Mix together the following: ½ cup chicken stock (i.e., the cooking water from the chicken), 2 tablespoons light soy sauce, 4 teaspoons Chinese dark vinegar, 2 tablespoons sugar, 1½ tablespoons sesame oil, 1 tablespoon chili oil (or to taste), 2 tablespoons toasted sesame seeds, 1 teaspoon ground Sichuan peppercorn, ½ teaspoon salt, and 2 tablespoons finely chopped scallions."
                    ];
                    rdf:_3 [
                        a cp:Step;
                        cp:stepDescription "Finally, pour the sauce over the chicken and cucumber, and serve. Toss the chicken and cucumber to coat with the sauce just before you’re ready to dig in!"
                    ]
                ]
            ]
        ].
*/
            SparqlUpdateStatement.AppendLine("}");

}

            var z = x == null ? "nope" : x.ToString();

            var y = x?.ToString() ?? "nope";

            this._fuseki.Update(SparqlUpdateStatement.ToString());
            throw new NotImplementedException();
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
            //Execute a raw SPARQL Query
            //Should get a SparqlResultSet back from a SELECT query
            Object results = this._fuseki.ExecuteQuery("SELECT * WHERE { { ?s ?p ?o } LIMIT 10 }");
            if (results is SparqlResultSet)
            {
                //Print out the Results
                SparqlResultSet rset = (SparqlResultSet)results;
                string rawQueryResult ="";
                foreach (SparqlResult result in rset)
                {
                    rawQueryResult = rawQueryResult + result.ToString() + "\n";
                }

                return rawQueryResult;
            }
            else
            {
                throw Exception();
            }


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