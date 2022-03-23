using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Order.API.Consumers;
using Order.Application;
using Order.Infrastructure;
using Shared;

namespace Order.API
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
            services.AddApplication();
            services.AddInfrastructure(Configuration, x =>
            {
                x.AddConsumer<StockNotReservedEventConsumer>();
                x.AddConsumer<PaymentFailedEventConsumer>();
                x.AddConsumer<PaymentSucceededEventConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(host: Configuration.GetConnectionString("RabbitMQ"), h =>
                    {
                        h.Username(Configuration.GetSection("RabbitMQ")["UserName"]);
                        h.Password(Configuration.GetSection("RabbitMQ")["Password"]);
                    });

                    cfg.ReceiveEndpoint(RabbitMQConsts.OrderPaymentSucceededQueueName, e =>
                    {
                        e.ConfigureConsumer<PaymentSucceededEventConsumer>(context);
                    });

                    cfg.ReceiveEndpoint(RabbitMQConsts.OrderPaymentFailedQueueName, e =>
                    {
                        e.ConfigureConsumer<PaymentFailedEventConsumer>(context);
                    });

                    cfg.ReceiveEndpoint(RabbitMQConsts.OrderStockNotReservedQueueName, e =>
                    {
                        e.ConfigureConsumer<StockNotReservedEventConsumer>(context);
                    });
                });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Order.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
