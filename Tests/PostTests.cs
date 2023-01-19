using AnstigramAPI.DatabaseContext;
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
        public void TestGetFeedOfAccount ()
        {
            //arrange
            var optionsBuilder = new DbContextOptionsBuilder<AccountContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new AccountContext(optionsBuilder.Options);

            PostDTO p1 = new PostDTO
            {
                Id = 1,
                UserId = 2,
                Caption = "Test1"
            };
            PostDTO p2 = new PostDTO
            {
                Id = 2,
                UserId = 2,
                Caption = "test2"
            };
            PostDTO p3 = new PostDTO
            {
                Id = 3,
                UserId = 6,
                Caption = "This post should not show up"
            };
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
                Name = "Jonny"
            };

            FollowDTO follow1 = new FollowDTO(a1.Id, a2.Id);

            context.Account.Add(a1);
            context.Account.Add(a2);
            context.Follower.Add(follow1);
            context.Post.Add(p1);
            context.Post.Add(p2);
            context.Post.Add(p3);
            context.SaveChanges();

            var repository = new PostRepository(context);
            //act
            IEnumerable<ReadPost> posts = repository.GetFeedOfAccount(a1.AuthId);

            //assert
            Assert.AreEqual(2, posts.Count());
            Assert.AreEqual(posts.First().Id, context.Post.First().Id);
        }

        [TestMethod]
        public void TestGetFeedOfAccountWhenUserDoesNotExist()
        {
            //arrange
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
                Id = 1,
                AuthId = "AuthId1",
                Name = "Henk"
            };
            Account a2 = new Account
            {
                Id = 2,
                AuthId = "AuthId2",
                Name = "Henk"
            };
            FollowDTO follow = new FollowDTO(a1.Id, a2.Id);

            context.Account.Add(a1);
            context.Follower.Add(follow);
            context.Post.Add(p1);
            context.Post.Add(p2);
            context.SaveChanges();

            var repository = new PostRepository(context);
            //act
            IEnumerable<ReadPost> posts = repository.GetFeedOfAccount("nonExistend");

            //assert
            Assert.AreEqual(posts.Count(), 0);
        }

        [TestMethod]
        public void TestCreatePost()
        {
            //arrange
            var optionsBuilder = new DbContextOptionsBuilder<AccountContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new AccountContext(optionsBuilder.Options);

            CreatePost createPost = new CreatePost
            {
                Caption = "1",
                AuthId = "AuthId1",
                Image = null
            };
            Account a1 = new Account
            {
                Id = 8,
                AuthId = "AuthId1",
                Name = "Henk"
            };
            context.Account.Add(a1);
            context.SaveChanges();

            var repository = new PostRepository(context);
            //act
            repository.Create(createPost);

            //assert
            Assert.AreEqual(createPost.Caption, context.Post.First().Caption);
            Assert.AreEqual(a1.Id, context.Post.First().UserId);
        }

        [TestMethod]
        public void TestGetMyPosts()
        {
            //arrange
            var optionsBuilder = new DbContextOptionsBuilder<AccountContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new AccountContext(optionsBuilder.Options);

            PostDTO myPost = new PostDTO
            {
                Id = 1,
                UserId = 8,
                Caption = "Test1"
            };
            PostDTO otherPost = new PostDTO
            {
                Id = 2,
                UserId = 2,
                Caption = "test2"
            };
            Account a1 = new Account
            {
                Id = 8,
                AuthId = "AuthId1",
                Name = "Henk"
            };
            context.Account.Add(a1);
            context.Post.Add(otherPost);
            context.Post.Add(myPost);
            context.SaveChanges();

            var repository = new PostRepository(context);
            //act
            IEnumerable<ReadPost> posts = repository.GetMyPosts(a1.AuthId);

            //assert
            Assert.AreEqual(posts.Count(), 1);
            Assert.AreEqual(posts.First().UserId, a1.Id);
        }

        [TestMethod]
        public void TestDeletePost()
        {
            //arrange
            var optionsBuilder = new DbContextOptionsBuilder<AccountContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new AccountContext(optionsBuilder.Options);

            PostDTO myPost = new PostDTO
            {
                Id = 1,
                UserId = 8,
                Caption = "Test1"
            };
            PostDTO otherPost = new PostDTO
            {
                Id = 2,
                UserId = 2,
                Caption = "test2"
            };

            context.Post.Add(otherPost);
            context.Post.Add(myPost);
            context.SaveChanges();

            var repository = new PostRepository(context);
            //act
            repository.DeletePost(1);

            //assert
            Assert.AreEqual(1, context.Post.Count());
            Assert.AreEqual(otherPost.Id, context.Post.First().Id);
        }

        [TestMethod]
        public void TestUpdatePost()
        {
            //arrange
            var optionsBuilder = new DbContextOptionsBuilder<AccountContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new AccountContext(optionsBuilder.Options);

            PostDTO myPost = new PostDTO
            {
                Id = 1,
                UserId = 8,
                Caption = "Test1"
            };

            context.Post.Add(myPost);
            context.SaveChanges();

            var repository = new PostRepository(context);

            string newCaption = "new caption";
            //act
            repository.UpdatePost(new UpdatePost(newCaption, 1));

            //assert
            Assert.AreEqual(newCaption, context.Post.First().Caption);
        }
    }
}
