//#define TCP

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using IEC60870_5_102;
using IEC60870_5_102.TransportLayer;
using IEC60870_5_102.ApplicationLayer;

using IEC60870_5_102.LinkLayer;

using IEC60870_5_102.Serialization;

using System.Runtime.Serialization;
using System.IO.Ports;

[DataContract]
class Meter
{
    [DataMember(IsRequired = true, Name = "Address", Order = 0)]
    public ushort Address { get; set; }

    [DataMember(IsRequired = true, Name = "Password", Order = 1)]
    public uint Password { get; set; }

    [DataMember( IsRequired = true, Name = "LinkLayerAddress", Order = 2)]
    public ushort LinkLayerAddress { get; set; }

    [DataMember(IsRequired = true, Name = "PortSpeed", Order = 3)]
    public ushort PortSpeed { get; set; }

    [DataMember(IsRequired = true, Name = "DataBits", Order = 4)]
    public int DataBits { get; set; }

    [DataMember(IsRequired = true, Name = "Parity", Order = 5)]
    public Parity Parity { get; set; }

    [DataMember(IsRequired = true, Name = "StopBits", Order = 6)]
    public StopBits StopBits { get; set; }
}

class Option
{
    public int ID { get; set; }

    public string Name { get; set; }

    public Action Action {get;set;}
}

namespace IEC60870_Test
{
    class Program
    {
        static void Main(string[] args)
        {

#if TCP

            using (Master master = new Master(
                41744,
                7,
                1,
                new TcpEndpoint(
                    "IP ADDRESS",
                    40001)))

#else

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

#endif
            {

                try
                {
                    master.Connect();

                    Console.WriteLine("Connected");

                    master.StartSession();

                    Console.WriteLine("Activated");

                    InstantValues v = master.RequestInstantValues();

                    //List<PowerIntegrator> integrators = master.RequestOperationalIntegratorsTotes(
                    //    DateTime.Now.AddDays(-2),
                    //    DateTime.Now,
                    //    RegisterDirections.HourlyTotes);

                    //MeassurePointConfiguration c = master.RequestConfiguration();

                    //List<TariffInformationObject> o = master.RequestCurrentTariffInformation(RegisterDirections.ContractIIITariffInformation);

                    //MeassurePointParameters p = master.RequestSlaveParameters();

                    master.EndSession();
                }
                catch(Exception e)
                {

                }
                finally
                {
                    master.Disconnect();
                }
            }
        }
    }
}
