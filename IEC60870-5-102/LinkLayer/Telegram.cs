using System;
using System.IO;

using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.LinkLayer
{
    /// <summary>
    /// Base class for telegrams
    /// </summary>
    public abstract class Telegram : IEncodeable
    {

        #region Static

        /// <summary>
        /// End byte for telegrams
        /// </summary>
        protected static readonly byte EndByte = 0x16;

        /// <summary>
        /// Serializes a telegram into a stream
        /// </summary>
        /// <param name="telegram">Telegram to serialize</param>
        /// <returns>Stream with the serialized data</returns>
        public static Stream Serialize(Stream stream, Telegram telegram)
        {
            if (telegram.Equals(null))
                throw new NullReferenceException("Telegram can not be null.");

            BinaryEncoder encoder = new BinaryEncoder(stream);

            telegram.Encode(encoder);

            return encoder.Stream;
        }

        /// <summary>
        /// Deserializes a telegram from a stream
        /// </summary>
        /// <param name="stream">Stream with the telegram to deserialize</param>
        /// <returns>Deserialized telegram</returns>
        public static Telegram Deserialize(Stream stream)
        {
            BinaryDecoder decoder = new BinaryDecoder(stream);

            Telegram telegram = null;

            byte start = decoder.ReadByte();

            while(start != FixedTelegram.StartByte && start != VariableTelegram.StartByte)
                start = decoder.ReadByte();

            if (start == FixedTelegram.StartByte)
                telegram = FixedTelegram.GetTelegram(decoder);
            else if (start == VariableTelegram.StartByte)
                telegram = VariableTelegram.GetTelegram(decoder);

            if (telegram == null)
                throw new InvalidOperationException("Unknow data telegram type.");

            return telegram;
        }

        /// <summary>
        /// Gets the checksum of a fixed telegram 
        /// </summary>
        /// <returns>Checksum byte</returns>
        protected static byte GetChecksum(IEncoder encoder, int start, int end)
        {
            int count = 0;

            byte[] data = encoder.ToArray();

            if (encoder.Length < end)
                throw new InvalidOperationException("The end byte must not be higher than the length of the encoder stream");
            //68 09 09 68 73 D2 B7 67 00 05 01 00 00 73 16
            for (int ii = start; ii < end; ii++)
            {
                count += data[ii];
            }

            return ( byte ) ( count % 256 );
        }

        /// <summary>
        /// Gets the checksum of a given decoder
        /// </summary>
        /// <param name="decoder">Decoder with the data</param>
        /// <param name="start">Start position</param>
        /// <param name="end">End position</param>
        /// <returns>Checksum byte</returns>
        protected static byte GetChecksum(IDecoder decoder, int start, int end)
        {
            int count = 0;

            byte[] data = decoder.ToArray();

            for (int ii = start; ii < end; ii++)
                count += data[ii];

            return ( byte ) ( count % 256 );
        }

        #endregion

        #region PROPERTIES

        ushort m_LinkDirection;
        /// <summary>
        /// Link direction
        /// </summary>
        public ushort LinkDirection
        {
            get
            {
                return m_LinkDirection;
            }
            set
            {
                m_LinkDirection = value;
            }
        }

        ControlField m_ControlField;
        /// <summary>
        /// Telegram control field
        /// </summary>
        public ControlField ControlField
        {
            get
            {
                return m_ControlField;
            }
            protected set
            {
                m_ControlField = value;
            }
        } 

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Telegram constructor
        /// </summary>
        public Telegram()
        {
            this.ControlField = new ControlField();
        }

        /// <summary>
        /// Telegram constructor
        /// </summary>
        /// <param name="linkDirection">Direction of the device</param>
        /// <param name="code">Function code of the control field</param>
        /// <param name="lastFBC">Last FCB used</param>
        public Telegram(ushort linkDirection, FunctionCodesPRM1 code)
        {
            this.LinkDirection = linkDirection;
            this.ControlField = new ControlField(code);
        }

        #endregion
        
        #region ISerializable

        /// <summary>
        /// Serializes the current instance of the telegram
        /// </summary>
        /// <param name="encoder">Encoder used for the serialization</param>
        public abstract void Encode(IEncoder encoder);

        /// <summary>
        /// Deserializes a telegram instance
        /// </summary>
        /// <param name="decoder">Decoder used for deserialization</param>
        public abstract void Decode(IDecoder decoder);

        #endregion
        
    }
}
