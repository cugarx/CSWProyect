using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCSW.infrastructure
{
    public class BookFull
    {
        public int IdBook { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public int IdAuthor { get; set; }
        public string Category { get; set; }
        public string Publisher { get; set; }
        public string Country { get; set; }

    }
}
