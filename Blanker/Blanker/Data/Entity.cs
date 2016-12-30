using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blanker.Data
{
    public abstract class Entity
    {
        public int cid { get; set; }

        public string title { get; set; }
    }
}
