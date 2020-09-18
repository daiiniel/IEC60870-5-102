using System;

using IEC60870_5_102.LinkLayer;
using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// ASDU to receive the parameters of the point
    /// </summary>
    [ASDU(129)]
    public class P_ME_NA_2 : ASDU
    {
        #region Properties

        /// <summary>
        /// Parameters of the point
        /// </summary>
        public MeassurePointParameters Parameters { get; private set; }
        
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor of the class
        /// </summary>
        public P_ME_NA_2() :base()
        {
            this.Parameters = new MeassurePointParameters();
        }

        #endregion

        #region IEncodeable

        /// <summary>
        /// Serializes the current instance of the class to the specified encoder
        /// </summary>
        /// <param name="encoder">Encoder to use</param>
        public override void Encode(IEncoder encoder)
        {

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

            this.Parameters.Decode(decoder);

        }

        #endregion

    }
}
