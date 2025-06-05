using FluentValidation.AspNetCore;
using FluentValidation;
using Mapster;
using MovieStoreB.BL;
using MovieStoreB.DL;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using MovieStoreB.Controllers;
using MovieStoreB.HealthChecks;
using MovieStoreB.ServiceExtensions;
using MovieStoreB.Services.Kafka;
using MovieStoreB.Services.External;
using Confluent.Kafka;
using MovieStoreB.DL.Interfaces;
using MovieStoreB.DL.Repositories.MongoRepositories;
using MovieStoreB.Validators;

var builder = WebApplication.CreateBuilder(args);


var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

builder.Logging.AddSerilog(logger);


builder.Services
    .AddConfigurations(builder.Configuration)
    .AddDataDependencies()
    .AddBusinessDependencies()
    .AddBackgroundServices();

builder.Services.AddMapster();
builder.Services.AddValidatorsFromAssemblyContaining<TestRequest>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddCheck<SampleHealthCheck>("Sample");
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<ActorService>();
builder.Services.AddHostedService<KafkaActorPublisherService>();
builder.Services.AddHostedService<KafkaActorConsumerService>();
builder.Services.AddHostedService<KafkaCachePublisherService>();
builder.Services.AddHostedService<KafkaCacheConsumerService>();
builder.Services.AddScoped<IMovieRepository, MoviesRepository>();
builder.Services.AddScoped<MovieService>();
builder.Services
    .AddValidatorsFromAssemblyContaining<AddMovieRequestValidator>();
builder.Services.AddFluentValidationAutoValidation();



var kafkaBootstrapServers = builder.Configuration.GetValue<string>("Kafka:BootstrapServers") ?? "localhost:9092";


builder.Services.AddSingleton(new ProducerConfig
{
    BootstrapServers = kafkaBootstrapServers
});

builder.Services.AddSingleton<IProducer<Null, string>>(sp =>
{
    var config = sp.GetRequiredService<ProducerConfig>();
    return new ProducerBuilder<Null, string>(config).Build();
});

builder.Services.AddSingleton(new ConsumerConfig
{
    BootstrapServers = kafkaBootstrapServers,
    GroupId = "movie-cache-consumer-group",
    AutoOffsetReset = AutoOffsetReset.Earliest
});


builder.Services.AddHostedService<KafkaCacheConsumerService>();

builder.Services.AddHttpClient<ExternalMovieService>();




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/healthz");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
