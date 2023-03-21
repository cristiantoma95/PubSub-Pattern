namespace CentralBank.Dtos
{
    public class ReferenceIndexReadDto
    {
        public int Id { get; set; }

        public static string CurrencySymbol { get; set; }

        public float Index { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
