using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;
using System.Linq;

namespace RealEstateAgency.DataService
{
    public interface IUserDataService : IBaseDataService<User, int>
    {
        User GetUserByName(string Username);
        int Authenticate(string userName, string password);
        bool IsRegistered(string username, string password);
        bool ChangePassword(string username, string oldPassword, string newPassword);
        bool HasUserAccessPage(string formName, string username);
        IQueryable<UserSchema> Search(UserSchema model);
    }
}
