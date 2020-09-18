using System;
using System.IO;
using System.Text;

namespace IEC60870_5_102.Serialization
{
    /// <summary>
    /// Binary decoder for IEC 60870-5-102 types
    /// </summary>
    public class BinaryDecoder : Coder, IDecoder
    {
        #region PROPERTIES

        /// <summary>
        /// Gets the length of the stream within the binary decoder
        /// </summary>
        public int Length
        {
            get
            {
                return ( int ) this.Reader.BaseStream.Length;
            }
        }

        /// <summary>
        /// Gets the position of the stream
        /// </summary>
        public int Position
        {
            get
            {
                return ( int ) this.Reader.BaseStream.Position;
            }
        }

        BinaryReader m_Reader;
        /// <summary>
        /// Binary reader for binary deserialization
        /// </summary>
        BinaryReader Reader
        {
            get
            {
                return m_Reader;
            }
        }
                
        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Constructor for decoders
        /// </summary>
        /// <param name="dataToDecode">Data to deserialize</param>
        public BinaryDecoder(byte[] dataToDecode) : base(dataToDecode)
        {
            m_Reader = new BinaryReader(base.Stream);
        }

        /// <summary>
        /// Constructor for decoders
        /// </summary>
        /// <param name="stream">Stream with the data to deserialize</param>
        public BinaryDecoder(Stream stream) : base(stream)
        {
            m_Reader = new BinaryReader(stream);
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Gets an array of the data within the decoder
        /// </summary>
        /// <returns>Array of bytes</returns>
        public byte[] ToArray()
        {
            Stream stream = this.Reader.BaseStream;

            long position = stream.Position;
            stream.Position = 0;

            int length = (int) stream.Length;
            int count = 0;

            byte[] data = new byte[length];

            while (count < length)
                count += stream.Read(data, count, length - count);

            stream.Position = position;

            return data;
        }

        /// <summary>
        /// Reads one byte from the stream and moves foward one position
        /// </summary>
        /// <returns>Deserialized byte</returns>
        public Byte ReadByte()
        {
            return this.Reader.ReadByte();
        }

        /// <summary>
        /// Gets a byte from the stream and it does not move the position foward
        /// </summary>
        /// <returns>Next byte</returns>
        public byte PeekByte()
        {
            return (byte) this.Reader.PeekChar();
        }

        /// <summary>
        /// Returns an unsigned integer of 16 bits from the stream and moves foward 2 positions
        /// </summary>
        /// <returns>16 bits unsigned integer</returns>
        public UInt16 ReadUInt16()
        {
            return this.Reader.ReadUInt16();
        }

        /// <summary>
        /// Returns an Int16 from the stream and moves foward 2 positions
        /// </summary>
        /// <returns>Deserialized Int16</returns>
        public Int16 ReadInt16()
        {
            return this.Reader.ReadInt16();
        }

        /// <summary>
        /// Returns an unsigned integer of 32 bits from the stream and moves foward 4 positions
        /// </summary>
        /// <returns>32 bits unsigned integer</returns>
        public UInt32 ReadUInt32()
        {
            return this.Reader.ReadUInt32();
        }

        /// <summary>
        /// Returns an Int32 from the stream and moves foward 4 positions
        /// </summary>
        /// <returns>Deserialized Int32</returns>
        public Int32 ReadInt32()
        {
            return this.Reader.ReadInt32();
        }

        /// <summary>
        /// Returns an unsigned integer of 64 bits from the stream and moves foward 8 positions
        /// </summary>
        /// <returns>64 bits unsigned integer</returns>
        public UInt64 ReadUInt64()
        {
            return this.Reader.ReadUInt64();
        }

        /// <summary>
        /// Returns an Int64 from the stream and moves foward 8 positions
        /// </summary>
        /// <returns>Deserialized Int64</returns>
        public Int64 ReadInt64()
        {
            return this.Reader.ReadInt64();
        }

        /// <summary>
        /// Reads a string from the stream
        /// </summary>
        /// <param name="length">Length of the stream to read</param>
        /// <returns>String read from the stream</returns>
        public string ReadString(int length)
        {
            byte[] data = this.ReadBuffer(length);

            return ASCIIEncoding.ASCII.GetString(data);
        }

        /// <summary>
        /// Reads a buffer of bytes from the stream
        /// </summary>
        /// <param name="length">Number of bytes to read</param>
        /// <returns>Buffer of bytes</returns>
        public byte[] ReadBuffer(int length)
        {
            byte[] data = new byte[length];

            for(int ii = 0; ii < length; ii++)
            {
                data[ii] = this.ReadByte();
            }

            return data;
        }

        /// <summary>
        /// Reads a string 
        /// </summary>
        public string ReadString(byte terminator)
        {
            StringBuilder b = new StringBuilder();

            bool read = false;

            byte c = this.ReadByte();

            while (c != terminator || read == false)
            {
                if (c != terminator && c != 0x0a)
                    b.Append(Convert.ToChar(c));

                c = this.ReadByte();

                read = true;
            }

            c = this.ReadByte();

            return b.ToString();
        }

        #endregion
    }
}
