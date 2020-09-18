using System;
using System.IO;
using System.Collections.Generic;

using IEC60870_5_102.ApplicationLayer;
using IEC60870_5_102.LinkLayer;
using IEC60870_5_102.TransportLayer;

namespace IEC60870_5_102
{
    /// <summary>
    /// Represents a master according to the IEC60870-5-102
    /// </summary>
    public class Master : Link, IDisposable
    {

        #region Properties

        UInt16 m_linkAddress;
        /// <summary>
        /// Link direction of the master
        /// </summary>
        UInt16 LinkAddress
        {
            get
            {
                return m_linkAddress;
            }
            set
            {
                m_linkAddress = value;
            }
        }

        UInt32 m_password;
        /// <summary>
        /// Password to access the master
        /// </summary>
        UInt32 Password
        {
            get
            {
                return m_password;
            }
            set
            {
                m_password = value;
            }
        }

        UInt16 m_meassurePoint;
        /// <summary>
        /// Meassure point 
        /// </summary>
        UInt16 MeassurePoint
        {
            get
            {
                return m_meassurePoint;
            }
            set
            {
                m_meassurePoint = value;
            }
        }

        /// <summary>
        /// Gets a boolean indicating if the session is activated at the slave sid
        /// </summary>
        public bool Activated { get; private set; }
        
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for masters
        /// </summary>
        /// <param name="linkDirection">Link direction for communicating with slave</param>
        /// <param name="password">Password of the slave</param>
        /// <param name="meassurePoint">Meassure point</param>
        /// <param name="endpoint">Channel endpoint</param>
        public Master(UInt16 linkDirection, UInt32 password, UInt16 meassurePoint, TransportEndpoint endpoint) : base(endpoint)
        {
            m_linkAddress = linkDirection;
            m_password = password;
            m_meassurePoint = meassurePoint;

            this.Activated = false;
        }

        #endregion

        #region Methods

        
        /// <summary>
        /// Connects the master to the slave to start the comunication exchange
        /// </summary>
        public new void Connect()
        {
            base.Connect();
        }

        /// <summary>
        /// Disconnects the master from the slave fishing the comunication exchange
        /// </summary>
        public new void Disconnect()
        {
            this.Activated = false;
            base.Disconnect();
        }

        /// <summary>
        /// Gets a response by sending a telegram to the channel 
        /// </summary>
        /// <param name="request">Request telegram</param>
        /// <returns>Telegram response</returns>
        private FunctionCodesPRM0 Send(ASDU request)
        {
            return base.Send(this.LinkAddress, request);
        }                
        
        /// <summary>
        /// Resets the session so it removes any prevoius communication
        /// </summary>
        public void ResetLink()
        {
            base.ResetLink(this.LinkAddress);
        }

        public void RequestLink()
        {
            base.RequestLinkStatus(this.LinkAddress);
        }

        /// <summary>
        /// Starts a session in the slave
        /// </summary>
        public void StartSession()
        {
            this.RequestLink();

            this.ResetLink();

            this.RequestLink();

            ASDU request = new C_AC_NA_2(
                this.Password,
                this.MeassurePoint);

            FunctionCodesPRM0 res = this.Send(request);

            if (res != FunctionCodesPRM0.ACK)
                throw new Exception("Command not accepted");

            C_AC_NA_2 activateSessionResponse = base.RequestData(this.LinkAddress) as C_AC_NA_2;

            if (activateSessionResponse == null)
                throw new Exception(
                    String.Format(
                        "Could not start session at the slave with password: {0}",
                        this.Password));

            if (activateSessionResponse.Cause.Code == TransmissionCauses.ActivationACK && activateSessionResponse.Cause.Positive == true)
                this.Activated = true;

            if (this.Activated != true)
                throw new Exception(
                    "Coud not start the session on the specified slave with the current password");
        }

