using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NerdDinner.Core.Model
{
    //Generic Representation of DBGeography for all Platforms
    //Can add more if needed for consistency
    public class CoreGeography
    {
        public Object Provider { get; set; }
        public Nullable<double> LatitudePoint{ get; set; }
        public Nullable<double> LongitudePoint{ get; set; }
        
    }
}
