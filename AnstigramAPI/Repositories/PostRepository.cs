using AnstigramAPI.DatabaseContext;
using AnstigramAPI.Interfaces;
using AnstigramAPI.Models;
using AnstigramAPI.Models.Post;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace AnstigramAPI.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AccountContext _context;

        public PostRepository(AccountContext accountContext)
        {
            _context = accountContext;
        }

        public void Create(CreatePost post)
        {
            int userId = _context.Account.Where(account => account.AuthId == post.AuthId).Select(account => account.Id).FirstOrDefault();
            PostDTO postDTO = new PostDTO(post);
            postDTO.UserId = userId;
            _context.Post.Add(postDTO);
            _context.SaveChanges();
        }

        public IEnumerable<ReadPost> GetFeedOfAccount(string userId)
        {
            IEnumerable<PostDTO> query = _context.Post;
            IEnumerable<Account> query2 = _context.Account;
            //where Ac.AuthId == userId
            //from fl in _context.FollowerLogic
            //where fl.UserId == Ac.Id
            //from fo in _context.Account
            //where fo.Id == fl.FollowerId
            //select fo;

            //IEnumerable<Account> Accounts = query.ToList();
            IEnumerable<ReadPost> h = _context.Post
                .Join(_context.Account,
                p => p.UserId,
                a => a.Id,
                (p, a) => new ReadPost(p, a.Name)
                ).ToList();

            //List<ReadPost> niks = new();
            //foreach (PostDTO postDTO in query)
            //{
            //    string username = query2.Where(account => account.AuthId == postDTO.AuthId).Select(account => account.Name).FirstOrDefault();

            //    ReadPost post = new ReadPost(postDTO, "frank");
            //    niks.Add(post);
            //}
            //h = niks;

            return h;
        }

        IEnumerable<ReadPost> IPostRepository.GetPostsOfAccount(int userId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ReadPost> GetMyPosts(string authId)
        {
            IEnumerable<PostDTO> query = _context.Post;
            IEnumerable<Account> query2 = _context.Account;

            IEnumerable<ReadPost> h = _context.Post
                   .Join(_context.Account.Where(account => account.AuthId == authId),
                    p => p.UserId,
                    a => a.Id,
                    (p, a) => new ReadPost(p, a.Name)
                    ).ToList();
            return h;
        }
    }
}
