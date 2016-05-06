using System.Data.Entity;
using Banners.Data.Migrations;
using Banners.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Banners.Data
{
    public class BannersDbContext : IdentityDbContext<ApplicationUser>
    {
        public BannersDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BannersDbContext, Configuration>());
        }

        public static BannersDbContext Create()
        {
            return new BannersDbContext();
        }

        public IDbSet<Banner> Banners { get; set; }
    }
}
