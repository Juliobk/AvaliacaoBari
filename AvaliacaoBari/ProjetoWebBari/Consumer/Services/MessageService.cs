using MassTransit;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Services
{
    public class MessageService : IMessageService
    {
        readonly IBusControl _bus;
        readonly IPublishEndpoint _publishEndpoint;
        public MessageService(IBusControl bus, IPublishEndpoint publishEndpoint)
        {
            _bus = bus;
            _publishEndpoint = publishEndpoint;
        }
        public async Task SendMessage(Message message)
        {
            try
            {
                Uri uri = new Uri("queue:message-queue");
                try
                {
                    ISendEndpoint sendEndpointTask = await _bus.GetSendEndpoint(uri);
                    await sendEndpointTask.Send<Message>(message);
                    // await _publishEndpoint.Publish<Message>(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    
    }
}
