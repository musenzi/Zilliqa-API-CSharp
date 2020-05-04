using MusZil_Core.Crypto;
using MusZil_Core.Transactions;
using MusZil_Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusZil_Core.Accounts
{
    public class Wallet
    {
        private AccountsRepository _repo;
        private Zilliqa _zil;
        private Account _curr;
        public Wallet(Account acc = null,bool test = true)
        {
            _repo = new AccountsRepository();
            _repo.Add(acc);
            _zil = test ? new Zilliqa() : new Zilliqa(Zilliqa.MAINNET);
            _curr = acc;
        }
        public void AddAccount(Account acc)
        {
            _repo.Add(acc);
        }
        public void AddAccountWithPrivateKey(string pk)
        {
            _repo.Add(new Account(pk));
        }
        public void AddAccountWithKeystore(string pk)
        {
            _repo.Add(new Account(pk));
        }
        public Account GetAccount(Account acc)
        {
            return _repo.GetAccount(acc);
        }
        public Account GetAccount(string address,Address.AddressEncoding encoding = Address.AddressEncoding.BECH32)
        {
            return _repo.GetAccount(address,encoding);
        }
        public Balance GetBalance(Account acc)
        {
            var balance = _zil.GetBalance(acc).Result;
            return balance;
        }
        public TransactionPayload Sign(TransactionPayload transaction)
        {
            if (transaction.ToAddr.ToUpper().StartsWith("0X"))
            {
                transaction.ToAddr = transaction.ToAddr.Substring(2);
            }
            
            string address = CryptoUtil.GetAddressFromPublicKey(transaction.PubKey).ToUpper();
            
            if (_curr == null)
            {
                throw new Exception("Could not sign the transaction with" + address + "  as it does not exist");
            }
            return SignWith(transaction, _curr);
            
        }

        public TransactionPayload SignWith(TransactionPayload tx, Account signer, bool getNonce = false)
        {
            
            if (signer == null)
            {
                throw new Exception("account not exists");
            }
            try
            {
                if (getNonce)
                {
                    var result = _zil.GetBalance(signer.Address).Result;
                    tx.Nonce = (int)result.Nonce + 1;
                }
            }
            catch (Exception e)
            {
                throw new Exception("cannot get nonce", e);
            }
            
            tx.PubKey = signer.GetPublicKey();
            byte[] message = tx.Encode();
            Signature signature = Schnorr.Sign(signer.KeyPair, message);
            tx.Signature = (signature.ToString().ToLower());
            return tx;
        }

        public static int Pack(int a, int b)
        {
            return (a << 16) + b;
        }
    }
}
