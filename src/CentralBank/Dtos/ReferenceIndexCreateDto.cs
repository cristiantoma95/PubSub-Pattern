using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CentralBank.Dtos
{
    [DataContract]
    public sealed class ReferenceIndexCreateDto
    {
        public static string CurrencySymbol => "RON";

        [DataMember]
        [Required]
        public float Index { get; set; }

        [DataMember]
        [Required]
        public DateTime TimeStamp { get; set; }
    }
}
