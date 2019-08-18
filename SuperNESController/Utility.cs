using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperNESController
{
    public static class Utility
    {
        public static Buttons[] SplitButtonFlags(Buttons buttons)
        {
            var values = Enum.GetValues(typeof(Buttons));

            var list = new List<Buttons>();

            foreach (Buttons v in values)
            {
                if ((buttons & v) != 0)
                    list.Add(v);
            }

            return list.ToArray();
        }

        public static Buttons MergeButtonFlags(Buttons[] flags)
        {
            Buttons value = Buttons.None;

            foreach (Buttons f in flags)
                value |= f;

            return value;
        }
    }
}
