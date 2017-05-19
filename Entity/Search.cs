using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Search
    {
        public int page { get; set; }
        public int limit { get; set; }
        public string orderBy { get; set; }
        public string query { get; set; }
    }
}
