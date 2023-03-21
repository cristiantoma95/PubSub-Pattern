using System.ComponentModel.DataAnnotations;

namespace CentralBank.Models
{
    public sealed class ReferenceIndex
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public static string CurrencySymbol => "RON";

        [Required]
        public float Index { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; }
    }
}
