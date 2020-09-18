using System;

using IEC60870_5_102.LinkLayer;
using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    // <summary>
    /// Represents a C_TA_VM_2 according to the IEC 60870-5-102 to read the tariff information from the memory
    /// </summary>
    [ASDU(134)]
    public class C_TA_VM_2 : ASDU
    {
        #region Propeties

        /// <summary>
        /// Date from to read the tariff information
        /// </summary>
        public CP40Time2a From { get; private set; }

        /// <summary>
        /// Date to to read the tariff information
        /// </summary>
        public CP40Time2a To { get; private set; }

        #endregion

        #region Constructor

        public C_TA_VM_2() : base()
        {
            this.From = new CP40Time2a();
            this.To = new CP40Time2a();
        }

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="meassurePoint">Slave meassure point</param>
        /// <param name="direction">Direction of the object to read</param>
        /// <param name="from">Date from</param>
        /// <param name="to">Date to</param>
        public C_TA_VM_2(
            UInt16 meassurePoint,
            RegisterDirections direction,
            DateTime from, 
            DateTime to) : base(
                TransmissionCauses.Activation,
                meassurePoint,
                direction)
        {
            if (( byte ) direction < 134 || ( byte ) direction > 136)
                throw new Exception("The specified contract is not valid");

            this.From = new CP40Time2a(from);
            this.To = new CP40Time2a(to);
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

            this.From.Encode(encoder);
            this.To.Encode(encoder);
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

            this.From.Decode(decoder);
            this.To.Decode(decoder);
        }

        #endregion
    }
}
