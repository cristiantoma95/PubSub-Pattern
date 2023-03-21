using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisShared.Models
{
    public class InterestRate
    {
        public string? Currency { get; set; }

        public float Value { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
