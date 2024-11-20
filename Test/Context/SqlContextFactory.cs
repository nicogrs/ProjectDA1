using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Test.Context;

public class SqlContextFactory
{
        public SqlContext CreateMemoryContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqlContext>();
            
            optionsBuilder.UseInMemoryDatabase("BaseDeDatosEnMemoria");

            return new SqlContext(optionsBuilder.Options);
        }
}