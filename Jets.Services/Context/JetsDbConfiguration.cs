using System.Data.Entity.Migrations;

namespace Jets.Services.Context
{
    public class JetsDbConfiguration : DbMigrationsConfiguration<JetsDatabaseContext>
    {
        public JetsDbConfiguration()
        {
            AutomaticMigrationDataLossAllowed = false;
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(JetsDatabaseContext context)
        {
            base.Seed(context);
        }
    }
}
