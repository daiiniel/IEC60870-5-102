using System;

using IEC60870_5_102.LinkLayer;
using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Represents a telegram of type C_FS_NA_2 according to the IEC60870-5-102 to end the session
    /// </summary>
    [ASDU(187)]
    public class C_FS_NA_2 : ASDU
    {

        #region Constructors

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public C_FS_NA_2() : base()
        {

        }

        /// <summary>
        /// Constructor for ending session telegram
        /// </summary>
        /// <param name="meassurePoint">Slave meassure point</param>
        public C_FS_NA_2(
            UInt16 meassurePoint) : base(
                TransmissionCauses.Activation,
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
            encoder.Write((byte)0);

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
