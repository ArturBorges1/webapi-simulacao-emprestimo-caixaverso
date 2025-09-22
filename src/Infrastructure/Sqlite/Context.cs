using Caixaverso.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Caixaverso.Backend.Infrastructure.Sqlite.Context
{
    public class SqliteContext : DbContext
    {
        public SqliteContext() { }
        public SqliteContext(DbContextOptions<SqliteContext> options) : base(options)
        {
        }

        public virtual DbSet<Produto> Produtos { get; set; }
    }
}
