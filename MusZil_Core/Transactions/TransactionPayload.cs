using Google.Protobuf;
using MusZil_Core.Utils;
using MusZilCore.Proto;
using Newtonsoft.Json;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace MusZil_Core.Transactions
{
    public class TransactionPayload
    {
        [JsonProperty("version")]
        public int Version { get; set; }
        [JsonProperty("nonce")]
        public int Nonce { get; set; }
        [JsonProperty("toAddr")]
        public string ToAddr { get; set; }
        [JsonProperty("amount")]
        public string Amount { get; set; }
        [JsonProperty("pubKey")]
        public string PubKey { get; set; }
        [JsonProperty("gasPrice")]
        public string GasPrice { get; set; }
        [JsonProperty("gasLimit")]
        public string GasLimit { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("data")]
        public string Data { get; set; }
        [JsonProperty("signature")]
        public string Signature { get; set; }
        [JsonProperty("priority")]
        public bool Priority { get; set; }

        /// <summary>
        /// Set version using the decimal conversion of the bitwise concatenation of CHAIN_ID and MSG_VERSION parameters.
        /// </summary>
        /// <param name="testnet"></param>
        public void SetVersion(bool testnet)
        {
            //TODO Change to actually do the conversion here
            Version = testnet ? 21823489 : 65537;
        }

        public byte[] Encode()
        {

            BigInteger amount = BigInteger.ValueOf(long.Parse(Amount));
            BigInteger gasPrice = BigInteger.ValueOf(long.Parse(GasPrice));

            ProtoTransactionInfo info = new ProtoTransactionInfo();
            info.Version = (uint)Version;
            info.Nonce = (ulong)Nonce;
            info.Toaddr = ByteString.CopyFrom(ByteUtil.HexStringToByteArray(ToAddr.ToLower()));
            info.Senderpubkey = new ByteArray() { Data = ByteString.CopyFrom(ByteUtil.HexStringToByteArray(PubKey)) };
            info.Amount = new ByteArray() { Data = ByteString.CopyFrom(BigIntegers.AsUnsignedByteArray(16, amount)) };
            info.Gasprice = new ByteArray() { Data = ByteString.CopyFrom(BigIntegers.AsUnsignedByteArray(16, gasPrice)) };
            info.Gaslimit = ulong.Parse(GasLimit);
            if (!string.IsNullOrEmpty(Code))
            {
                info.Code = ByteString.CopyFrom(System.Text.Encoding.Default.GetBytes(Code));
            }

            if (!string.IsNullOrEmpty(Data))
            {
                info.Data = ByteString.CopyFrom(System.Text.Encoding.Default.GetBytes(Data));
            }


            return info.ToByteArray();

        }

       
    }
}
