using NewsManagementSystem_Assigment01.Models;
using NewsManagementSystem_Assigment01.ViewModel;

namespace NewsManagementSystem_Assigment01.Repositories
{
    public interface IAccountRepository
    {
        SystemAccount? SignIn(LoginViewModel model);

        List<SystemAccount> GetListUser ();

        SystemAccount? GetAccountById(short Id); 

        void AccountStatus(SystemAccount account);
    }
}
