using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kursach.Client.Models
{
    public class Option
    {
        public string Name { get; set; } = "";
        public List<string> Values { get; set; } = new List<string>();

        public Option()
        {

        }
    }
}
