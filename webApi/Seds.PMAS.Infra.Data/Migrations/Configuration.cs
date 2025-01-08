namespace Seds.PMAS.Infra.Data.Migrations
{
    using Seds.PMAS.Infra.Data.Context;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DBPMASContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DBPMASContext context)
        {
   
        }
    }
}
