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

        public IEnumerable<ReadPost> GetFeedOfAccount(string authId)
        {
            //var q = from Ac in _context.Account 
            //        where Ac.AuthId == authId
            //        from fl in _context.Follower
            //        where fl.FollowerId == Ac.Id
            //        from po in _context.Post
            //        where po.UserId == fl.UserId
            //        from pun in _context.Account
            //        where pun.Id == po.UserId
            //        select new
            //        {
            //            post = po,
            //            username = pun.Name
            //        };

            //List<ReadPost> posts = new List<ReadPost>();
            List<ReadPost> posts = new List<ReadPost>();

            try
            {
                int userId = _context.Account.Where(a => a.AuthId == authId).First().Id;
                List<FollowDTO> follewedAccounts = _context.Follower.Where(f => f.FollowerId == userId).ToList();
                foreach (FollowDTO follow in follewedAccounts)
                {

                    List<ReadPost> postsOfUser = _context.Post.Where(p => p.UserId == follow.UserId)
                    .Join(_context.Account,
                    p => p.UserId,
                    a => a.Id,
                    (p, a) => new ReadPost(p, a.Name)).ToList();
                    foreach (ReadPost post in postsOfUser)
                    {
                        posts.Add(post);
                    }
                }
            }
            catch { posts.Clear(); }


            return posts;
        }

        IEnumerable<ReadPost> IPostRepository.GetPostsOfAccount(int userId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ReadPost> GetMyPosts(string authId)
        {
            IEnumerable<PostDTO> query = _context.Post;
            IEnumerable<Account> query2 = _context.Account;

            IEnumerable<ReadPost> posts = _context.Post
                   .Join(_context.Account.Where(account => account.AuthId == authId),
                    p => p.UserId,
                    a => a.Id,
                    (p, a) => new ReadPost(p, a.Name)
                    ).ToList();
            return posts;
        }

        public void DeletePost(int postId)
        {
            _context.Post.Remove(_context.Post.Where(p => p.Id == postId).FirstOrDefault());
            _context.SaveChanges();
        }

        public void UpdatePost(UpdatePost updatedPost)
        {
            _context.Post.Where(p => p.Id == updatedPost.Id).FirstOrDefault().Caption = updatedPost.Caption;
            _context.SaveChanges();
        }
    }
}
