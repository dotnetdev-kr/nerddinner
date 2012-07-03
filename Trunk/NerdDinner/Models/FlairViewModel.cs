using System.Collections.Generic;

namespace NerdDinner.Models
{
    public class FlairViewModel
    {
        public IList<Dinner> Dinners { get; set; }
        public string LocationName { get; set; }
    }
}
