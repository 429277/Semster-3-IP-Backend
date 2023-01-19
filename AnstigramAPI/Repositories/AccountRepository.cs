using AnstigramAPI.DatabaseContext;
using AnstigramAPI.Interfaces;
using AnstigramAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace AnstigramAPI.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountContext _context;

        public AccountRepository(AccountContext accountContext)
        {
            _context = accountContext;
        }

        public void FollowAccount(string authId, int followUserId)
        {
            int userId = _context.Account.Where(account => account.AuthId == authId).Select(account => account.Id).First();
            _context.Follower.Add(new FollowDTO(userId, followUserId));
            _context.SaveChanges();
        }

        public IEnumerable<Account> GetAccountRecommends()
        {
            return _context.Account;
        }

        void IGenericRepository<IAccountRepository>.Create(IAccountRepository obj)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Account> GetFollowedAccounts(string authId)
        {
            //var query = from Ac in _context.Account
            //            where Ac.AuthId == authId
            //            from fl in _context.Follower
            //            where fl.UserId == Ac.Id
            //            from fo in _context.Account
            //            where fo.Id == fl.FollowerId
            //            select fo;
            //IEnumerable<Account> Accounts = query.ToList();
            
            int userId = _context.Account.Where(a => a.AuthId == authId).First().Id;
            List<FollowDTO> follewedAccounts = _context.Follower.Where(f => f.FollowerId == userId).ToList();
            List<Account> accounts = new List<Account>();
            foreach (FollowDTO follow in follewedAccounts) 
            {
                accounts.Add(_context.Account.Where(a => a.Id == follow.UserId).First());
            }
            
            return accounts;
        }

        IEnumerable<Account> IAccountRepository.GetFollowingAccounts(string userId)
        {
            throw new System.NotImplementedException();
        }

        Account IAccountRepository.GetUser(string Id)
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<Account> IAccountRepository.SearchForAccounts(string searchterm)
        {
            throw new System.NotImplementedException();
        }
    }
}
