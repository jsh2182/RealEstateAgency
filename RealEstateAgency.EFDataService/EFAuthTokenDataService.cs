using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;

namespace RealEstateAgency.EFDataService
{
    public class EFAuthTokenDataService : EFBaseDataService<AuthToken, long>, DataService.IAuthTokenDataService
    {
        public EFAuthTokenDataService(AgencyContext context):base(context)
        {

        }
        public override Expression<Func<AuthToken, long>> GetKey()
        {
            return a => a.TokenID;
        }
        public AuthTokenSchema GenerateToken(int userID, string AuthExpiry)
        {
            string token = Guid.NewGuid().ToString();
            DateTime issuedOn = DateTime.Now;
            DateTime expiredOn = DateTime.Now.AddSeconds(Convert.ToDouble(AuthExpiry));
            var tokendomain = new AuthToken
            {
                UserID = userID,
                Token = token,
                IssuedOn = issuedOn,
                ExpiresOn = expiredOn
            };

            Add(tokendomain);
            var tokenModel = new AuthTokenSchema()
            {
                UserID = userID,
                IssuedOn = issuedOn,
                ExpiresOn = expiredOn,
                Token = token
            };

            return tokenModel;
        }

        public bool ValidateToken(string token, string AuthExpiry)
        {
            var authToken = All().FirstOrDefault(t => t.Token == token && t.ExpiresOn > DateTime.Now);
            if (authToken != null && !(DateTime.Now > authToken.ExpiresOn))
            {
                authToken.ExpiresOn = authToken.ExpiresOn.AddSeconds(Convert.ToDouble(AuthExpiry));
                Update(authToken);
                //Context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Kill(string token)
        {
            Delete(All().FirstOrDefault(o => o.Token == token));
            var isNotDeleted = All().Where(x => x.Token == token).Any();
            if (isNotDeleted) { return false; }
            return true;
        }
        public bool DeleteByUserId(int userID)
        {
            Delete(All().FirstOrDefault(o => o.UserID == userID));

            var isNotDeleted = All().Where(x => x.UserID == userID).Any();
            return !isNotDeleted;
        }
    }
}
