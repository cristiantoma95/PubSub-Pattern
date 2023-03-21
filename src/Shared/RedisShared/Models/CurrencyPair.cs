using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisShared.Models
{
    public class CurrencyPair
    {
        public string Symbol { get; set; }

        public decimal Value { get; set; }

        public long Timestamp { get; set; }
    }
}
