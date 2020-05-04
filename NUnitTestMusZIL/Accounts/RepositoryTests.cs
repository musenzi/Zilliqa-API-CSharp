using System;
using System.Collections.Generic;
using System.Text;
using MusZil_Core.Accounts;
using NUnit.Framework;

namespace NUnitTestMusZIL.Accounts
{
    public class RepositoryTests
    {
        private Account _testAccount;
        private AccountsRepository _repo;
        [SetUp]
        public void Setup()
        {
            _repo = new AccountsRepository();
        }

    }
}
