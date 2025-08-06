using Microsoft.EntityFrameworkCore;
namespace SchoolFees.API.DataBase
{
    public class AplicationDBContext : DbContext
    {
        public AplicationDBContext(DbContextOptions<AplicationDBContext> options) : base(options) { }


    }
}
