using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellDB
{
    public class Champion
    {
        public QSlot Q;
        public WSlot W;
        public ESlot E;
        public RSlot R;

        public class QSlot
        {
            public string Name;
            public int MaxRank;
            public int[] CoolDown;
            public string CoolDownBurn;
            public int[] Cost;
            public string CostType;
            public string MaxAmmo;
            public int[] Range;
            public string RangeBurn;
        }

        public class WSlot
        {
            public string Name;
            public int MaxRank;
            public int[] CoolDown;
            public string CoolDownBurn;
            public int[] Cost;
            public string CostType;
            public string MaxAmmo;
            public int[] Range;
            public string RangeBurn;
        }

        public class ESlot
        {
            public string Name;
            public int MaxRank;
            public int[] CoolDown;
            public string CoolDownBurn;
            public int[] Cost;
            public string CostType;
            public string MaxAmmo;
            public int[] Range;
            public string RangeBurn;
        }

        public class RSlot
        {
            public string Name;
            public int MaxRank;
            public int[] CoolDown;
            public string CoolDownBurn;
            public int[] Cost;
            public string CostType;
            public string MaxAmmo;
            public int[] Range;
            public string RangeBurn;
        }
    }
}
