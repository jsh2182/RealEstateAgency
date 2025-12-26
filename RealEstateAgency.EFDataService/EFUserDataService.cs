using RealEstateAgency.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using RealEstateAgency.Models.Schema;

namespace RealEstateAgency.EFDataService
{
    public class EFUserDataService : EFBaseDataService<User, int>, DataService.IUserDataService
    {
        public EFUserDataService(AgencyContext context) : base(context)
        {

        }
        public override Expression<Func<User, int>> GetKey()
        {
            return u => u.UserID;
        }

        public User GetUserByName(string Username)
        {
            return Current.FirstOrDefault(c => c.UserName == Username);
        }

        public int Authenticate(string userName, string password)
        {
            string passwordDecrypted = Cryptography.RC2Encryption(password, Cryptography.cipherKey);
            var user = All().FirstOrDefault(u => u.UserName == userName && u.Password == passwordDecrypted);

            if (user != null)
            {
                return user.UserID;
            }
            else
            {
                return 0;
            }

        }

        public bool IsRegistered(string username, string password)
        {
            var q =
                from p in Current
                where p.UserName == username && p.Password == password
                select p;
            if (q.Count() > 0)
            {
                return true;
            }

            return false;
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            var usr = All().FirstOrDefault(c => c.UserName == username && c.Password == oldPassword);
            if (usr != null)
            {
                usr.Password = newPassword;
                Context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool HasUserAccessPage(string formName, string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return false;
            }

            User User = Current.FirstOrDefault(c => c.UserName == username);
            if (User == null)
            {
                return false;
            }

            var user = All().Where(o => o.UserName == username.Trim()).FirstOrDefault();
            var page = Context.PageLists.Where(o => o.Name == formName).FirstOrDefault();

            var permission = Context.PageListPermissions.Where(o => o.UserID == user.UserID && o.PageListID == page.PageID).FirstOrDefault();
            if (permission != null)
            {
                return permission.IsRead;
            }

            return false;
        }
        public IQueryable<UserSchema> Search(UserSchema model)
        {
            var qry = All();
            if (model.IsActive.HasValue)
                qry = qry.Where(q => q.IsActive == model.IsActive);
            if (model.UserID > 0)
                qry = qry.Where(q => q.UserID == model.UserID);
            if (!string.IsNullOrEmpty(model.UserName))
                qry = qry.Where(q => q.UserName.Contains(model.UserName));
            return qry.Select(q=> new UserSchema
            {
                IsActive = q.IsActive,
                Name = q.Name,
                UserID = q.UserID,
                UserName = q.UserName
            });

        }
    }
}
