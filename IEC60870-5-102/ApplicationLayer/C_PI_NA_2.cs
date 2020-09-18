using System;

using IEC60870_5_102.Serialization;
using IEC60870_5_102.LinkLayer;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// ASDU to read the parameters from the slave
    /// </summary>
    [ASDU(182)]
    public class C_PI_NA_2 : ASDU
    {
        #region Properties

        #endregion

        #region Constructors

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public C_PI_NA_2() : base()
        {
            
        }

        /// <summary>
        /// Initializes an instance of the C_PI_NA_2 telegram
        /// </summary>
        /// <param name="meassurePoint">Meassure point</param>
        public C_PI_NA_2(
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
