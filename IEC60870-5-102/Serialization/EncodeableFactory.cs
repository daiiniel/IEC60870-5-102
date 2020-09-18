using System;
using System.Collections.Generic;
using System.Reflection;

using IEC60870_5_102.LinkLayer;

namespace IEC60870_5_102.Serialization
{
    /// <summary>
    /// Encodeable factory for the assembly classes
    /// </summary>
    public class EncodeableFactory
    {

        #region Static

        static EncodeableFactory encodeableFactory;
        /// <summary>
        /// Gets the only instance of the encodeable factory
        /// </summary>
        /// <returns>Only instance of encodeable factory</returns>
        public static EncodeableFactory GetFactory()
        {
            if (encodeableFactory == null)
                encodeableFactory = new EncodeableFactory();

            return encodeableFactory;
        }

        #endregion

        #region Properties

        Dictionary<byte, Type> m_factory;
        /// <summary>
        /// Dictionary with the pair key type
        /// </summary>
        Dictionary<byte, Type> Factory
        {
            get
            {
                return m_factory;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an instance of a encodeable factory
        /// </summary>
        EncodeableFactory()
        {
            m_factory = new Dictionary<byte, Type>();

            this.Initialize();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load the implemented ASDUs
        /// </summary>
        void Initialize()
        {
            Type[] types = Assembly.GetExecutingAssembly().GetExportedTypes();

            foreach (Type t in types)
            {
                ASDUAttribute atribute = t.GetCustomAttribute(typeof(ASDUAttribute)) as ASDUAttribute;

                if (atribute != null)
                    this.Factory.Add(atribute.Code, t);
            }
        }

        /// <summary>
        /// Gets the type of the specified key
        /// </summary>
        /// <param name="key">Index key</param>
        /// <param name="assemblyType">Rerturn type</param>
        /// <returns>Boolean indicating if the current type is implemented or not</returns>
        public bool GetType(byte key, out Type assemblyType)
        {
            return this.Factory.TryGetValue(key, out assemblyType);
        }

        #endregion
    }
}
