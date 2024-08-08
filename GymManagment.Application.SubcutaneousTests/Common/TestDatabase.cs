using GymManagement.Infrastructure.Common.Presistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.SubcutaneousTests.Common
{
    // In Subcutaneous tests we aren't testing integration with a real database,
    // so even if we weren't using SQLite we would use some in-memory database.
    public class SqliteTestDatabase : IDisposable
    {
        public SqliteConnection Connection { get; }

        public static SqliteTestDatabase CreateAndInitialize()
        {
            var testDatabase = new SqliteTestDatabase("DataSource=:memory:");

            testDatabase.InitializeDatabase();

            return testDatabase;
        }

        public void InitializeDatabase()
        {
            Connection.Open();

            var options = new DbContextOptionsBuilder<GymManagementDbContext>()
                .UseSqlite(Connection)
                .Options;

            var context = new GymManagementDbContext(options, null!, null!);

            context.Database.EnsureCreated();
        }

        public void ResetDatabase()
        {
            Connection.Close();

            InitializeDatabase();
        }

        private SqliteTestDatabase(string connectionString)
        {
            Connection = new SqliteConnection(connectionString);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
