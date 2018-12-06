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

            var SparqlUpdateStatement = new StringBuilder();
            SparqlUpdateStatement.AppendLine("PREFIX cp: <http://voiding-warranties.de/cookpot/1.0#>").AppendLine("INSERT DATA {").AppendLine("a cp:Dish;");
            // not looking nice. Ask for better method for doing this
            SparqlUpdateStatement.conditionalAppend("title ", dish.Title);
            SparqlUpdateStatement.conditionalAppend("description ", dish.Description);
            SparqlUpdateStatement.conditionalAppend("source ", dish.Source);
            SparqlUpdateStatement.conditionalAppend("author ", dish.Author);
            // TODO: how do I test if ServingSize is set?
            SparqlUpdateStatement.conditionalAppend("servings ", dish.ServingSize);
            SparqlUpdateStatement.conditionalAppend("servings ", dish.ServingSizeMin);
            SparqlUpdateStatement.conditionalAppend("servings ", dish.ServingSizeMax);
            // origin china, sichuan;
            // cuisine chinese;
            // recipeType "Chicken","Poultry";
            if (dish.Ingredients != null)
            {
                SparqlUpdateStatement.Append("ingredient");

                var listCount = 0;
                foreach (Ingredient ingredient in dish.Ingredients)
                {
                    SparqlUpdateStatement.AppendLine("[").AppendLine("a rdf:Ingredient;");
                    SparqlUpdateStatement.conditionalAppend("ingredientName ", ingredient.Name);
                    SparqlUpdateStatement.conditionalAppend("ingredientAmount ", ingredient.Amount);
                    SparqlUpdateStatement.conditionalAppend("ingredientMeasure ", ingredient.Measure);
                }
                    // ingredientUnit lb; ??
                    SparqlUpdateStatement.Append("]");
                    listCount++;
                    SparqlUpdateStatement.AppendLine(listCount == dish.Ingredients.Count ? ";" : ",");
            }

            if (dish.Recipes != null)
            {
                SparqlUpdateStatement.Append("recipe");

                var listCount = 0;
                foreach (Recipe recipe in dish.Recipes)
                {
                    SparqlUpdateStatement.AppendLine("[").AppendLine("a rdf:Seq;").AppendLine("rdf:_" + listCount + " [").AppendLine("a Recipe;");
                    SparqlUpdateStatement.conditionalAppend("durationTime " , recipe.DurationTime);
                    SparqlUpdateStatement.conditionalAppend("durationUnit " , recipe.DurationUnit);
                    SparqlUpdateStatement.conditionalAppend("recipeType " , recipe.RecipeType);

                    if (recipe.Steps != null)
                    {
                        SparqlUpdateStatement.Append("step");
                        var stepCount = 0;
                        foreach (Step step in recipe.Steps)
                        {
                            SparqlUpdateStatement.AppendLine("[").AppendLine("a rdf:Seq;").AppendLine("rdf:_" + stepCount + " [").AppendLine("a Step;");
                            SparqlUpdateStatement.conditionalAppend("stepDescription ", step.Description);
                            SparqlUpdateStatement.Append("]");
                            stepCount++;
                            SparqlUpdateStatement.AppendLine(stepCount == recipe.Steps.Count ? ";" : ",");

                        }
                    }
                    SparqlUpdateStatement.Append("]");
                    listCount++;
                    SparqlUpdateStatement.AppendLine(listCount == dish.Recipes.Count ? ";" : ",");
                }

                SparqlUpdateStatement.AppendLine("}");

                if (this.debug == true) { Console.WriteLine(SparqlUpdateStatement.ToString()); return dish; }

                this._fuseki.Update(SparqlUpdateStatement.ToString());
            }

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