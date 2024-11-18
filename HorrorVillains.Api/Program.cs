using HorrorVillains.Api.Features.Greeter.V1;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.OpenApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddOpenApi();
builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo { Title = "HorrorVillains.Api", Version = "v1" });

    var filePath = Path.Combine(System.AppContext.BaseDirectory, "HorrorVillains.Api.xml");
    o.IncludeXmlComments(filePath);
    o.IncludeGrpcXmlComments(filePath, includeControllerXmlComments: true);
});

builder.Services.AddGrpc()
    .AddJsonTranscoding();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapRazorPages();

app.MapOpenApi();
app.UseSwagger(o =>
{
    o.RouteTemplate = "/openapi/{documentName}.json";
});
app.UseSwaggerUI(o =>
{
    o.SwaggerEndpoint("/openapi/v1.json", "HorrorVillains.Api v1");
});

app.MapScalarApiReference();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterFeature>();
app.Run();