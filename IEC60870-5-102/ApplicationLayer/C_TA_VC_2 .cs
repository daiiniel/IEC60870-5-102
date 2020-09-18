using System;

using IEC60870_5_102.LinkLayer;
using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Represents a C_TA_VC_2 according to the IEC 60870-5-102 to read the current tariff information
    /// </summary>
    [ASDU(133)]
    public class C_TA_VC_2 : ASDU
    {
        #region Propeties

        #endregion

        #region Constructor

        public C_TA_VC_2() :base ()
        {

        }

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="meassurePoint">Slave meassure point</param>
        /// <param name="direction">Direction of the object to read</param>
        public C_TA_VC_2(
            UInt16 meassurePoint,
            RegisterDirections direction) : base(
                TransmissionCauses.Activation,
                meassurePoint,
                direction)
        {
            if (( byte ) direction < 134 || ( byte ) direction > 136)
                throw new Exception("The specified contract is not valid");
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
