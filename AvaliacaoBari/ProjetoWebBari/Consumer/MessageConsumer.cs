using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consumer.Services;
using System.IO;

namespace Consumer
{
    public class MessageConsumer : IConsumer<Message>
    {
        private readonly IMessageService _messageService;
        public MessageConsumer(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public async Task Consume(ConsumeContext<Message> context)
        {
            var data = context.Message;
            Console.WriteLine(data.Text);
            //message received
        }
    }
}
