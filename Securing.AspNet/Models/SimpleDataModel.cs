using System.ComponentModel.DataAnnotations;

namespace Securing.AspNet.Models
{
    public class SimpleDataModel
    {
        [Required]
        public string Name { get; set; }
    }
}