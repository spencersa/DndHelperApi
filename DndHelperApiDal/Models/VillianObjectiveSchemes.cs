using System.Collections.Generic;

namespace DndHelperApiDal.Models
{
    public class VillianObjectiveSchemes
    {
        public string Objective { get; set; }
        public IEnumerable<string> Schemes { get; set; }
    }
}
