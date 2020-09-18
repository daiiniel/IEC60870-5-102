using System;

namespace IEC60870_5_102.LinkLayer
{
    /// <summary>
    /// Attribute for Variable length telegrams
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ASDUAttribute : Attribute
    {
        #region Properties
        
        /// <summary>
        /// ASDU Code
        /// </summary>
        public byte Code { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// ASDU attribute constructor
        /// </summary>
        /// <param name="code">ASDU code</param>
        public ASDUAttribute(byte code)
        {
            this.Code = code;
        }

        #endregion
    }
}
