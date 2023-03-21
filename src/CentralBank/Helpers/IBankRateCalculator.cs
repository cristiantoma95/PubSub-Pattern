using CentralBank.Dtos;
using RedisShared.Models;

namespace CentralBank.Helpers
{
    public interface IBankRateCalculator
    {
        InterestRate CalculateBankInterestRates(ReferenceIndexCreateDto referenceIndexCreate, string currency);
        List<InterestRate> AdjustRatesForDeposits(List<InterestRate> interestRates);
    }
}
