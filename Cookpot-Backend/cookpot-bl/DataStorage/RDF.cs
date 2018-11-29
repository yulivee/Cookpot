using System;
using System.Collections.Generic;
using VDS.RDF;
using VDS.RDF.Query;
using VDS.RDF.Storage;

namespace cookpot.bl.DataStorage
{
    public class RDF : IDBAccess<string>
    {
        public RDF()
        {
            this._fuseki = new FusekiConnector(_fusekiURI);

        }
        
        private const string _fusekiURI = "https://fuseki.voiding-warranties.de/cookpot/data";
        private FusekiConnector _fuseki;

        public string Create(string obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> Create(IEnumerable<string> objs)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string obj)
        {
            throw new NotImplementedException();
        }

        public SparqlResultSet Read(string obj)
        {
            Object results = this._fuseki.Query(
                                                "SELECT ?subject ?predicate ?object WHERE {" +
                                                " ?subject ?predicate ?object }" +
                                                " LIMIT 25");
                if (results is SparqlResultSet)
                {
                        //Print the results
                        SparqlResultSet rset = (SparqlResultSet)results;
                        return rset;
                }
                else
                {
                        throw new Exception("Did not get a SPARQL Result Set as expected");
                }

        }

        public IEnumerable<string> Read(IEnumerable<string> objs)
        {
            throw new NotImplementedException();
        }

        public string Update(string obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> Update(IEnumerable<string> objs)
        {
            throw new NotImplementedException();
        }
    }
}
