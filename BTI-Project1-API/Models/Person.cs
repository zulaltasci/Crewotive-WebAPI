using BTI_Project1_API.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BTI_Project1_API.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        [IgnoreCopy]
        public string Password { get; set; } 

        public string Name { get; set; } 

        public string Surname { get; set; }
        public string Role { get; set; }
        public string GithubLink { get; set; }
        public string LinkedInLink { get; set; }

        [IgnoreCopy]
        public bool IsActive { get; set; }

        [IgnoreCopy]
        public string ProjectIds { get; set; }
    }
}
