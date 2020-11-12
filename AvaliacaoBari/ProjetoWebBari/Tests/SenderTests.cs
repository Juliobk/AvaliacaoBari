using Consumer.Services;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class SenderTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public SenderTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        [Fact]
        public async Task SendMessageTest()
        {
            //Arrange
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IHostedService, APISender>();

            //mock the dependencies for injection
            services.AddSingleton(Mock.Of<IMessageService>(_ =>
                _.SendMessage(It.IsAny<Consumer.Message>()) == Task.CompletedTask
            ));
            services.AddSingleton(Mock.Of<IServiceScopeFactory>());


            var serviceProvider = services.BuildServiceProvider();
            var hostedService = serviceProvider.GetService<IHostedService>();

            //Act
            await hostedService.StartAsync(CancellationToken.None);
            await Task.Delay(6000);
            await hostedService.StopAsync(CancellationToken.None);

            //Assert
            var messageService = serviceProvider
                .GetRequiredService<IMessageService>();
            var mock = Mock.Get(messageService);

            //assert expected behavior
            mock.Verify(_ => _.SendMessage(It.IsAny<Consumer.Message>()), Times.AtLeastOnce);

        }
    }
}
