using System;

namespace RealEstateAgency.Models
{
    public class AuthToken
    {
        public long TokenID { get; set; }
        public int UserID { get; set; }
        public string Token { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
        public virtual User User { get; set; }
    }
}
