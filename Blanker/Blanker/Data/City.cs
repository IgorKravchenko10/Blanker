using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blanker.Data
{
    public class City:Entity
    {
       
        public string area { get; set; }

        public string region { get; set; }

        public int? important { get; set; }
    }

    public class RootCities
    {
        public List<City> response { get; set; }
    }
}
