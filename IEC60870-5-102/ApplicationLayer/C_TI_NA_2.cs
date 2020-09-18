using System;

using IEC60870_5_102.LinkLayer;
using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Represents a C_TI_NA_2 telegram according to the IEC 680-5-102 to read the date and time from the slave
    /// </summary>
    [ASDU(103)]
    public class C_TI_NA_2 : ASDU
    {
        #region Constructors

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public C_TI_NA_2() : base()
        {

        }

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="linkDirection">Slave link direction</param>
        /// <param name="meassurePoint">Slave meassure point</param>
        /// <param name="lastFBC">Last FBC used</param>
        public C_TI_NA_2(
            UInt16 meassurePoint) : base(
                TransmissionCauses.Request,
                meassurePoint,
                RegisterDirections.Default)
        {

        }

        #endregion

        #region IEncodeable

        /// <summary>
        /// Serializes the current instance of the class to the specified encoder
        /// </summary>
        /// <param name="encoder">Encoder to use</param>
        public override void Encode(IEncoder encoder)
        {            
            encoder.Write(( byte ) 0);

            this.Cause.Encode(encoder);
            this.ASDUDirection.Encode(encoder);
        }

        /// <summary>
        /// Deserializes the current instance of the class from the specified decoder
        /// </summary>
        /// <param name="decoder">Decoder to use</param>
        public override void Decode(IDecoder decoder)
        {
            int numberOfObjects = decoder.ReadByte();

            this.Cause.Decode(decoder);
            this.ASDUDirection.Decode(decoder);
        }

        #endregion
    }
}
