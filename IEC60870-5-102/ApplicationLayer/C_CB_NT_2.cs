using System;

using IEC60870_5_102.LinkLayer;
using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Represents a C_CB_NT_2 telegram according to IEC60870-5-102 to operational integrators blocks
    /// </summary>
    [ASDU(189)]
    public class C_CB_NT_2 : ASDU
    {
        #region Properties

        /// <summary>
        /// Time from
        /// </summary>
        public CP40Time2a TimeFrom { get; set; }

        /// <summary>
        /// Time to
        /// </summary>
        public CP40Time2a TimeTo { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public C_CB_NT_2() : base()
        {
            this.TimeFrom = new CP40Time2a();
            this.TimeTo = new CP40Time2a();
        }

        /// <summary>
        /// Initializes an instance of the C_CI_NT_2 telegram
        /// </summary>
        /// <param name="meassurePoint">Meassure point</param>
        /// <param name="timeFrom">Time from</param>
        /// <param name="timeTo">Time to</param>
        /// <param name="registerDirection">Register direction</param>
        public C_CB_NT_2(
            UInt16 meassurePoint,
            CP40Time2a timeFrom,
            CP40Time2a timeTo,
            RegisterDirections registerDirection) : base(
                TransmissionCauses.Activation,
                meassurePoint,
                registerDirection)
        {
            this.TimeFrom = timeFrom;
            this.TimeTo = timeTo;
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

            encoder.Write(( byte ) ObjectsDirections.Totes1To6);

            this.TimeFrom.Encode(encoder);
            this.TimeTo.Encode(encoder);
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

            ObjectsDirections firstObject = (ObjectsDirections) decoder.ReadByte();

            this.TimeFrom.Decode(decoder);
            this.TimeTo.Decode(decoder);
        }

        #endregion
    }
}
