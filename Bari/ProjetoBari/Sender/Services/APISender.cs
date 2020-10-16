using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sender.Services
{
    public class APISender : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
     //   private readonly IMessageService _messageService;
        public APISender(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var _messageService = scope.ServiceProvider.GetRequiredService<IMessageService>();

            while (true)
            {
                string timeStamp = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                string IDRequisition = Guid.NewGuid().ToString().Substring(1, 7);
                await _messageService.SendMessage(IDRequisition, " TimeStamp: "+ timeStamp + "\n ID Requisicao: " + IDRequisition + "\n ID Serviço: 123", "https://localhost:44381/api/message");
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken); //Run every 5 seconds
            }
        }
    }
}
