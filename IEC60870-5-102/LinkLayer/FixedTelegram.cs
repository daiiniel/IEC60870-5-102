using System;

using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.LinkLayer
{
    /// <summary>
    /// Fixed telegram
    /// </summary>
    public class FixedTelegram : Telegram
    {
        #region Static

        /// <summary>
        /// Start byte for fixed telegrams
        /// </summary>
        internal static readonly byte StartByte = 0x10;

        /// <summary>
        /// Deserializes a fixed telegram with the data in the specified decoder
        /// </summary>
        /// <param name="decoder">Decoder to use</param>
        /// <returns>Fixed telegram</returns>
        public static FixedTelegram GetTelegram(IDecoder decoder)
        {
            FixedTelegram telegram = new FixedTelegram();

            telegram.Decode(decoder);

            return telegram;
        }

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Constructor for fixed telegrams (deserialization)
        /// </summary>
        public FixedTelegram() : base()
        {

        }

        /// <summary>
        /// Creates an instance of fixed telegram
        /// </summary>
        /// <param name="linkDirection">Direction of the device</param>
        /// <param name="code">Function code of the control field</param>
        public FixedTelegram(UInt16 linkDirection, FunctionCodesPRM1 code) : base(linkDirection, code)
        {

        }

        #endregion

        #region ISerializable

        /// <summary>
        /// Deserializes an instance of fixed telegram
        /// </summary>
        /// <param name="decoder">Decoder used for the deserialization</param>
        public override void Decode(IDecoder decoder)
        {            
            using (BinaryEncoder writer = new BinaryEncoder())
            {
                writer.Write(decoder.ReadBuffer(3));

                writer.Stream.Position = 0;

                using (BinaryDecoder reader = new BinaryDecoder(writer.Stream))
                {
                    this.ControlField.Decode(reader);

                    this.LinkDirection = reader.ReadUInt16();

                    byte checksum = decoder.ReadByte();

                    if (decoder.ReadByte() != Telegram.EndByte)
                        throw new InvalidOperationException("The telegram has not the rigth format");

                    if (checksum != Telegram.GetChecksum(reader, 0, reader.Length))
                        throw new InvalidOperationException("There are errors within the data received");
                }
            }
        }

        /// <summary>
        /// Serializes the current instance of the FixedTelegram object into the stream
        /// </summary>
        /// <param name="encoder">Encoder used for the serialization</param>
        public override void Encode(IEncoder encoder)
        {
            encoder.Write(StartByte);

            using (BinaryEncoder writer = new BinaryEncoder())
            {
                this.ControlField.Encode(writer);
                writer.Write(this.LinkDirection);

                encoder.Write(writer.ToArray());

                encoder.Write(Telegram.GetChecksum(writer, 0, writer.Length));
            }

            encoder.Write(EndByte);
        }

        #endregion
    }
}
