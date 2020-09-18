using System;
using System.IO;

using IEC60870_5_102.ApplicationLayer;
using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.LinkLayer
{
    /// <summary>
    /// Class for variable length telegrams
    /// </summary>
    public class VariableTelegram : Telegram
    {
        #region Static

        /// <summary>
        /// Start byte of a variable length telegram
        /// </summary>
        public static readonly byte StartByte = 0x68;

        /// <summary>
        /// Gets a variable telegram from a decoder
        /// </summary>
        /// <param name="decoder">Decoder to use</param>
        /// <returns>Variable length telegram</returns>
        public static VariableTelegram GetTelegram(IDecoder decoder)
        {            
            VariableTelegram telegram = new VariableTelegram();

            telegram.Decode(decoder);            

            return telegram;
        }

        #endregion

        #region Properties

        /// <summary>
        /// ASDU
        /// </summary>
        public ASDU ASDU { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Parameterless constructor for deserialization
        /// </summary>
        public VariableTelegram() : base()
        {

        }

        /// <summary>
        /// Telegram constructor
        /// </summary>
        /// <param name="linkDirection">Direction of the device</param>
        /// <param name="cause">Transmission cause</param>
        /// <param name="meassurePoint">Direction of the point to meassure</param>
        /// <param name="registerDirection">Direction of the register</param>
        /// <param name="code">Function code of the control field</param>
        public VariableTelegram(
            UInt16 linkDirection, ASDU asdu) : base(linkDirection, FunctionCodesPRM1.UserData)
        {
            this.ASDU = asdu;
        }

        #endregion

        #region Methods


        #endregion

        #region Telegram


        /// <summary>
        /// Serializes the current instance of the telegram
        /// </summary>
        /// <param name="encoder">Encoder used for the serialization</param>
        public override void Encode(IEncoder encoder)
        {
            using (IEncoder ASDUEncoder = new BinaryEncoder())
            {
                this.ASDU.Encode(ASDUEncoder);

                encoder.Write(VariableTelegram.StartByte);
                encoder.Write((byte) (ASDUEncoder.Length + 4));
                encoder.Write((byte) (ASDUEncoder.Length + 4));
                encoder.Write(VariableTelegram.StartByte);

                this.ControlField.Encode(encoder);
                encoder.Write(this.LinkDirection);

                encoder.Write(( ( ASDUAttribute ) this.ASDU.GetType().GetCustomAttributes(typeof(ASDUAttribute), false)[0]).Code);

                encoder.Write(ASDUEncoder.ToArray());

                encoder.Write(Telegram.GetChecksum(encoder, 4, encoder.Length));
                encoder.Write(Telegram.EndByte);

            }               
        }

        /// <summary>
        /// Deserializes a telegram instance
        /// </summary>
        /// <param name="decoder">Decoder used for deserialization</param>
        public override void Decode(IDecoder decoder)
        {
            decoder.ReadByte();
            decoder.ReadByte();

            decoder.ReadByte();

            this.ControlField.Decode(decoder);

            this.LinkDirection = decoder.ReadUInt16();

            this.ASDU = ASDU.GetASDU(decoder);
            
            byte checksum = decoder.ReadByte();

            if (checksum != Telegram.GetChecksum(decoder, 4, decoder.Position - 1))
                throw new Exception(
                    String.Format(
                        "The received telegram is inconsistent"));

            if (decoder.ReadByte() != Telegram.EndByte)
                throw new Exception(
                    string.Format(
                        "The telegram received has not the correct format"));
            
        }

        #endregion
    }
}