        /// <summary>
        /// Closes the session in the slave
        /// </summary>
        /// <returns>Boolean indicating wether the current session has been finalized or not</returns>
        public void EndSession()
        {
            if (this.Activated == false)
                return;

            ASDU endSessionRequest = new C_FS_NA_2(
                this.MeassurePoint);

            if (this.Send(endSessionRequest) != FunctionCodesPRM0.ACK)
                throw new Exception("Command not accepted");

            C_FS_NA_2 response = base.RequestData(this.LinkAddress) as C_FS_NA_2;

            if (response.Cause.Positive)
                this.Activated = true;

            if (this.Activated == false)
                throw new Exception(
                    "Could not finalize the current session");
        }

        /// <summary>
        /// Gets the operational integrationals
        /// </summary>
        /// <param name="from">Date from to request the incremental operational integrators</param>
        /// <param name="to">Date to to request the incremental operational integrators</param>
        /// <param name="direction">Frecuency of the meter</param>
        /// <returns>List of power integrator read from the slave</returns>
        public List<PowerIntegrator> RequestOperationalIntegratorsTotes(DateTime from, DateTime to, RegisterDirections direction)
        {
            List<PowerIntegrator> integrators = new List<PowerIntegrator>();

            ASDU request = new C_CI_NT_2(
                this.MeassurePoint,
                new CP40Time2a(from),
                new CP40Time2a(to),
                direction);

            if (this.Send(request) != FunctionCodesPRM0.ACK)
                throw new Exception("Command not accepted");

            ASDU response = base.RequestData(this.LinkAddress);

            C_CI_NT_2 operationalsResponse = response as C_CI_NT_2;

            if (operationalsResponse == null)
                throw new Exception(
                    "Could not get operational integrators. Bad result.");

            if (operationalsResponse.Cause.Code == TransmissionCauses.IntegrationPeriodNotAvailable)
                return integrators;
            
            while (response != null && ( response is C_CI_NT_2 ) ? ( response as C_CI_NT_2 ).Cause.Code != TransmissionCauses.ActivationEnd : true)
            {
                response = base.RequestData(this.LinkAddress);

                M_IT_TG_2 toteResponse = response as M_IT_TG_2;

                if (toteResponse != null)
                {
                    integrators.Add(toteResponse.Integrator);
                }
            }

            return integrators;
        }

        /// <summary>
        /// Gets the incremental operational integrationals
        /// </summary>
        /// <param name="from">Date from to request the incremental operational integrators</param>
        /// <param name="to">Date to to request the incremental operational integrators</param>
        /// <param name="direction">Frecuency of the meter</param>
        /// <returns>List of power integrator read from the slave</returns>
        public List<PowerIntegrator> RequestIncrementalOperationalIntegratorsTotes(DateTime from, DateTime to, RegisterDirections direction)
        {
            List<PowerIntegrator> integrators = new List<PowerIntegrator>();

            ASDU request = new C_CI_NU_2(
                this.MeassurePoint,
                new CP40Time2a(from),
                new CP40Time2a(to),
                direction);

            if (this.Send(request) != FunctionCodesPRM0.ACK)
                throw new Exception("Command not accepted");

            ASDU response = base.RequestData(this.LinkAddress);

            C_CI_NU_2 operationalsResponse = response as C_CI_NU_2;

            if (operationalsResponse == null)
                throw new Exception(
                    "Could not get operational integrators. Bad result.");

            if (operationalsResponse.Cause.Code == TransmissionCauses.IntegrationPeriodNotAvailable)
                return integrators;
            
            while (response != null && (response is C_CI_NU_2) ? (response as C_CI_NU_2).Cause.Code != TransmissionCauses.ActivationEnd : true)
            {
                response = base.RequestData(this.LinkAddress);

                M_IT_TK_2 toteResponse = response as M_IT_TK_2;

                if (toteResponse != null)
                {
                    integrators.Add(toteResponse.Integrator);
                }
            }

            return integrators;
        }

