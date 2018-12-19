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
        public string Name { get; set; }
    }
}
