using BTI_Project1_API.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTI_Project1_API.Models
{
    public class _Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        public string GithubLink { get; set; }
        public string LinkedInLink { get; set; }

        [IgnoreCopy]
        public Project[] Projects { get; set; }
    }
}
