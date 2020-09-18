using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using IEC60870_5_102.LinkLayer;
using IEC60870_5_102.Serialization;
using IEC60870_5_102.ApplicationLayer;

namespace IEC60870_5_102_Test
{
    [TestClass]
    public class ApplicationLayerTests
    {
        [TestMethod]
        public void DateTimeToCP40Time2aSerializationTest()
        {
            DateTime dateTime = new DateTime(2016, 11, 29, 15, 26, 0);
            byte[] dataToCheck = { 0x1A, 0x0F, 0x5D, 0x0B, 0x10 };

            CP40Time2a CP40DateTime = new CP40Time2a(dateTime);

            using (BinaryEncoder encoder = new BinaryEncoder())
            {
                CP40DateTime.Encode(encoder);

                byte[] encodedData = encoder.ToArray();

                Assert.AreEqual(encodedData.Length, dataToCheck.Length);

                for (int ii = 0; ii < encodedData.Length; ii++)
                {
                    Assert.AreEqual(encodedData[ii], dataToCheck[ii]);
                }
            }
        }

        [TestMethod]
        public void CP40Time2aToDateTimeDeserializationTest()
        {
            DateTime dateToCheck = new DateTime(2016, 11, 29, 15, 26, 0);
            byte[] dataToDeserialize = { 0x1A, 0x0F, 0x1D, 0x0B, 0x10 };

            using (BinaryDecoder decoder = new BinaryDecoder(dataToDeserialize))
            {
                CP40Time2a decodedTime = new CP40Time2a();
                decodedTime.Decode(decoder);

                Assert.AreEqual((DateTime)decodedTime, dateToCheck);
            }
        }

        [TestMethod]
        public void DateTimeToCP56Time2aSerializationTest()
        {
            DateTime dateTime = new DateTime(2016, 11, 23, 20, 09, 24, 576);
            byte[] dataToCheck = { 0x00, 0x60, 0x09, 0x14, 0x77, 0x0B, 0x10 };

            CP56Time2a CP56DateTime = new CP56Time2a(dateTime);

            using (BinaryEncoder encoder = new BinaryEncoder())
            {

                CP56DateTime.Encode(encoder);

                byte[] encodedData = encoder.ToArray();

                Assert.AreEqual(encodedData.Length, dataToCheck.Length);

                for (int ii = 0; ii < encodedData.Length; ii++)
                {
                    Assert.AreEqual(encodedData[ii], dataToCheck[ii]);
                }
            }
        }

        [TestMethod]
        public void CP56Time2aToDateTimeDeserializationTest()
        {
            DateTime dateToCheck = new DateTime(2016, 11, 23, 20, 09, 24, 576);
            byte[] dataToDeserialize = { 0x00, 0x60, 0x09, 0x14, 0x77, 0x0B, 0x10 };

            using (BinaryDecoder decoder = new BinaryDecoder(dataToDeserialize))
            {
                CP56Time2a decodedTime = new CP56Time2a();
                decodedTime.Decode(decoder);

                Assert.AreEqual((DateTime)decodedTime, dateToCheck);
            }
        }

        [TestMethod]
        public void PasswordTelegramSerializationTest()
        {
            byte[] dataToCheck = { 0x01, 0x06, 0x01, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00 };

            C_AC_NA_2 passwordTelegram = new C_AC_NA_2(
                1,
                1);

            using (BinaryEncoder encoder = new BinaryEncoder())
            {
                passwordTelegram.Encode(encoder);

                byte[] encodedData = encoder.ToArray();

                for (int ii = 0; ii < encodedData.Length; ii++)
                {
                    Assert.AreEqual(encodedData[ii], dataToCheck[ii]);
                }
            }
        }

