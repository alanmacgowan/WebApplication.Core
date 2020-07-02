using System.ComponentModel.DataAnnotations;

namespace WebApplication.Core.UI.Entities
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
