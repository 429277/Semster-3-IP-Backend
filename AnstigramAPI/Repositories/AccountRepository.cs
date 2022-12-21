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
            int userId = _context.Account.Where(account => account.AuthId == authId).Select(account => account.Id).FirstOrDefault();
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

        IEnumerable<Account> IAccountRepository.GetFollowedAccounts(string userId)
        {
            var query = from Ac in _context.Account
                        where Ac.AuthId == userId
                        from fl in _context.FollowerLogic
                        where fl.UserId == Ac.Id
                        from fo in _context.Account
                        where fo.Id == fl.FollowerId
                        select fo;
            //kijk naar methodes

            IEnumerable < Account > Accounts = query.ToList();

            return Accounts;
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
