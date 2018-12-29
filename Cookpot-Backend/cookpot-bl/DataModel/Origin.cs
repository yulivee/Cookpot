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
        public new string Name { get; set; }

    }

    public class OriginRegion : Origin 
    {
        [RdfName(":OriginRegion")]
        public new string Name { get; set; }

    }
    public class OriginCity : Origin 
    {
        [RdfName(":OriginCity")]
        public new string Name { get; set; }

    }
}