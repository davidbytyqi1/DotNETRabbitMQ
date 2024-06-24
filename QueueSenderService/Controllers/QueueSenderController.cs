using CommonResources;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace QueueSenderService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueueSenderController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IRequestClient<TransferData> _client;

        public QueueSenderController(IBus bus, IRequestClient<TransferData> client)
        {
            _bus = bus;
            _client = client;
        }

        [HttpPost("send-command")]
        public async Task<IActionResult> SendCommand()
        {
            var account = new Account()
            {
                Name = "David Bytyqi",
                Deposit = 500
            };

            var url = new Uri("rabbitmq://localhost/send-command");

            var endpoint = await _bus.GetSendEndpoint(url);
            await endpoint.Send(account);

            return Ok("Command sent successfully");
        }

        [HttpPost("publish-event")]
        public async Task<IActionResult> PublishEvent()
        {
            await _bus.Publish(new Client()
            {
                Name = "David Bytyqi",
                Pin = 123456
            });

            return Ok("Event published successfully");
        }

        [HttpPost("request-response")]
        public async Task<IActionResult> RequestResponse()
        {
            var requestData = new TransferData()
            {
                Type = "Withdrawal",
                Amount = 25
            };
            var request = _client.Create(requestData);
            var response = await request.GetResponse<CurrentBalance>();

            return Ok(response);
        }
    }
}
