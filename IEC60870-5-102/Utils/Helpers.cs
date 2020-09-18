using System.Text;

namespace IEC60870_5_102.Utils
{
    /// <summary>
    /// Static helpers functions
    /// </summary>
    public static class Helpers
    {
        public static string ByteArrayToHexString(byte[] byteArray)
        {
            StringBuilder sb = new StringBuilder();

            foreach(byte b in byteArray)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gets the addition of every element of a byte array
        /// </summary>
        /// <param name="array">Array to operate with</param>
        /// <returns>Addition of the byte elements</returns>
        public static int GetByteArrayElementsAddition(byte[] array)
        {
            int value = 0;

            for(int ii = 0; ii < array.Length; ii++)
            {
                value += array[ii];
            }

            return value;
        }

        /// <summary>
        /// Gets a string from a byte array
        /// </summary>
        /// <param name="data">Array to convert</param>
        /// <param name="encoding">Encoding to use</param>
        /// <returns></returns>
        public static string ByteArrayToString(byte[] data, Encoding encoding)
        {
            return encoding.GetString(data);
        }
    }
}
