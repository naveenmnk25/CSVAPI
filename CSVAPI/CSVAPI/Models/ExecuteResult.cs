using System.ComponentModel.DataAnnotations.Schema;

namespace CSVAPI.Models
{
    [NotMapped]
    public class ExecuteResult
    {
        public int Result { get; set; }
    }
}
