using Data.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NFC.Data;
using NFC.Data.Entities;
using NFCDataService;
using NFCDataService.Models;
using NFCDataService.Services;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<NFCDbContext>(options =>
{
	options.UseSqlServer(connectionString);
});
using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
	using (var dbContext = scope.ServiceProvider.GetRequiredService<NFCDbContext>())
	{
		if (dbContext.Database.GetPendingMigrations().Any())
		{
			dbContext.Database.Migrate();
		}
	}
}
builder.Services.AddIdentity<NFCUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<NFCDbContext>().AddDefaultTokenProviders();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Repositories
builder.Services.AddTransient<ISensorRepository, SensorRepository>();
builder.Services.AddTransient<IHearingRepository, HearingRepository>();
builder.Services.AddTransient<IKT_MIC_WF_SPLRepository, KT_MIC_WF_SPLRepository>();
builder.Services.AddTransient<IKT_TW_SPLRepository, KT_TW_SPLRepository>();
builder.Services.AddTransient<IProductionLineRepository, ProductionLineRepository>();
builder.Services.AddTransient<IHistoryUploadRepository, HistoryUploadRepository>();
builder.Services.AddTransient<IIdentityRepository, IdentityRepository>();

//Add Services
builder.Services.AddScoped<IMessageUploadService, MessageUploadService>();

//RabbitMQ
builder.Services.Configure<RabbitMQSetting>(builder.Configuration.GetSection("RabbitMq"));
var serviceProvider = builder.Services.BuildServiceProvider();
var rabbitMqSetting = serviceProvider.GetService<IOptions<RabbitMQSetting>>().Value;
var factory = new ConnectionFactory()
{
	Uri = new Uri(rabbitMqSetting.ConnectionString)
};

builder.Services.AddMassTransit(x =>
{
	x.AddConsumer<MessageUploadConsumer>();
	x.UsingRabbitMq((context, cfg) =>
	{
		var uri = rabbitMqSetting.ConnectionString;
		cfg.Host(uri, host =>
		{
			host.Username(rabbitMqSetting.UserName);
			host.Password(rabbitMqSetting.Password);
			host.Heartbeat(60);
		});
		cfg.ReceiveEndpoint(rabbitMqSetting.QueueName, c =>
		{
			c.ConfigureConsumer<MessageUploadConsumer>(context);
		});
		// Register the error queue
		cfg.ReceiveEndpoint(rabbitMqSetting.QueueErrorName, c =>
		{
			c.ConfigureConsumer<MessageUploadConsumer>(context);
		});
		cfg.ConfigureEndpoints(context);
		cfg.AutoStart = true;
		cfg.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
	});
});
//Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
	options.Configuration = builder.Configuration.GetSection("Redis")["ConnectionString"].ToString();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
	app.UseSwagger();
	app.UseSwaggerUI();
//}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