        /// <summary>
        /// Gets the operational integrational blocks
        /// </summary>
        /// <returns>Power integrators</returns>
        /// <param name="from">Date from to request the incremental operational integrators</param>
        /// <param name="to">Date to to request the incremental operational integrators</param>
        /// <param name="direction">Frecuency of the meter</param>
        /// <returns>List of power integrator read from the slave</returns>
        public List<PowerIntegrator> RequestOperationalIntegrationalBlocks(DateTime from, DateTime to, RegisterDirections direction)
        {
            List<PowerIntegrator> integrators = new List<PowerIntegrator>();

            ASDU request = new C_CB_NT_2(
                this.MeassurePoint,
                new CP40Time2a(from),
                new CP40Time2a(to),
                direction);

            if (this.Send(request) != FunctionCodesPRM0.ACK)
                throw new Exception("Command not accepted");

            ASDU response = base.RequestData(this.LinkAddress);

            C_CB_NT_2 operationalsResponse = response as C_CB_NT_2;

            if (operationalsResponse == null)
                throw new Exception(
                    "Could not get operational integrators. Bad result.");

            if (operationalsResponse.Cause.Code == TransmissionCauses.IntegrationPeriodNotAvailable)
                return integrators;

            while (response != null && ( response is C_CB_NT_2 ) ? ( response as C_CB_NT_2 ).Cause.Code != TransmissionCauses.ActivationEnd : true)
            {
                response = base.RequestData(this.LinkAddress);

                M_IB_TG_2 toteResponse = response as M_IB_TG_2;

                if (toteResponse != null)
                {
                    integrators.AddRange(toteResponse.Integrators);
                }
            }

            return integrators;
        }

        /// <summary>
        /// Gets the operational integrational blocks
        /// </summary>
        /// <returns>Power integrators</returns>
        /// <param name="from">Date from to request the incremental operational integrators</param>
        /// <param name="to">Date to to request the incremental operational integrators</param>
        /// <param name="direction">Frecuency of the meter</param>
        /// <returns>List of power integrator read from the slave</returns>
        public List<PowerIntegrator> RequestIncrementalOperationalIntegrationalBlocks(DateTime from, DateTime to, RegisterDirections direction)
        {
            List<PowerIntegrator> integrators = new List<PowerIntegrator>();

            ASDU request = new C_CB_NU_2(
                this.MeassurePoint,
                new CP40Time2a(from),
                new CP40Time2a(to),
                direction);

            if (this.Send(request) != FunctionCodesPRM0.ACK)
                throw new Exception("Command not accepted");

            ASDU response = base.RequestData(this.LinkAddress);

            C_CB_NU_2 operationalsResponse = response as C_CB_NU_2;

            if (operationalsResponse == null)
                throw new Exception(
                    "Could not get operational integrators. Bad result.");

            if (operationalsResponse.Cause.Code == TransmissionCauses.IntegrationPeriodNotAvailable)
                return integrators;
            
            while (response != null && ( response is C_CB_NU_2 ) ? ( response as C_CB_NU_2 ).Cause.Code != TransmissionCauses.ActivationEnd : true)
            {
                response = base.RequestData(this.LinkAddress);

                M_IB_TK_2 toteResponse = response as M_IB_TK_2;

                if (toteResponse != null)
                {
                    integrators.AddRange(toteResponse.Integrators);
                }
            }

            return integrators;
        }

        /// <summary>
        /// Current date time of the slave
        /// </summary>
        /// <returns>Datetime of the slave</returns>
        public DateTime GetDateTime()
        {
            ASDU request = new C_TI_NA_2(
                this.MeassurePoint);

            if (this.Send(request) != FunctionCodesPRM0.ACK)
                throw new Exception("Command not accepted");

            M_TI_TA_2 responseDate = base.RequestData(this.LinkAddress) as M_TI_TA_2;

            if (responseDate == null || !responseDate.DateTime.IV)
                throw new Exception(
                    "Could not read the date and time from the slave");

            return responseDate.DateTime.DateTime;
        }

