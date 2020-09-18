using System;

using IEC60870_5_102.ApplicationLayer;
using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.LinkLayer
{
    /// <summary>
    /// Represents an ASDU from the 
    /// </summary>
    public abstract class ASDU :IEncodeable
    {
        #region Static

        public static ASDU GetASDU(IDecoder decoder)
        {
            byte ASDUIdentifier = decoder.ReadByte();

            Type type = null;

            EncodeableFactory factory = EncodeableFactory.GetFactory();

            if (!factory.GetType(ASDUIdentifier, out type))
                throw new NotSupportedException(
                    String.Format(
                        "The received type is not implemented. Type code: {0}",
                        ASDUIdentifier));

            ASDU asdu = Activator.CreateInstance(type) as ASDU;

            asdu.Decode(decoder);

            return asdu;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Cause of the transimision
        /// </summary>
        public TransmissionCause Cause { get; private set; }

        /// <summary>
        /// Direction of the register
        /// </summary>
        public CommonASDUDirection ASDUDirection { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public ASDU()
        {
            this.ASDUDirection = new CommonASDUDirection();
            this.Cause = new TransmissionCause();
        }

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="cause">Transmision cause of the ASDU</param>
        /// <param name="meassurePoint">Meassure point</param>
        /// <param name="registerDirection">Direction of the register</param>
        public ASDU(
            TransmissionCauses cause,
            UInt16 meassurePoint,
            RegisterDirections registerDirection)
        {
            this.ASDUDirection = new CommonASDUDirection(meassurePoint, registerDirection);
            this.Cause = new TransmissionCause(cause);
        }

        #endregion

        #region IEncodeable

        /// <summary>
        /// Encodes the current encodeable instance
        /// </summary>
        /// <param name="encoder">Endoder to use</param>
        public abstract void Encode(IEncoder encoder);

        /// <summary>
        /// Decodes an ASDU instance
        /// </summary>
        /// <param name="decoder">Decoder to use</param>
        public abstract void Decode(IDecoder decoder);

        #endregion
    }
}
