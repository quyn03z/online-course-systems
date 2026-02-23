using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess.Persistence
{
    /// <summary>
    /// Chỉ dùng bởi EF CLI khi chạy migrations (design-time).
    /// Connection string runtime vẫn lấy từ appsettings.json qua DI.
    /// </summary>
    public class OCMSMSFContextFactory : IDesignTimeDbContextFactory<OCMSMSFContext>
    {
        public OCMSMSFContext CreateDbContext(string[] args)
        {
            var connectionString = "server=.; database=OnlineCourseMSF;TrustServerCertificate=True;Trusted_Connection=True;";

            var optionsBuilder = new DbContextOptionsBuilder<OCMSMSFContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new OCMSMSFContext(optionsBuilder.Options);
        }
    }
}
