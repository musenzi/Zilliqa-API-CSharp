using MusZil_Core;
using MusZil_Core.Utils;
using NUnit.Framework;
using NUnitTestMusZIL.IntegrationTests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace NUnitTestMusZIL.UtilityTests
{
    public class UtilTests
    {
        #region Encode/Decode
        [Test]
        public void AddressEncode()
        {
            var address = "0x551AA8653Aa7b75D9fDD75f5D4D41d0647F734E8"; 
            var address2 = "0xFd154D1340e4d0c5F443eEB37891aC0e4EC25605";
            var bech = MusBech32.Base16ToBech32Address(address);
            var bech2 = MusBech32.Base16ToBech32Address(address2);

            Assert.IsTrue(bech.StartsWith("zil") && bech2.StartsWith("zil"));
            Console.WriteLine("Starts with zil");
            Assert.AreEqual("zil125d2sef657m4m87awh6af4qaqerlwd8gv6fglj", bech);
            Console.WriteLine($"Address1 : zil125d2sef657m4m87awh6af4qaqerlwd8gv6fglj <-> {bech}");
            Assert.AreEqual("zil1l5256y6qungvtazra6eh3ydvpe8vy4s9rl87ec", bech2);
            Console.WriteLine($"Address1 : zil125d2sef657m4m87awh6af4qaqerlwd8gv6fglj <->{bech}");
        }

        [Test]
        public void AddressDecode()
        {
            var encStr = "zil1fs6jhg4axvj9ekscq6v7ddwxxd9tthpxl7820q";

            var dec = MusBech32.Bech32ToBase16Address(encStr);

            Assert.AreEqual("0x" + "4C352ba2Bd33245CDA180699e6B5c6334AB5dC26".ToUpper(), dec);
        }
        [Test]
        public void AddressDecodeEncode()
        {
            var encStr = "zil1fs6jhg4axvj9ekscq6v7ddwxxd9tthpxl7820q";
            var length = encStr.Length;
            var dec = MusBech32.Decode(encStr);
            var enc = MusBech32.Encode(dec);
            Assert.AreEqual(encStr,enc);
        }

        #endregion

        #region ByteUtil

        [Test]
        public void ConvertStringToHex()
        {
            var t = ByteUtil.LongToBaseN(15, 16);
            Assert.AreEqual("F", t);

        }
        [Test]
        public void ConvertStringToB32()
        {
            var t = ByteUtil.LongToBaseN(64, 32);
            Assert.AreEqual("20", t);
        }

        [Test]
        public void ConverToBinary()
        {
            var t = ByteUtil.LongToBaseN(8, 2);
            Assert.AreEqual("1000", t);
        }

        [Test]
        public void GetHexVal()
        {
            var t = ByteUtil.GetHexVal('F');
            Assert.AreEqual(t, 15);
        }
        [Test]
        public void GetHexString()
        {
            var s = ByteUtil.HexStringToByteArray("FF");
            var t = ByteUtil.ConvertByteArrToString(s);
            Assert.AreEqual(t, "11111111");
        }

        #endregion

        

    }
}
