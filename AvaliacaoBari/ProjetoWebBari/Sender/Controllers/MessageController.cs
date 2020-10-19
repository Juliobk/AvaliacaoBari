using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consumer;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Sender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        readonly IBusControl _bus;
        readonly IPublishEndpoint _publishEndpoint;
        public MessageController(IBusControl bus, IPublishEndpoint publishEndpoint)
        {
            _bus = bus;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<ActionResult> CreateMessage(Message message)
        {
            Uri uri = new Uri("queue:message-queue");
            try
            {
                  ISendEndpoint sendEndpointTask = await _bus.GetSendEndpoint(uri);
                  await sendEndpointTask.Send<Message>(message);
               // await _publishEndpoint.Publish<Message>(message);
            }
            catch(Exception ex)
            {

            }
            return Ok("Sucess");
        }
    }
}
