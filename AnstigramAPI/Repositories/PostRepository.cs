using AnstigramAPI.DatabaseContext;
using AnstigramAPI.Interfaces;
using AnstigramAPI.Models;
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
        void IGenericRepository<IPostRepository>.Create(IPostRepository obj)
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<Post> IPostRepository.GetFeedOfAccount(string userId)
        {
            IEnumerable<PostDTO> query = _context.Post;
            //where Ac.AuthId == userId
            //from fl in _context.FollowerLogic
            //where fl.UserId == Ac.Id
            //from fo in _context.Account
            //where fo.Id == fl.FollowerId
            //select fo;

            //IEnumerable<Account> Accounts = query.ToList();
            IEnumerable<Post> h;
            List<Post> niks = new();
            foreach (PostDTO postDTO in query)
            {
                Post post = new Post(postDTO);
                niks.Add(post);
            }
            h = niks;

            return h;
        }

        IEnumerable<Post> IPostRepository.GetPostsOfAccount(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
