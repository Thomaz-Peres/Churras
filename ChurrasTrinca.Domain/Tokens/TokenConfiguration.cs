using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurrasTrinca.Domain.Tokens
{
    public class TokenConfiguration
    {
        public string Public { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }
}
