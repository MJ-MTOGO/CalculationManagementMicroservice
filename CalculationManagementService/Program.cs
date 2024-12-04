using CalculationManagementService.Application.Interfaces;
using CalculationManagementService.Application.Ports;
using CalculationManagementService.Application.Services;
using CalculationManagementService.Infrastructure.Publishers;
using CalculationManagementService.Infrastructure.Subscribers;

namespace CalculationManagementService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            // Register IMessageBus with GooglePubSubMessageBus
            builder.Services.AddSingleton<IMessageBus, GooglePubSubMessageBus>(sp =>
                new GooglePubSubMessageBus(builder.Configuration["GoogleCloud:ProjectId"]));

            // Register HttpClient for API calls
            builder.Services.AddHttpClient();

            // Register the CalculationService
            builder.Services.AddScoped<CalculationService>();

            // Register the SubscribeService
            builder.Services.AddSingleton<SubscribeService>();

            // Add controllers and Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            // Start the subscription service
            StartSubscriptions(app);

            app.Run();
        }

        private static void StartSubscriptions(WebApplication app)
        {
            // Start the subscription service
            var subscribeService = app.Services.GetRequiredService<SubscribeService>();
            _ = subscribeService.StartAsync(); // Fire-and-forget task
        }
    }
}