        /// <summary>
        /// Sends a datetime to the slave to synchronize the slave
        /// </summary>
        /// <param name="value">Date time value to send</param>
        public void Synchronize(DateTime value)
        {
            ASDU request = new C_CS_TA_2(
                this.MeassurePoint,
                value);

            if (this.Send(request) != FunctionCodesPRM0.ACK)
                throw new Exception("Command not accepted");

            ASDU response = base.RequestData(this.LinkAddress);

            C_CS_TA_2 syncResponse = response as C_CS_TA_2;

            if (syncResponse == null)
                throw new Exception("There was a problem synchornizing the slave.");

            if (syncResponse.Cause.Code != TransmissionCauses.ActivationACK)
                throw new Exception(
                    String.Format(
                        "Could not synchronize the slave. Reason: {0}",
                        syncResponse.Cause.Code));
        }

        /// <summary>
        /// Request the instant values of the slave
        /// </summary>
        /// <returns>Instant values</returns>
        public InstantValues RequestInstantValues()
        {
            ASDU request = new C_IV_RQ(
                this.MeassurePoint);

            FunctionCodesPRM0 code = this.Send(request);

            if (this.Send(request) != FunctionCodesPRM0.ACK)
                throw new Exception("Command not accepted");

            ASDU response = base.RequestData(this.LinkAddress);

            M_IV_RP instantValuesResponse = response as M_IV_RP;

            if (instantValuesResponse == null)
                throw new Exception(
                    string.Format(
                        "Could not read the instant values. {0} code received.",
                        instantValuesResponse.Cause.Code));

            return new InstantValues(
                instantValuesResponse.InstantEnergyTotes,
                instantValuesResponse.InstantPowers,
                instantValuesResponse.InstantVI);
        }

        /// <summary>
        /// Request the tariff information for the given period
        /// </summary>
        /// <param name="from">Date from</param>
        /// <param name="to">Date to</param>
        /// <param name="contract">Contract whouse information is going to be retrieved</param>
        /// <returns>List of tariff information objects</returns>
        public List<TariffInformationObject> RequestTariffInformation(DateTime from, DateTime to, RegisterDirections contract)
        {
            List<TariffInformationObject> tariffInformations = new List<TariffInformationObject>();

            ASDU request = new C_TA_VM_2(
                this.MeassurePoint,
                contract,
                from,
                to);

            if (this.Send(request) != FunctionCodesPRM0.ACK)
                throw new Exception("Command not accepted");

            ASDU response = base.RequestData(this.LinkAddress) as C_TA_VM_2;

            C_TA_VM_2 informationResponse = response as C_TA_VM_2;

            if (informationResponse == null)
                throw new Exception("Could not get the tariff information with the given parameter");

            while (response != null && (response is C_TA_VM_2) ? (response as C_TA_VM_2).Cause.Code != TransmissionCauses.ActivationEnd : true)
            {
                response = base.RequestData(this.LinkAddress);

                M_TA_VM_2 tariffInformation = response as M_TA_VM_2;

                if (tariffInformation != null)
                    tariffInformations.Add(tariffInformation.TariffInformation);
            }

            return tariffInformations;
        }

        /// <summary>
        /// Request the tariff information for the given period
        /// </summary>
        /// <param name="contract">Contract whouse information is going to be retrieved</param>
        /// <returns>List of tariff information objects</returns>
        public List<TariffInformationObject> RequestCurrentTariffInformation(RegisterDirections contract)
        {
            List<TariffInformationObject> tariffInformations = new List<TariffInformationObject>();

            ASDU request = new C_TA_VC_2(
                this.MeassurePoint,
                contract);

            if (this.Send(request) != FunctionCodesPRM0.ACK)
                throw new Exception("Command not accepted");

            ASDU response =base.RequestData(this.LinkAddress) as C_TA_VC_2;

            C_TA_VC_2 informationResponse = response as C_TA_VC_2;

            if (informationResponse == null || informationResponse.Cause.Code != TransmissionCauses.ActivationACK)
                throw new Exception("Could not get the tariff information with the given parameter");

            if (informationResponse.Cause.Code == TransmissionCauses.DataRegisterNotAvailable)
                return tariffInformations;

            while (response != null && ( response is C_TA_VC_2 ) ? ( response as C_TA_VC_2 ).Cause.Code != TransmissionCauses.ActivationEnd : true)
            {
                response = base.RequestData(this.LinkAddress);

                M_TA_VC_2 tariffInformation = response as M_TA_VC_2;

                if (tariffInformation != null)
                    tariffInformations.Add(tariffInformation.TariffInformation);
            }

            return tariffInformations;
        }

