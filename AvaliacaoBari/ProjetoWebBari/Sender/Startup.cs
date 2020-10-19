using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consumer;
using Consumer.Services;
using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services;

namespace Sender
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMassTransit(collection =>
            {
                collection.AddConsumer<MessageConsumer>();
                Console.WriteLine("Vai configurar");
                collection.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var rabbitHostName = Environment.GetEnvironmentVariable("RABBIT_HOSTNAME");
                    if (string.IsNullOrEmpty(rabbitHostName))
                    {
                        rabbitHostName = "localhost";
                    }
                    cfg.UseHealthCheck(provider);
                    cfg.Host(new Uri(@"rabbitmq://"+ rabbitHostName +"/"), settings =>
                    {
                        settings.Username("userbari");
                        settings.Password("userbari");
                    });
                    cfg.ReceiveEndpoint("message-queue", ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.Consumer<MessageConsumer>(provider);
                    });
                }));

            });
            services.AddTransient<IMessageService, MessageService>();
            services.AddSingleton<IHostedService, APISender>();
            Console.WriteLine("Configurou");
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
               options.IdleTimeout = TimeSpan.FromMinutes(15);//You can set Time   
           });

            services.AddMassTransitHostedService();
            services.AddControllers();
            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();
            app.Use((context, next) =>
            {
                var url = context.Request.GetDisplayUrl().Replace("weatherforecast", "");
                context.Session.SetString("URL", url);
                return next.Invoke();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
