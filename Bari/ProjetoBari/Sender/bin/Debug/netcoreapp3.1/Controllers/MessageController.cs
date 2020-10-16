using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sender.Model;
using Sender.Services;

namespace Sender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        readonly IBusControl _bus;
        readonly IMessageService _messageService;
        public MessageController(IBusControl bus, IMessageService messageService)
        {
            _bus = bus;
            _messageService = messageService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateMessage(Message message)
        {
            Uri uri = new Uri("rabbitmq://localhost:15672/message-queue");
 //           Task<ISendEndpoint> sendEndpointTask = _bus.GetSendEndpoint(uri);
 //           ISendEndpoint sendEndpoint = sendEndpointTask.Result;
 //           Task sendTask = sendEndpoint.Send<Message>(message);
            var endpoint = await _bus.GetSendEndpoint(uri);
            await endpoint.Send(message);
            return Ok("Sucess");
        }
    }
}
