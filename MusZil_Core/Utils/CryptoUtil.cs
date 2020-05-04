﻿using MusZil_Core.Crypto;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.EC;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using ECPoint = Org.BouncyCastle.Math.EC.ECPoint;

namespace MusZil_Core.Utils
{
    public class CryptoUtil
    {
        

        #region Keystore utils
        /**
         * The parameters of the secp256k1 curve that Bitcoin uses.
         */
        
        private static KeyStore keystore = new KeyStore();
        private static string pattern = "^(0x)?[0-9a-f]";


        public static string GeneratePrivateKey()
        {
            ECKeyPair keys = Schnorr.GenerateKeyPair();
            return keys.PrivateKey.ToString(8);
        }

        public static string GetAddressFromPrivateKey(string privateKey)
        {
            string publicKey = GetPublicKeyFromPrivateKey(privateKey, true);
            return GetAddressFromPublicKey(publicKey);
        }

        public static bool IsBytestring(string address)
        {
            System.Console.WriteLine(address);

            MatchCollection matchs = Regex.Matches(address, pattern);
            foreach (Match match in matchs)
            {
                System.Console.WriteLine(match.Groups);
            }
            return true;
        }

        /**
         * @param privateKey hex string without 0x
         * @return
         */
        public static string GetPublicKeyFromPrivateKey(string privateKey, bool compressed)
        {
            BigInteger bigInteger = new BigInteger(privateKey, 16);
            ECPoint point = GetPublicPointFromPrivate(bigInteger);
            return ByteUtil.ByteArrayToHexString(point.GetEncoded(compressed));
        }

        public static string GetAddressFromPublicKey(string publicKey)
        {
            SHA256 s = new SHA256Managed();
            byte[] address = s.ComputeHash(ByteUtil.HexStringToByteArray(publicKey));
            //byte[] address = GetAddressFromPublicKey);
            return ByteUtil.ByteArrayToHexString(address).Substring(24);
        }

        public static byte[] GenerateRandomBytes(int size)
        {
            byte[] bytes = new byte[size];
            new SecureRandom().NextBytes(bytes);
            return bytes;
        }

        private static ECPoint GetPublicPointFromPrivate(BigInteger privateKeyPoint)
        {
            var CURVE_PARAMS = CustomNamedCurves.GetByName("secp256k1");
            var CURVE = new ECDomainParameters(CURVE_PARAMS.Curve, CURVE_PARAMS.G, CURVE_PARAMS.N, CURVE_PARAMS.H);
            if (privateKeyPoint.BitLength > CURVE.N.BitLength)
            {
                privateKeyPoint = privateKeyPoint.Mod(CURVE.N);
            }
            return new FixedPointCombMultiplier().Multiply(CURVE.G, privateKeyPoint);
        }

        public static string DecryptPrivateKey(string file, string passphrase)
        {
            return keystore.DecryptPrivateKey(file, passphrase);
        }

        public static string EncryptPrivateKey(string privateKey, string passphrase, KDFType type)
        {
            return keystore.EncryptPrivateKey(privateKey, passphrase, type);
        }
        
        #endregion
    }
}
