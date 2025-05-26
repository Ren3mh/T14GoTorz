//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;
//using System.IO;

//namespace SharedLib.Data
//{
//    public class GotorzContextFactory : IDesignTimeDbContextFactory<GotorzContext>
//    {
//        public GotorzContext CreateDbContext(string[] args)
//        {
//            var configuration = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory()) // ensure it's where appsettings.json is
//                .AddJsonFile("appsettings.json", optional: false)
//                .Build();

//            var optionsBuilder = new DbContextOptionsBuilder<GotorzContext>();
//            var connectionString = configuration.GetConnectionString("DbConnectionString");

//            optionsBuilder.UseSqlServer(connectionString);

//            return new GotorzContext(optionsBuilder.Options);
//        }
//    }
//}
