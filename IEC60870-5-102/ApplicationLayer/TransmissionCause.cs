using System;

using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Transmission cause
    /// </summary>
    public class TransmissionCause : IEncodeable
    {

        #region Static

        /// <summary>
        /// Converts a cause to a byte
        /// </summary>
        /// <param name="cause">Cause to convert</param>
        public static implicit operator byte(TransmissionCause cause)
        {
            byte res = 0;

            res = ( byte ) ( res | (byte) cause.Code );

            if (!cause.Positive)
                res = ( byte ) ( res | ( 0x1 << 6 ) );

            if (cause.Test)
                res = ( byte ) ( res | ( 0x1 << 7 ) );

            return res;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 0 No test, 1 Test
        /// </summary>
        public bool Test { get; private set; }

        /// <summary>
        /// Positive ACK. Positive = 0, Negative = 1
        /// </summary>
        public bool Positive { get; private set; }

        /// <summary>
        /// Code of the transmission
        /// </summary>
        public TransmissionCauses Code { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public TransmissionCause()
        {

        }

        /// <summary>
        /// Initializes a new transmission cause with the given cause code
        /// </summary>
        /// <param name="cause">Transmision cause</param>
        public TransmissionCause(TransmissionCauses cause)
        {
            this.Code = cause;
            this.Positive = true;
        }

        #endregion

        #region ISerializable

        /// <summary>
        /// Serializes the current instance
        /// </summary>
        /// <param name="encoder">Encoder to use</param>
        public void Encode(IEncoder encoder)
        {
            encoder.Write(this);
        }

        /// <summary>
        /// Deserializes the data within the specified decoder
        /// </summary>
        /// <param name="decoder">Decoder with the data to deserialize</param>
        public void Decode(IDecoder decoder)
        {
            byte value = decoder.ReadByte();

            this.Code = ( TransmissionCauses ) (value & 0x1F);

            this.Positive = ( value & 0x40 ) == 0;

            this.Test = ( value & 0x80 ) > 0;
        }

        #endregion

        #region Object

        /// <summary>
        /// Gets a string representation of the current instance
        /// </summary>
        /// <returns>String representing the current instance</returns>
        public override string ToString()
        {
            return Enum.GetName(typeof(TransmissionCauses), this.Code);
        }

        /// <summary>
        /// Gets a boolean indicating whether the given object is equal to the current insta
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>Boolean indicating whether the given object is equal or not to the given instance</returns>
        public override bool Equals(object obj)
        {
            TransmissionCause cause = obj as TransmissionCause;

            if (cause == null)
                return false;

            return this.Code == cause.Code && this.Positive == cause.Positive && this.Test == cause.Test;
        }

        /// <summary>
        /// Gets a int representing the current instance
        /// </summary>
        /// <returns>Integer representing the current instance</returns>
        public override int GetHashCode()
        {
            return ( byte ) this.Code * ( ( this.Positive == true ) ? 10 : 11 ) + ( ( this.Test ) ? 0 : 1 );
        }

        #endregion

    }
}
