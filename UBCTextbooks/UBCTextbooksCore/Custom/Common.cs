using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UBCTextbooksCore.Custom
{
    public class Common
    {
        public static int SafeIntParse(string s)
        {
            int i;
            int.TryParse(s, out i);
            return i;
        }

        public static float SafeFloatParse(string s)
        {
            float f;
            float.TryParse(s, out f);
            return f;
        }
    }
}
