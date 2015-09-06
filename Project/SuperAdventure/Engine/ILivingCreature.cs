using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    interface ILivingCreature
    {
        int CurrentHitPoints { get; set; }
        int MaximumHitPoints { get; set; }
    }
}
