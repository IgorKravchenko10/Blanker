using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blanker.Data
{
    /// <summary>
    /// Класс Университет. Не наследуется от класса Entity, так как в vkAPI имеет идентификатор-переменную id,
    /// в отличие от города и страны, у которых эта переменная называется cid.
    /// </summary>
    public class University
    {
        public int id { get; set; }

        public string title { get; set; }
    }

    public class RootUniversities
    {
        public List<University> response { get; set; }
    }
}
