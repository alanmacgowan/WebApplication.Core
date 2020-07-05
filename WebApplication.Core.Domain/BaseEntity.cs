using System.ComponentModel.DataAnnotations;

namespace WebApplication.Core.Domain
{
    public class BaseEntity
    {
        public BaseEntity()
        {

        }

        [Key]
        public int Id { get; set; }
    }
}
