namespace WebBanVeMayBay.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebBanVeMayBay.Models.WebBanVeMayBayDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "WebBanVeMayBay.Models.WebBanVeMayBayDBContext";
        }

        protected override void Seed(WebBanVeMayBay.Models.WebBanVeMayBayDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
