using BTI_Project1_API.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTI_Project1_API.Models
{
    public class _Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Explanation { get; set; }
        public string GithubLink { get; set; }

        [IgnoreCopy]
        public Person[] People { get; set; }
    }
}
