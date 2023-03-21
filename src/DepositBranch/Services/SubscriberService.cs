using RedisShared.Models;
using RedisShared.Streaming;

namespace DepositBranch.Services
{
    internal sealed class SubscriberService : BackgroundService
    {
        private readonly IStreamSubscriber _streamSubscriber;

        public SubscriberService(IStreamSubscriber streamSubscriber)
        {
            _streamSubscriber = streamSubscriber ?? throw new ArgumentNullException(nameof(streamSubscriber));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _streamSubscriber.SubscribeAsync<List<InterestRate>>(Events.DepositInterestRates, DepositInterestRatesEventHandler);
        }

        private static void DepositInterestRatesEventHandler(List<InterestRate> interestRateList)
        {
            foreach (var rate in interestRateList)
            {
                Console.WriteLine($"DepositBranch Subscriber --> The interest rate for a consumer Deposit in {rate.Currency} is {rate.Value:F2}% for the date of {rate.TimeStamp}");
            }
        }
    }
}
