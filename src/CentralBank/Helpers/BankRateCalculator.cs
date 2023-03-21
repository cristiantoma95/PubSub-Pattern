using CentralBank.Dtos;
using RedisShared.Models;

namespace CentralBank.Helpers
{
    public sealed class BankRateCalculator : IBankRateCalculator
    {
        private const int DepositIndexAdjuster = -2;

        public InterestRate CalculateBankInterestRates(ReferenceIndexCreateDto referenceIndexCreate, string currency) =>
            new()
            {
                Currency = currency,
                Value = referenceIndexCreate.Index + NextFloat(),
                TimeStamp = referenceIndexCreate.TimeStamp
            };

        public List<InterestRate> AdjustRatesForDeposits(List<InterestRate> interestRates)
        {
            interestRates.ForEach(e => e.Value += DepositIndexAdjuster);
            return interestRates;
        }

        private static float NextFloat()
        {
            var random = new Random();
            var val = random.NextDouble() * (2 - 1) + 1;
            return (float) val;
        }
    }
}
