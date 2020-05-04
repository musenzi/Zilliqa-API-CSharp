using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusZil_Core.Accounts
{
    public class AccountsRepository
    {
        private List<Account> _accounts;

        public AccountsRepository(List<Account> accs = null)
        {
            _accounts = accs ?? new List<Account>();
        }
        public AccountsRepository(Account acc): this(new List<Account>())
        {
            _accounts.Add(acc);
        }
        public void Add(Account acc)
        {
            if (String.IsNullOrWhiteSpace(acc.Address.Raw))
            {
                throw new ArgumentException("Address is empty");
            }
            else if (_accounts.Any(a => a.Address.Equals(acc.Address)))
            {
                throw new ArgumentException("Account already exists in repository");
            }
            
            _accounts.Add(acc);
        }
        public void Remove(Account acc)
        {
            if (String.IsNullOrWhiteSpace(acc.Address.Raw))
            {
                throw new ArgumentException("Address is empty");
            }
            _accounts.Remove(acc);
        }
        public void Remove(string address)
        {
            var acc = _accounts.SingleOrDefault(a=> a.Address.Equals(address));
            if (acc != null && String.IsNullOrWhiteSpace(acc.Address.Raw))
            {
                throw new ArgumentException("Address is empty");
            }
            _accounts.Remove(acc);
        }
        public Account GetAccount(Account acc)
        {
            return _accounts.SingleOrDefault(a => a.Address.Equals(acc.Address));
        }
        public Account GetAccount(Address address)
        {
            return _accounts.SingleOrDefault(a => a.Address.Equals(address));
        }
        public Account GetAccount(string address,Address.AddressEncoding encoding)
        {
            Account acc = null;
            if(encoding == Address.AddressEncoding.BASE16)
                acc = _accounts.SingleOrDefault(a => a.Address.Base16.Equals(address));
            else
                acc = _accounts.SingleOrDefault(a => a.Address.Bech32.Equals(address));

            return acc;
        }
    }
}
