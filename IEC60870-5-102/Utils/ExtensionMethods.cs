using System;
using System.IO;

namespace IEC60870_5_102.Utils
{
    /// <summary>
    /// Extension methods
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Extension method to convert streams to byte arrays
        /// </summary>
        /// <param name="data">Stream containing the data</param>
        /// <returns>Array of bytes with the information within the stream</returns>
        public static byte[] ToArray(this Stream data)
        {
            int count =  0;

            int length = (int) data.Length;
            byte[] buffer = new byte[length];

            data.Position = 0;

            while (count < length)
            {
                count += data.Read(buffer, count, length - count);
            }

            return buffer;
        }

        /// <summary>
        /// Converts to a byte array
        /// </summary>
        /// <param name="value">Int to convert</param>
        /// <returns>Byte array</returns>
        public static byte[] ToByteArray(this UInt16 value)
        {
            byte[] res = new byte[2];

            res[0] = ( byte ) ( value & 0xFF );
            res[1] = ( byte ) ( ( value & 0xFF00 ) >> 8 );

            return res;
        }
    }
}
