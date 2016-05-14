using System.ComponentModel.DataAnnotations;

namespace Securing.AspNet.ApiModels
{
    public class PostValueModel
    {
        [Required]
        public string Value { get; set; }
    }
}