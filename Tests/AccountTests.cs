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
    public class AccountTests
    {
        [TestMethod]
        public void TestGetAccountReccomends()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AccountContext>()
                .UseInMemoryDatabase(databaseName:Guid.NewGuid().ToString());
            var context = new AccountContext(optionsBuilder.Options);

            //arrange
            Account a1 = new Account {Id = 1, Name = "Harry"};
            Account a2 = new Account {Id = 2, Name = "Henk"};
            
            context.Account.Add(a1);
            context.Account.Add(a2);
            context.SaveChanges();

            var repository = new AccountRepository(context);

            //act
            IEnumerable<Account> accounts = repository.GetAccountRecommends();
            
            //assert
            Assert.AreSame(accounts, context.Account);
        }
    }
}