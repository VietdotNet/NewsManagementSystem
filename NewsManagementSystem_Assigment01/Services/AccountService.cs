using Microsoft.EntityFrameworkCore;
using NewsManagementSystem_Assigment01.Models;
using NewsManagementSystem_Assigment01.Repositories;
using NewsManagementSystem_Assigment01.ViewModel;

namespace NewsManagementSystem_Assigment01.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _repo;
        public AccountService(IAccountRepository repo) 
        {
            _repo = repo;            
        }
        public SystemAccount? CheckLogin(LoginViewModel model)
        {
            var user = _repo.SignIn(model);
            return user;
        }

        public List<SystemAccount> GetListUser() 
        {
            return _repo.GetListUser();
        }

        public SystemAccount? GetAccountById(short id) 
        {
            return _repo.GetAccountById(id);
        }

        public void AccountStatus(SystemAccount account)
        {
            _repo.AccountStatus(account);
        }

        public void Register(SystemAccount account)
        {
           _repo.CreateNewAccount(account);
        }


    }
}
