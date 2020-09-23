# IEC60870-5-102

<h2>Introduction</h2>

<p>Library for communicating with meters or devices supporting IEC60870-5-102. The library implements the link layer and most ASDUs from the application layer
of the protocol. It supports communication over:

<ul>
<li>Serial (either RS232 or RS485)</li>
<li>Modem (supporting AT commands over data calls)</li>
<li>TCP sockets</li>
</ul>

The list of ASDUS currently implemented is:

<ul>
<li>C_AC_NA_2</li>
<li>C_CB_NT_2</li>
<li>C_CB_NU_2</li>
<li>C_CI_NT_2</li>
<li>C_CI_NU_2</li>
<li>C_CS_TA_2</li>
<li>C_FS_NA_2</li>
<li>C_IV_RQ</li>
<li>C_PC_NA_2</li>
<li>C_RM_NA_2</li>
<li>C_SP_NB_2</li>
<li>C_TA_VC_2</li>
<li>C_TA_VM_2</li>
<li>C_TI_NA_2</li>
<li>M_IB_TG_2</li>
<li>M_IB_TK_2</li>
<li>M_IT_TG_2</li>
<li>M_IT_TK_2</li>
<li>M_IV_RP</li>
<li>M_RM_NA_2</li>
<li>M_SP_TA_2</li>
<li>M_TA_VC_2</li>
<li>M_TA_VM_2</li>
<li>M_TI_TA_2</li>
<li>M_ME_NA_2</li>
</ul>

</p>



<h2>Getting started</h2>

<h3>Creating a Master</h3>

<p>The project containing the implementation of the library is called <b>IEC60870-5-102</b> and must be added to the project in which is going to be used
as a reference. All the messages exchanged with a device are handled by a <b>Master</b>. The namespaces to be included are:

<p>

```
using IEC60870_5_102;
using IEC60870_5_102.TransportLayer;
```

<p>The first namespace contains the definition of the Master class and the second one the different transport mediums supported by the library. In order to
create a Master its link address, password and meassure point have to be know. That way a master can be implemented as:</p>

```
 using (Master master = new Master(
                41744,
                7,
                1,
                new TcpEndpoint(
                    "IP ADDRESS",
                    40001)))
```

<p>Where the IP address of the server and the port have to be changed. In case a modem connection is used:</p>

```
using (Master master = new Master(
              5197,
              7,
              1,
              new ModemEndpoint(
                  "CHANGE BY PHONE NUMBER ACCORDINGLY",
                  "COM10",
                  9600,
                  System.IO.Ports.Parity.None,
                  8, 
                  System.IO.Ports.StopBits.One,
                  "CHANGE BY SIM PIN")))
```

<p>Where the information of the modem connected has to be changed accordingly. In case a direct connection is used"</p>

```
using (Master master = new Master(
              5197,
              7,
              1,
              new SerialEndpoint(
                  "COM10",
                  9600,
                  System.IO.Ports.Parity.None,
                  8, 
                  System.IO.Ports.StopBits.One)))
```
<h3>Creating the connection</h3>

<p>Once a master has been instantiated the connection between the master and the meter can be stablished. That is achieved 
by calling the method <b>Connect</b>. That will create the TCP connection, open the serial port and make a data call to the
configured endpoint depending on the parameters used for creating the Master. Depending on the messages to be exchanged with 
the device a session might be or not be needed. The master creates a new session with the device by sending the password.</p>

```
master.Connect();

master.StartSession();
```

<h3>Reading Operational integrators totes</h3>
<p>Once the connection has been stablished the Master is able to send and receive messages from the device or meter. These 
operations have been implemented through methods of the Master class. In order to read the integrator totes the method 
<b>RequestOperationalTotes</b> can be used:<p/>

```
List<PowerIntegrator> integrators = master.RequestOperationalIntegratorsTotes(
             DateTime.Now.AddDays(-2),
             DateTime.Now,
             RegisterDirections.HourlyTotes);
```

<h2>Sample application</h2>
<p>A sample application can be found under the project <b>IEC60870 Test</b></p> but the details of the connection and the meter have to be adapted.</p>
