using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusZil_Core.Accounts
{
    public class AccountsRepository
    {
        public List<Account> Accounts { get; set; }

        public AccountsRepository(List<Account> accs = null)
        {
            Accounts = accs ?? new List<Account>();
        }
        public void Add(Account acc)
        {
            if (String.IsNullOrWhiteSpace(acc.Address.Raw))
            {
                throw new ArgumentException("Address is empty");
            }
            else if (Accounts.Any(a => a.Address.Equals(acc.Address)))
            {
                throw new ArgumentException("Account already exists in repository");
            }
            
            Accounts.Add(acc);
        }
        public void Remove(Account acc)
        {
            if (String.IsNullOrWhiteSpace(acc.Address.Raw))
            {
                throw new ArgumentException("Address is empty");
            }
            Accounts.Remove(acc);
        }
        public void Remove(string address)
        {
            var acc = Accounts.SingleOrDefault(a=> a.Address.Equals(address));
            if (acc != null && String.IsNullOrWhiteSpace(acc.Address.Raw))
            {
                throw new ArgumentException("Address is empty");
            }
            Accounts.Remove(acc);
        }
    }
}
