using Microsoft.AspNetCore.Identity;

namespace KitapSatis.Identity
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
