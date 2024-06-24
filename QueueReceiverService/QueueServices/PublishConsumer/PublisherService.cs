using CommonResources;
using MassTransit;

namespace QueueReceiverService.QueueServices.PublishConsumer
{
    public class PublisherService : IConsumer<Client>
    {
        public async Task Consume(ConsumeContext<Client> context)
        {
            var info = context.Message;
        }
    }
}
