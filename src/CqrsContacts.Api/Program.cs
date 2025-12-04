namespace CqrsContacts.Api;

using CqrsContacts.Api.Middleware;
using CqrsContacts.Domain.Common.Interfaces;
using CqrsContacts.Domain.Common.Models;
using CqrsContacts.Domain.Common.Options;
using CqrsContacts.Domain.Common.PipelineBehaviors;
using CqrsContacts.Domain.Common.Repositories;
using FluentValidation;
using Microsoft.Extensions.Options;
using Mongo2Go;
using MongoDB.Driver;

public class Program
{
    private static MongoDbRunner _inMemoryMongoDB = MongoDbRunner.StartForDebugging();
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
        builder.Services.Configure<ContactDatabaseOptions>(builder.Configuration.GetSection("ContactDatabase"));
        builder.Services.AddSingleton<IMongoClient>(x =>
        {                
            var options = x.GetService<IOptions<ContactDatabaseOptions>>();
            //return new MongoClient(options?.Value.ConnectionString); ;
            return new MongoClient(_inMemoryMongoDB.ConnectionString); 
        });

        builder.Services.AddValidatorsFromAssembly(typeof(BaseDocumentModel).Assembly);

        builder.Services.AddMediatR(cfg =>
        {
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.RegisterServicesFromAssembly(typeof(BaseDocumentModel).Assembly);
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseMiddleware<ValidationExceptionMiddleware>();

        app.Run();
    }
}