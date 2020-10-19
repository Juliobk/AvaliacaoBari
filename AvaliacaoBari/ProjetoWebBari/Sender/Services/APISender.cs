using Consumer;
using Consumer.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class APISender : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public APISender(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var _messageService = scope.ServiceProvider.GetRequiredService<IMessageService>();
            string idServico = Guid.NewGuid().ToString().Substring(1, 7);

            while (true)
            {
                string timeStamp = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                string IDRequisition = Guid.NewGuid().ToString().Substring(1, 7);
                Console.WriteLine("Serviço " + idServico + " enviou a mensagem " + IDRequisition);
                await _messageService.SendMessage(new Message() { ID = IDRequisition, Text = " TimeStamp: " + timeStamp + " ID Requisicao: " + IDRequisition + " ID Serviço: " + idServico });
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken); //Run every 5 seconds
            }
        }
    }
}
