using System;
using System.Collections.Generic;

namespace cookpot.bl.DataModel
{
    public class UnitofMeasurement
    {
        /* Get all UnitsofMeasurement individuals
        PREFIX cp: <http://voiding-warranties.de/cookpot/1.0#>

        SELECT ?s ?p ?o WHERE {
        ?s a cp:MeasureUnit.
        }
         */
        public const Dictionary<string,string>{  { "Cup" => "cp" }, { "Liter" => "l" },  { "Milliliter" => "ml" }, { "Slice" => "slice" }, { "Tablespoon" => "tbsp" }, { "Teaspoon" => "tsp" } } Unit 
        public string Name { get; set; }
    }
}
