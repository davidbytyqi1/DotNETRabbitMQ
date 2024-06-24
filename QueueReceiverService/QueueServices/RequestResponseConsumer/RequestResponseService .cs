using CommonResources;
using MassTransit;

namespace QueueReceiverService.QueueServices.RequestResponseConsumer
{
    public class RequestResponseService : IConsumer<TransferData>
    {

        public async Task Consume(ConsumeContext<TransferData> context)
        {
            var data = context.Message;


            var nowBalance = new CurrentBalance()
            {
                Balance = 1000 - data.Amount
            };

            await context.RespondAsync(nowBalance);
        }
    }
}
