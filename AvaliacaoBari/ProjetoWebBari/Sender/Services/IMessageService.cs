using Sender.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sender.Services
{
    public interface IMessageService
    {
        Task<string> SendMessage(string ID, string Text, string Url);
    }
}
