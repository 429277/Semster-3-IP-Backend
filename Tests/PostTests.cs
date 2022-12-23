using AnstigramAPI.DatabaseContext;
using AnstigramAPI.Logic.Models;
using AnstigramAPI.Models;
using AnstigramAPI.Models.Post;
using AnstigramAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class PostTests
    {
        [TestMethod]
        public void TestGetPosts ()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AccountContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new AccountContext(optionsBuilder.Options);

            PostDTO p1 = new PostDTO
            {
                Id = 1,
                UserId = 8,
                Caption = "Test1"
            };
            PostDTO p2 = new PostDTO
            {
                Id = 2,
                UserId = 8,
                Caption = "test2"
            };
            Account a1 = new Account
            {
                Id = 8,
                AuthId = "AuthId1",
                Name = "Henk"
            };

            context.Account.Add(a1);
            context.Post.Add(p1);
            context.Post.Add(p2);
            context.SaveChanges();

            var repository = new PostRepository(context);
            IEnumerable<ReadPost> posts = repository.GetFeedOfAccount("AuthId1");

            Assert.AreEqual(context.Post.Count(), posts.Count());
            Assert.AreEqual(posts.First().Id, context.Post.First().Id);
        }
    }
}
