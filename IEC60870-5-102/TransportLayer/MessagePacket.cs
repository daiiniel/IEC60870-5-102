using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using IEC60870_5_102.LinkLayer;

namespace IEC60870_5_102.TransportLayer
{
    public class MessagePacket
    {
        #region Properties

        /// <summary>
        /// Stream channel of the connection from where the data is received
        /// </summary>
        public Stream Channel { get; private set; }
        
        byte[] m_Message;

        public byte[] Message
        {
            get
            {
                return m_Message;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new MessagePacket instance to read messages from an endpoint
        /// </summary>
        /// <param name="s"></param>
        public MessagePacket(Stream s)
        {
            this.Channel = s;
        }

        /// <summary>
        /// Creates a new message packet isntance to write messages to an endpoint
        /// </summary>
        /// <param name="s">Channel</param>
        /// <param name="buffer">Message</param>
        public MessagePacket(Stream s, byte[] buffer)
        {
            this.Channel = s;
            m_Message = buffer;
        }

        #endregion

        #region Method

        /// <summary>
        /// Receives the next message from the channel
        /// </summary>
        public void Read()
        {
            if(this.Channel == null || this.Channel.CanRead == false)
            {
                throw new InvalidOperationException("Channel cannot be null");
            }
            
            this.Channel.ReadTimeout = 10000;

            byte start = (byte)this.Channel.ReadByte();

            while(start != FixedTelegram.StartByte && start != VariableTelegram.StartByte)
            {
                start = (byte)this.Channel.ReadByte();
            }

            int offset = 0;
            int index = 0;
            int bytesToRead = 0;

            if (start == FixedTelegram.StartByte)
            {
                m_Message = new byte[6];

                this.Message[0] = start;

                offset = 1;
                bytesToRead = 5;
            }
            else if (start == VariableTelegram.StartByte)
            {
                byte length = (byte)this.Channel.ReadByte();

                int totalLength = length + 6;

                m_Message = new byte[totalLength];

                this.Message[0] = start;
                this.Message[1] = length;

                offset = 2;
                bytesToRead = totalLength - 2;
            }

            while (index < bytesToRead)
            {
                index += this.Channel.Read(this.Message, offset + index, bytesToRead - index);
            }
        }

        /// <summary>
        /// Reads the given amount of bytes 
        /// </summary>
        /// <param name="buffer">Buffer to store the data</param>
        /// <param name="start">Position of the first byte to read</param>
        /// <param name="totalBytesToRead">Number of bytes to read</param>
        public void Read(byte[] buffer, int start, int totalBytesToRead)
        {
            if (this.Channel == null || this.Channel.CanRead == false)
            {
                throw new InvalidOperationException("Channel cannot be null");
            }

            int count = 0;

            while (count < totalBytesToRead)
            {
                count += this.Channel.Read(buffer, start + count, totalBytesToRead - count);
            }
        }

        /// <summary>
        /// Writes the current message to the channel
        /// </summary>
        public void Write()
        {
            if (this.Channel == null || this.Channel.CanRead == false)
            {
                throw new InvalidOperationException("Channel cannot be null");
            }

            this.Channel.WriteTimeout = 10000;

            int index = 0;
            int length = this.Message.Length;

            this.Channel.Write(this.Message, index, length - index);

            this.Channel.Flush();
        }

        #endregion
    }
}
