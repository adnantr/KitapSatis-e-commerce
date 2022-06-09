using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KitapSatis.Identity
{
    public class ApplicationUserContext:IdentityDbContext<User>
    {
        public ApplicationUserContext(DbContextOptions<ApplicationUserContext> options):base(options)
        {

        }
    }
}
