using System;

using IEC60870_5_102.LinkLayer;
using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// TAriff information response
    /// </summary>
    [ASDU(136)]
    public class M_TA_VM_2 : ASDU
    {
        #region Properties

        /// <summary>
        /// Tariff information object
        /// </summary>
        public TariffInformationObject TariffInformation { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor of the class
        /// </summary>
        public M_TA_VM_2()
        {
            this.TariffInformation = new TariffInformationObject();
        }

        #endregion

        #region ISerializable

        /// <summary>
        /// Serializes the current instance of the class to the specified encoder
        /// </summary>
        /// <param name="encoder">Encoder to use</param>
        public override void Encode(IEncoder encoder)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserializes the current instance of the class from the specified decoder
        /// </summary>
        /// <param name="decoder">Decoder to use</param>
        public override void Decode(IDecoder decoder)
        {
            decoder.ReadByte();

            this.Cause.Decode(decoder);
            this.ASDUDirection.Decode(decoder);
                        
            this.TariffInformation.Deserialize(decoder);
        }

        #endregion
    }
}
