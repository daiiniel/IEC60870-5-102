using System;

namespace IEC60870_5_102.Serialization
{
    /// <summary>
    /// Interface for decoders
    /// </summary>
    public interface IDecoder : IDisposable
    {
        #region Properties

        /// <summary>
        /// Length of the data within the decoder
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Gets the position of the current stream
        /// </summary>
        int Position { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an array of the data within the decoder
        /// </summary>
        /// <returns></returns>
        byte[] ToArray();

        /// <summary>
        /// Reads one byte from the stream
        /// </summary>
        /// <returns>Deserialized byte</returns>
        Byte ReadByte();

        /// <summary>
        /// Gets a byte from the stream and it does not move the position foward
        /// </summary>
        /// <returns>Next byte</returns>
        Byte PeekByte();

        /// <summary>
        /// Returns an unsigned integer of 16 bits from the stream
        /// </summary>
        /// <returns>16 bits unsigned integer</returns>
        UInt16 ReadUInt16();

        /// <summary>
        /// Returns an Int16 from the stream
        /// </summary>
        /// <returns>Deserialized Int16</returns>
        Int16 ReadInt16();

        /// <summary>
        /// Returns an unsigned integer of 32 bits from the stream
        /// </summary>
        /// <returns>32 bits unsigned integer</returns>
        UInt32 ReadUInt32();

        /// <summary>
        /// Returns an Int32 from the stream
        /// </summary>
        /// <returns>Deserialized Int32</returns>
        Int32 ReadInt32();

        /// <summary>
        /// Returns an unsigned integer of 64 bits from the stream
        /// </summary>
        /// <returns>64 bits unsigned integer</returns>
        UInt64 ReadUInt64();

        /// <summary>
        /// Returns an Int64 from the stream
        /// </summary>
        /// <returns>Deserialized Int64</returns>
        Int64 ReadInt64();

        /// <summary>
        /// Reads a string from the stream
        /// </summary>
        /// <param name="length">Length of the stream to read</param>
        /// <returns>String read from the stream</returns>
        string ReadString(int length);

        /// <summary>
        /// Reads a buffer of bytes from the stream
        /// </summary>
        /// <param name="length">Number of bytes to read</param>
        /// <returns>Buffer of bytes</returns>
        byte[] ReadBuffer(int length);

        #endregion
    }
}
