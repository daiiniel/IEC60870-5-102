using System;

using IEC60870_5_102.LinkLayer;
using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Telegram to read the current active contract powers 
    /// </summary>
    [ASDU(144)]
    public class C_PC_NA_2 : ASDU
    {
        #region Constructors

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public C_PC_NA_2() : base()
        {
            
        }

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="meassurePoint">Meassure point</param>
        /// <param name="direction">Direction of the object to read</param>
        public C_PC_NA_2(
            UInt16 meassurePoint,
            RegisterDirections direction) : base
            (
                TransmissionCauses.Request,
                meassurePoint,
                direction)
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
            encoder.Write(( byte ) 1);

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
