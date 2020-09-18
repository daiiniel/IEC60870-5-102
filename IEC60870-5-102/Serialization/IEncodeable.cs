
namespace IEC60870_5_102.Serialization
{
    /// <summary>
    /// Interface for seriable classes
    /// </summary>
    interface IEncodeable
    {

        /// <summary>
        /// Encodes the current instance into the encoder stream
        /// </summary>
        /// <param name="encoder">Encoder to serialize into</param>
        void Encode(IEncoder encoder);

        /// <summary>
        /// Deserializes the current instance with the data within the decoder stream
        /// </summary>
        /// <param name="decoder">Decoder with the data to deserialize</param>
        void Decode(IDecoder decoder);

    }
}
