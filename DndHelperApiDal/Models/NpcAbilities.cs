using System.Collections.Generic;

namespace DndHelperApiDal.Models
{
    public class NpcAbilities
    {
        public IEnumerable<NpcAbility> NpcAbilitiesHigh { get; set; }
        public IEnumerable<NpcAbility> NpcAbilitiesLow { get; set; }
    }
}
