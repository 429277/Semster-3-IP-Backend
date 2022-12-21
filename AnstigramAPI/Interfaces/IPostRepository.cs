using AnstigramAPI.Models.Post;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AnstigramAPI.Interfaces
{
    public interface IPostRepository
    {
        public IEnumerable<ReadPost> GetPostsOfAccount(int userId);
        public IEnumerable<ReadPost> GetFeedOfAccount(string userId);
        public void Create(CreatePost post);
        public IEnumerable<ReadPost> GetMyPosts(string authId);
    }
}
