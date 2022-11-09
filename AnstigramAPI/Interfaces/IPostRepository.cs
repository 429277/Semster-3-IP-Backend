using AnstigramAPI.Models;
using System.Collections.Generic;

namespace AnstigramAPI.Interfaces
{
    public interface IPostRepository : IGenericRepository<IPostRepository>
    {
        public IEnumerable<Post> GetPostsOfAccount(int userId);
        public IEnumerable<Post> GetFeedOfAccount(string userId);
    }
}
