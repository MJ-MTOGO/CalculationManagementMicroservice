using CalculationManagementService.Application.Ports;
using CalculationManagementService.Application.Services;
using CalculationManagementService.Infrastructure.Publishers;
using CalculationManagementService.Infrastructure.Subscribers;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddSingleton<IMessageBus, GooglePubSubMessageBus>(sp =>
            new GooglePubSubMessageBus(builder.Configuration["GoogleCloud:ProjectId"]));

        builder.Services.AddHttpClient();
        builder.Services.AddScoped<CalculationService>();

        // Register SubscribeService as a singleton
        builder.Services.AddSingleton<SubscribeService>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

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
