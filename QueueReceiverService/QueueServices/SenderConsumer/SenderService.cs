using CommonResources;
using MassTransit;

namespace QueueReceiverService.QueueServices.SenderConsumer
{
    public class SenderService : IConsumer<Account>
    {
        public async Task Consume(ConsumeContext<Account> context)
        {
            var product = context.Message;
        }
    }
}
