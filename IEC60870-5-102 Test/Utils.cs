using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEC60870_5_102_Test
{
    public static class Utils
    {
        public static string ByteArrayToString(byte[] array, string separator)
        {
            StringBuilder sb = new StringBuilder();

            foreach(byte b in array)
            {
                sb.Append($"0x{b.ToString("x2")}{separator}");
            }

            return sb.ToString();
        }
    }
}