        /// <summary>
        /// Gets the incidences from the slave
        /// </summary>
        /// <param name="from">Date from</param>
        /// <param name="to">Date to</param>
        /// <param name="direction">Object direction to read</param>
        /// <returns>List of incidences</returns>
        public List<Incidence> ReadEvent(DateTime from, DateTime to, RegisterDirections direction)
        {
            List<Incidence> incidences = new List<Incidence>();

            ASDU request = new C_SP_NB_2(
                this.MeassurePoint,
                direction,
                from,
                to);

            if (this.Send(request) != FunctionCodesPRM0.ACK)
                throw new Exception("Command not accepted");

            ASDU response = base.RequestData(this.LinkAddress);

            C_SP_NB_2 readEventRespone = response as C_SP_NB_2;            

            if (readEventRespone.Cause.Code == TransmissionCauses.DataRegisterNotAvailable)
                return incidences;

            while(response != null & (response is M_SP_TA_2) ? (response as M_SP_TA_2).Cause.Code != TransmissionCauses.ActivationEnd : true)
            {
                response = base.RequestData(this.LinkAddress);

                if(response is M_SP_TA_2)
                {
                    incidences.AddRange(( response as M_SP_TA_2 ).Incidences);
                }
                else
                {
                    break;
                }
            }

            return incidences;
        }

        /// <summary>
        /// Gets the configuration from the slave
        /// </summary>
        /// <returns>Slave configuration</returns>
        public MeassurePointConfiguration RequestConfiguration()
        {
            ASDU request = new C_RM_NA_2(
                this.MeassurePoint);

            if (this.Send(request) != FunctionCodesPRM0.ACK)
                throw new Exception("Command not accepted");

            ASDU response = base.RequestData(this.LinkAddress);

            M_RM_NA_2 configurationResponse = response as M_RM_NA_2;

            if (configurationResponse == null)
                throw new Exception(
                    "Could not get the configuration from the slave.");

            return configurationResponse.Configuration;
        }

        /// <summary>
        /// Gets the parameters from the point
        /// </summary>
        /// <returns>Slave parameters</returns>
        public MeassurePointParameters RequestSlaveParameters()
        {
            ASDU request = new C_PI_NA_2(
                this.MeassurePoint);

            if (this.Send(request) != FunctionCodesPRM0.ACK)
                throw new Exception("Command not accepted");

            ASDU response = base.RequestData(this.LinkAddress);

            P_ME_NA_2 parametersResponse = response as P_ME_NA_2;

            if (parametersResponse != null)
                return parametersResponse.Parameters;

            throw new Exception("Could not get the parameters from the endpoint");
        }

        /// <summary>
        /// Request current active contract powers
        /// </summary>
        /// <param name="direction">Direction of the object to read</param>
        public void RequestActiveContractPowers(RegisterDirections direction)
        {
            ASDU request = new C_PC_NA_2(
                this.MeassurePoint,
                direction);

            if (this.Send(request) != FunctionCodesPRM0.ACK)
                throw new Exception("Command not accepted");

            ASDU response = base.RequestData(this.LinkAddress);

            throw new NotImplementedException();
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Disposes the current instance
        /// </summary>
        void IDisposable.Dispose()
        {
            if(this.Activated)
                this.Disconnect();

            base.Dispose();

            GC.SuppressFinalize(this);
        }

        #endregion
    }

}
