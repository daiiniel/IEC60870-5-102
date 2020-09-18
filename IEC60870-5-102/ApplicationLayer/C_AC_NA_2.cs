using System;

using IEC60870_5_102.LinkLayer;
using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// ASDU 183 to send the password and start a session at the slave
    /// </summary>
    [ASDU(183)]
    public class C_AC_NA_2 : ASDU
    {
        #region Properties

        /// <summary>
        /// Password used for the command
        /// </summary>
        public UInt32 Password { get; private set; }
        
        #endregion

        #region Constructor

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public C_AC_NA_2() : base()
        {

        }

        /// <summary>
        /// Constructor for the start session and send password command
        /// </summary>
        /// <param name="password">Password to use</param>
        /// <param name="meassurePoint">Meassure point</param>
        public C_AC_NA_2(
            UInt32 password, 
            UInt16 meassurePoint) : base(
                TransmissionCauses.Activation, 
                meassurePoint, 
                RegisterDirections.Default)
        {
            this.Password = password;
        }

        #endregion

        #region ISerializable

        /// <summary>
        /// Serializes the current instance to the specified encoder
        /// </summary>
        /// <param name="encoder">Encoder to use</param>
        public override void Encode(IEncoder encoder)
        {            
            encoder.Write(( byte ) 1);
            this.Cause.Encode(encoder);
            this.ASDUDirection.Encode(encoder);
            encoder.Write(this.Password);
        }

        /// <summary>
        /// Deserializes a C_AC_NA_2 object
        /// </summary>
        /// <param name="decoder">Decodes to use</param>
        public override void Decode(IDecoder decoder)
        {            
            byte numberOfObjects = decoder.ReadByte();
            
            this.Cause.Decode(decoder);
            this.ASDUDirection.Decode(decoder);

            this.Password = decoder.ReadUInt32();
        }

        #endregion

    }
}
