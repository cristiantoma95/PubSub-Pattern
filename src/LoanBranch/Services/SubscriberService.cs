using RedisShared.Models;
using RedisShared.Streaming;

namespace LoanBranch.Services
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
            await _streamSubscriber.SubscribeAsync<List<InterestRate>>(Events.LoanInterestRates, LoanInterestRatesEventHandler);
        }

        private static void LoanInterestRatesEventHandler(List<InterestRate> interestRateList)
        {
            foreach (var rate in interestRateList)
            {
                Console.WriteLine($"LoanBranch Subscriber --> The Loan interest rate for a consumer credit in {rate.Currency} is {rate.Value:F2}% for the date of {rate.TimeStamp}");
            }
        }
    }
}
