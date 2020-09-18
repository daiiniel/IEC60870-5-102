using System;

using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Class to encapsulate the different instant values within an slave
    /// </summary>
    public class InstantValues
    {

        #region Properties

        /// <summary>
        /// Instant energy totalizers
        /// </summary>
        public InstantEnergyTotes InstantEnergyTotes { get; private set; }

        /// <summary>
        /// Instant power values
        /// </summary>
        public InstantPowers InstantPowers { get; private set; }

        /// <summary>
        /// Instant voltage and current values
        /// </summary>
        public V_I InstantVI { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="totes">Energy totes values</param>
        /// <param name="powers">Power instant values</param>
        /// <param name="vis">Instant voltage and current values</param>
        public InstantValues(InstantEnergyTotes totes, InstantPowers powers, V_I vis)
        {
            this.InstantEnergyTotes = totes;
            this.InstantPowers = powers;
            this.InstantVI = vis;
        }

        #endregion

    }
}
