using BMongo1.Models;

namespace BMongo1.Models.BusinessModels
{
    public interface IRepositoryAccount: IRepositoryGeneric<Account, int,Account>
    {
        Account GetEmail(string email );
        Account Login (string email, string password);
    }
}
