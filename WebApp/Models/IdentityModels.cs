using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("PoleConnection")
        {
        }

        public System.Data.Entity.DbSet<WebApp.Models.PoleInfoViewModel> PoleInfoViewModels { get; set; }

        public System.Data.Entity.DbSet<WebApp.Models.UserInformation> UserInformations { get; set; }

        public System.Data.Entity.DbSet<WebApp.Models.AspNetRole> AspNetRoles { get; set; }

        public System.Data.Entity.DbSet<WebApp.Models.AspNetUser> AspNetUsers { get; set; }
    }
}