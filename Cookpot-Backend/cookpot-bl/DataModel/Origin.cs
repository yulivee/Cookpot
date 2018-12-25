using System;
using System.Collections.Generic;

namespace cookpot.bl.DataModel
{
    public class Origin
    {
        [RdfName(":Origin")]
        public string Name { get; set; }
    }

    public class OriginCountry : Origin 
    {
        [RdfName(":OriginCountry")]
        public string Name { get; set; }

    }

    public class OriginRegion : Origin 
    {
        [RdfName(":OriginRegion")]
        public string Name { get; set; }

    }
    public class OriginCity : Origin 
    {
        [RdfName(":OriginCity")]
        public string Name { get; set; }

    }
}

