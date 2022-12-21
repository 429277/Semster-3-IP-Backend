using AnstigramAPI.DatabaseContext;
using AnstigramAPI.Models;
using AnstigramAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AccountContext>()
                .UseInMemoryDatabase(databaseName:Guid.NewGuid().ToString());
            var context = new AccountContext(optionsBuilder.Options);

            Account a1 = new Account
            {
                Id = 1,
                Name = "Harry"
            };
            Account a2 = new Account
            {
                Id = 2,
                Name = "Henk"
            };
            
            context.Account.Add(a1);
            context.Account.Add(a2);
            context.SaveChanges();

            var repository = new AccountRepository(context);
            IEnumerable<Account> accounts = repository.GetAccountRecommends();
            
            Assert.AreSame(accounts, context.Account);

        }
    }
}