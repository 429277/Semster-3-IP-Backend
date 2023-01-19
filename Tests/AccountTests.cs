using AnstigramAPI.DatabaseContext;
using AnstigramAPI.Models;
using AnstigramAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

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

        [TestMethod]
        public void TestFollowAccount()
        {
            //arrange
            var optionsBuilder = new DbContextOptionsBuilder<AccountContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new AccountContext(optionsBuilder.Options);

            Account a1 = new Account
            {
                Id = 1,
                AuthId = "AuthId1",
                Name = "Henk"
            };
            Account a2 = new Account
            {
                Id = 8,
                AuthId = "AuthId8",
                Name = "Harry"
            };
            context.Account.Add(a1);
            context.Account.Add(a2);
            context.SaveChanges();

            var repository = new AccountRepository(context);
            //act
            repository.FollowAccount(a1.AuthId, a2.Id);

            //assert
            Assert.AreEqual(context.Follower.First().UserId, a2.Id);
            Assert.AreEqual(context.Follower.First().FollowerId, a1.Id);
        }

        [TestMethod]
        public void TestUnFollowAccount()
        {
            //arrange
            var optionsBuilder = new DbContextOptionsBuilder<AccountContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new AccountContext(optionsBuilder.Options);

            Account a1 = new Account
            {
                Id = 1,
                AuthId = "AuthId1",
                Name = "Henk"
            };
            Account a2 = new Account
            {
                Id = 8,
                AuthId = "AuthId8",
                Name = "Harry"
            };
            FollowDTO follow = new FollowDTO(a2.Id, a1.Id);
            context.Follower.Add(follow);
            context.Account.Add(a1);
            context.Account.Add(a2);
            context.SaveChanges();

            var repository = new AccountRepository(context);
            //act
            repository.UnFollowAccount(a1.AuthId, a2.Id);

            //assert
            Assert.AreEqual(context.Follower.Count(), 0);
        }

        [TestMethod]
        public void TestGetFollowedAccounts()
        {
            //arrange
            var optionsBuilder = new DbContextOptionsBuilder<AccountContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new AccountContext(optionsBuilder.Options);

            Account a1 = new Account
            {
                Id = 1,
                AuthId = "AuthId1",
                Name = "Henk"
            };
            Account a2 = new Account
            {
                Id = 2,
                AuthId = "AuthId2",
                Name = "Harry"
            };
            Account a3 = new Account
            {
                Id = 8,
                AuthId = "AuthId8",
                Name = "Frank"
            };
            Account a4 = new Account
            {
                Id = 4,
                AuthId = "AuthId4",
                Name = "Niel"
            };
            FollowDTO follow = new FollowDTO(a2.Id, a1.Id);
            FollowDTO follow2 = new FollowDTO(a2.Id, a3.Id);

            context.Account.Add(a1);
            context.Account.Add(a2);
            context.Account.Add(a3);
            context.Account.Add(a4);
            context.Follower.Add(follow);
            context.Follower.Add(follow2);
            context.SaveChanges();

            var repository = new AccountRepository(context);
            //act
            IEnumerable<Account> followers = repository.GetFollowedAccounts(a2.AuthId);

            //assert
            Assert.AreEqual(2, followers.Count());
            Assert.AreSame(a1, followers.First());
        }
    }
}