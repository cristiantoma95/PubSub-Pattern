using CentralBank.Dtos;
using CentralBank.Helpers;
using RedisShared.Models;
using RedisShared.Streaming;

namespace CentralBank.DataSynchronizer
{
    public sealed class Publisher : IPublisher
    {
        private readonly IEnumerable<string> _currencyList = new List<string> { "EUR", "USD", "GBP" };
        private readonly IStreamPublisher _streamPublisher;
        private readonly IBankRateCalculator _bankRateCalculator;

        public Publisher(IStreamPublisher streamPublisher, IBankRateCalculator bankRateCalculator)
        {
            _streamPublisher = streamPublisher;
            _bankRateCalculator = bankRateCalculator;
        }

        public async Task PublishData(ReferenceIndexCreateDto referenceIndexCreate)
        {
            var loanInterestRates = _currencyList.Select(currency => _bankRateCalculator.CalculateBankInterestRates(referenceIndexCreate, currency)).ToList();
            await _streamPublisher.PublishAsync(Events.LoanInterestRates, loanInterestRates);

            var depositInterestRates = _bankRateCalculator.AdjustRatesForDeposits(loanInterestRates);
            await _streamPublisher.PublishAsync(Events.DepositInterestRates, depositInterestRates);
        }
    }
}
