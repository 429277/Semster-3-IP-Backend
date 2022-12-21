using AnstigramAPI.Models;
using AnstigramAPI.Models.Post;
using Microsoft.EntityFrameworkCore;

namespace AnstigramAPI.DatabaseContext
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options)
            : base(options)
        {

        }

        public DbSet<Account> Account { get; set; }
        public DbSet<FollowerLogic> FollowerLogic { get; set;}
        public DbSet<PostDTO> Post { get; set; }
        public DbSet<FollowDTO> Follower { get; set; }

    }
}
