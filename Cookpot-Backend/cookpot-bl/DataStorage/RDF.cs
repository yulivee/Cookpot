using System;
using System.Collections.Generic;
using cookpot.bl;
using VDS.RDF;
using VDS.RDF.Query;

namespace cookpot.bl.DataStorage
{
    public class RDF : IManager<string>
    {

        private const string _fusekiURI = "https://fuseki.voiding-warranties.de/cookpot/data";
        private const string _graphURI = "";

        private FusekiConnector _fuseki;

        public RDF()
        {
            this._fuseki = new FusekiConnector(_fusekiURI);
        }

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

        public string Read(string obj)
        {
            //Execute a raw SPARQL Query
            //Should get a SparqlResultSet back from a SELECT query
            Object results = this._fuseki.ExecuteQuery("SELECT * WHERE { { ?s ?p ?o } LIMIT 10 }");
            if (results is SparqlResultSet)
            {
                //Print out the Results
                SparqlResultSet rset = (SparqlResultSet)results;
                string rawQueryResult;
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