        [TestMethod]
        public void RequestOperationalIntegratorsSerializationTest()
        {
            byte[] dataToCheck = { 0x01, 0x06, 0x01, 0x00, 0x0b, 0x01, 0x06, 0x00, 0x00, 0x41, 0x01, 0x08, 0x00, 0x00, 0x21, 0x0C, 0x08 };

            C_CI_NT_2 request = new C_CI_NT_2(
                1,
                new CP40Time2a(
                    new DateTime(2008, 1, 1, 0, 0, 0)),
                new CP40Time2a(
                    new DateTime(2008, 12, 1, 0, 0, 0)),
                RegisterDirections.HourlyTotes);

            using (BinaryEncoder encoder = new BinaryEncoder())
            {
                request.Encode(encoder);

                byte[] encodedData = encoder.ToArray();

                for (int ii = 0; ii < encodedData.Length; ii++)
                {
                    Assert.AreEqual(encodedData[ii], dataToCheck[ii]);
                }
            }
        }
                

        [TestMethod]
        public void OperationalIntegratorDeserializationTest()
        {
            byte[] dataToDecode = { 122, 1, 7, 1, 0, 11, 1, 6, 0, 0, 1, 1, 8, 0, 0, 1, 12, 08 };

            using (System.IO.Stream stream = new System.IO.MemoryStream(dataToDecode))
            {
                BinaryDecoder decoder = new BinaryDecoder(stream);

                C_CI_NT_2 telegram = ASDU.GetASDU(decoder) as C_CI_NT_2;

                Assert.AreNotEqual(null, telegram);
                Assert.AreEqual(1, telegram.ASDUDirection.MeassurePoint);
                Assert.AreEqual(RegisterDirections.HourlyTotes, telegram.ASDUDirection.RegisterDirection);
                Assert.AreEqual(new TransmissionCause(TransmissionCauses.ActivationACK), telegram.Cause);
                Assert.AreEqual(new DateTime(2008, 1, 1, 0, 0, 0), telegram.TimeFrom.DateTime);
                Assert.AreEqual(new DateTime(2008, 12, 1, 0, 0, 0), telegram.TimeTo.DateTime);
            }
        }


        [TestMethod]
        public void V_I_SerialziationTest()
        {
            byte[] dataToCheck = { 0xec, 0x03, 0x00, 0x34, 0x09, 0x00, 0x80, 0x5d, 0x02, 0x00, 0xe2, 0x04, 0x00, 0x00, 0x28, 0x00, 0x00, 0xa8, 0x0f, 0x00, 0x80, 0x22, 0x04, 0x66, 0x02, 0x08 };

            V_I structure = new V_I(
                new Phase(100.4f, 235.6f, false),
                new Phase(60.5f, 125.1f, true),
                new Phase(4, 400.9f, false),
                new CP40Time2a(new DateTime(2008, 2, 6, 4, 34, 7)));

            using (BinaryEncoder encoder = new BinaryEncoder())
            {
                structure.Encode(encoder);

                byte[] encodedData = encoder.ToArray();

                string s = Utils.ByteArrayToString(encodedData, ", ");

                for (int ii = 0; ii < encodedData.Length; ii++)
                {
                    Assert.AreEqual(encodedData[ii], dataToCheck[ii]);
                }
            }
        }


        [TestMethod]
        public void V_I_DeserializationnTest()
        {
            byte[] dataToDecode = { 0xec, 0x03, 0x00, 0x34, 0x09, 0x00, 0x80, 0x5d, 0x02, 0x00, 0xe2, 0x04, 0x00, 0x00, 0x28, 0x00, 0x00, 0xa8, 0x0f, 0x00, 0x80, 0x22, 0x04, 0x66, 0x02, 0x08 };

            using (BinaryDecoder decoder = new BinaryDecoder(dataToDecode))
            {
                V_I structure = new V_I();

                structure.Decode(decoder);

                Assert.AreEqual(structure, new V_I(
                    new Phase(100.4f, 235.6f, false),
                    new Phase(60.5f, 125.1f, true),
                    new Phase(4, 400.9f, false),
                    new CP40Time2a(new DateTime(2008, 2, 6, 4, 34, 7))));
            }
        }
    }
}
