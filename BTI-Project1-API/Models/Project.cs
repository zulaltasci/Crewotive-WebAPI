using BTI_Project1_API.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BTI_Project1_API.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Explanation { get; set; }
        public string GithubLink { get; set; }

        [IgnoreCopy]
        public string PersonIds { get; set; }
    }
}
