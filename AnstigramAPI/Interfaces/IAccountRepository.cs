using AnstigramAPI.Models;
using System.Collections.Generic;

namespace AnstigramAPI.Interfaces
{
    public interface IAccountRepository : IGenericRepository<IAccountRepository>
    {
        public Account GetUser(string Id);
        public IEnumerable<Account> GetFollowedAccounts(string userId);
        public IEnumerable<Account> GetFollowingAccounts(string userId);
        public IEnumerable<Account> GetAccountRecommends();
        public IEnumerable<Account> SearchForAccounts(string searchterm);
        public void FollowAccount(string authId, int followerUserId);

    }
}
