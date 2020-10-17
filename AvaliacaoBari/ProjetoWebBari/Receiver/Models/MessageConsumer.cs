using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services;

namespace Models
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
            await _messageService.DisplayMessage(data.ID.ToString(), data.Text.ToString(), "https://localhost:44336/");
            //message received
        }
    }
}
