using System;

using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.LinkLayer
{
    /// <summary>
    /// Class representing a control field according to the IEC
    /// </summary>
    public class ControlField : IEncodeable
    {
        #region STATIC

        /// <summary>
        /// Converts a control field to a byte
        /// </summary>
        /// <param name="value">Control field to convert</param>
        public static implicit operator byte(ControlField value)
        {
            return value.GetControlField();
        }

        /// <summary>
        /// Converts a value to a control field instance
        /// </summary>
        /// <param name="value">Control field converted</param>
        public static implicit operator ControlField(byte value)
        {
            ControlField controlField = new ControlField();

            controlField.Code = (byte) (value & 0xF);
            controlField.FCV = ( value & 0x10 ) > 0;
            controlField.FCB = ( value & 0x20 ) > 0;
            controlField.toSlave = ( value & 0x40 ) > 0;

            return controlField;
        }

        /// <summary>
        /// Gets the new lastFCB value
        /// </summary>
        /// <param name="lastFCB">LastFCB used</param>
        /// <param name="lastControlField">Last control field used</param>
        /// <returns>New FCB</returns>
        bool GetFCB(bool lastFCB, ControlField lastControlField)
        {
            bool res = lastFCB;

            if (lastControlField.FCV)
                res = lastControlField.FCB; 

            return res;
        }

        #endregion

        #region PROPERTIES

        byte m_Code;
        /// <summary>
        /// Function code of the telegram
        /// </summary>
        internal byte Code
        {
            get
            {
                return m_Code;
            }
            private set
            {
                m_Code = value;

                if (m_Code == (byte) FunctionCodesPRM1.LinkReset || m_Code == (byte) FunctionCodesPRM1.LinkRequest)
                    this.FCV = false;

                if (m_Code == (byte) FunctionCodesPRM1.UserData || m_Code == (byte) FunctionCodesPRM1.Class2UserData)
                    this.FCV = true;
            }
        }

        bool m_toSlave;
        /// <summary>
        /// Determines if the message is from secundary to primary
        /// </summary>
        internal bool toSlave
        {
            get
            {
                return m_toSlave;
            }
            private set
            {
                m_toSlave = value;
            }
        }

        bool m_FCB;
        /// <summary>
        /// Flow control bit. If FCV is set to true this bit alternates between 0 and 1 for every message
        /// </summary>
        internal bool FCB
        {
            get
            {
                return m_FCB;
            }
            private set
            {
                m_FCB = value;
            }
        }

        bool m_FCV;
        /// <summary>
        /// Bit to enable or disable the FCB bit
        /// </summary>
        internal bool FCV
        {
            get
            {
                return m_FCV;
            }
            private set
            {
                m_FCV = value;
            }
        }
        
        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Control field constructor
        /// </summary>
        public ControlField()
        {
            this.toSlave = false;
        }

        /// <summary>
        /// Creates a new instance of the field constructor with the given function code
        /// </summary>
        /// <param name="code">Function code for PRM 1 (Telegram from master to slave)</param>
        /// <param name="lastFCB">Last flow control bit</param>
        public ControlField(FunctionCodesPRM1 code)
        {
            this.toSlave = true;
            this.Code = (byte) (UInt16) code;
            
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the current FCB
        /// </summary>
        /// <param name="lastFCB">Last FCB used</param>
        internal bool Update(bool lastFCB)
        {
            if (FCV)
            {
                this.FCB = (lastFCB) ? false : true;

                return this.FCB;
            }
            else
            {
                return lastFCB;
            }
        }

        /// <summary>
        /// Gets the control field byte
        /// </summary>
        /// <returns>Control field byte</returns>
        public byte GetControlField()
        {
            byte res = 0;

            res = ( byte ) ( res | this.Code );

            if (this.toSlave)
                res = ( byte ) ( res | 0x40 );

            if (this.FCV)
                res = ( byte ) ( res | 0x10 );

            if (this.FCB)
                res = ( byte ) ( res | 0x20 );

            return res;
        }

        #endregion

        #region ISerializable

        /// <summary>
        /// Deserializes a control field instance
        /// </summary>
        /// <param name="decoder">Decoder used for deserialization</param>
        public void Decode(IDecoder decoder)
        {
            byte b = decoder.ReadByte();

            this.toSlave = ( b & 0x40 ) > 0;
            this.FCV = ( b & 0x10 ) > 0;
            this.FCB = ( b & 0x20 ) > 0;

            this.Code = (byte) ( b & 0x0F );
        }

        /// <summary>
        /// Serializes the current instance of the control field
        /// </summary>
        /// <param name="encoder">Encoder used for the serialization</param>
        public void Encode(IEncoder encoder)
        {
            encoder.Write(this);
        }

        #endregion

        #region Object

        /// <summary>
        /// Gets a string representing the current instance of the control field
        /// </summary>
        /// <returns>String representing the current instance</returns>
        public override string ToString()
        {
            return this.GetControlField().ToString();
        }

        #endregion

    }
}
