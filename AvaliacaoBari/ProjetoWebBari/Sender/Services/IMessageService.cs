using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public interface IMessageService
    {
        Task<string> SendMessage(string ID, string Text, string Url);
        Task<string> DisplayMessage(string Text, string Url);
    }
}
