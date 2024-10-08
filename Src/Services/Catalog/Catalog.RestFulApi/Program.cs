using Catalog.RestFulApi.Data.Context;
using Catalog.RestFulApi.Data.Presistence.Repository;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1",new OpenApiInfo(){Title = "Catalog.Api",Version = "v1"});
    }
    );

#region Ioc

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICatalogContext, CatalogContext>();

#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
