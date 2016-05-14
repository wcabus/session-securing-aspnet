using System;
using System.ComponentModel.DataAnnotations;

namespace Securing.AspNet.ApiModels
{
    public class ValueModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Value { get; set; }
    }
}