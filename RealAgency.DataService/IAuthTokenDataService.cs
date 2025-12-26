using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;

namespace RealEstateAgency.DataService
{
    public interface IAuthTokenDataService : IBaseDataService<AuthToken, long>
    {
        AuthTokenSchema GenerateToken(int userID, string AuthExpiry);
        bool ValidateToken(string token, string AuthExpiry);
        bool Kill(string token);
        bool DeleteByUserId(int userID);
    }
}
