using CSVAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface ICSVDbContext
    {
        DbSet<Column> Columns { get; set; }
        DbSet<ColumnValue> ColumnValues { get; set; }
        DbSet<Company> Companies { get; set; }
        DbSet<QueryResult> QueryResult { get; set; }
        DbSet<ExecuteResult> ExecuteResult { get; set; }
    }
}
