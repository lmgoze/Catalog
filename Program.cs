

using System.Net.Mime;
using System.Text.Json;
using Catalog.Repositories;
using Catalog.Settings;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    
    return new MongoClient(mongoDbSettings.ConnectionString);
});

builder.Services.AddSingleton<IItemsRepository,MongoDbItemsRepository>();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddMongoDb(
        mongoDbSettings.ConnectionString, 
        name: "mongodb", 
        timeout: TimeSpan.FromSeconds(3),
        tags: new[]{"ready"}
        );

var app = builder.Build();

//ready to see if service is ready to serve request 
app.MapHealthChecks("/health/ready", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions{
    Predicate = (check) => check.Tags.Contains("ready"),
    ResponseWriter = async(context, report) => 
    {
       var result = JsonSerializer.Serialize(
         new {
            status = report.Status.ToString(),
            checks = report.Entries.Select(entry => new {
                name = entry.Key,
                status = entry.Value.Status.ToString(),
                exception = entry.Value.Exception != null ? entry.Value.Exception.Message : "none",
                duration = entry.Value.Duration.ToString()
            })
         }   
       );
       context.Response.ContentType = MediaTypeNames.Application.Json;
       await context.Response.WriteAsync(result);
    }
});


// life to check if service is up and running
app.MapHealthChecks("/health/live", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions{
    Predicate = (_) => false
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Run inside the docker container the ASP net environment switches from development to production.
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

