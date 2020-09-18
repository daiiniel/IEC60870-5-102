

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Represents a power integrator 
    /// </summary>
    public class PowerIntegrator
    {
        #region Properties

        /// <summary>
        /// Active energy in
        /// </summary>
        public ToteIntegrator Active_In { get; set; }

        /// <summary>
        /// Active energy out
        /// </summary>
        public ToteIntegrator Active_Out { get; set; }

        /// <summary>
        /// Reactive energy first square
        /// </summary>
        public ToteIntegrator Reactive_1 { get; set; }

        /// <summary>
        /// Reactive energy second square
        /// </summary>
        public ToteIntegrator Reactive_2 { get; set; }

        /// <summary>
        /// Reactive energy third square
        /// </summary>
        public ToteIntegrator Reactive_3 { get; set; }

        /// <summary>
        /// Public energy fourth square
        /// </summary>
        public ToteIntegrator Reactive_4 { get; set; }

        /// <summary>
        /// Date time of the integrator
        /// </summary>
        public CP40Time2a DateTime { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public PowerIntegrator()
        {
            this.Active_In = new ToteIntegrator();
            this.Active_Out = new ToteIntegrator();

            this.Reactive_1 = new ToteIntegrator();
            this.Reactive_2 = new ToteIntegrator();
            this.Reactive_3 = new ToteIntegrator();
            this.Reactive_4 = new ToteIntegrator();

            this.DateTime = new CP40Time2a();
        }

        #endregion
    }
}
