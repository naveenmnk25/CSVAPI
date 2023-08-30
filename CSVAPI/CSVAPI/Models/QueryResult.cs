using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSVAPI.Models
{
    [NotMapped]
    [Keyless]
    public class QueryResult
    {
        public string JsonResult { get; set; }
    }
}
