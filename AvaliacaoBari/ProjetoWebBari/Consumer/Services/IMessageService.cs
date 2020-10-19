using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer.Services
{
    public interface IMessageService
    {
        Task SendMessage(Message message);
    }
}
