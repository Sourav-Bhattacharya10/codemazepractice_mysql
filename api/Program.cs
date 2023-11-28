using Azure.Identity;
using codemazepractice.persistence;
using codemazepractice.application;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;

builder.Services.AddAzureClients(options =>
{
    options.AddBlobServiceClient(new Uri($"https://{configuration["AzureStorageAccountName"]}.blob.core.windows.net"));
    options.UseCredential(new DefaultAzureCredential());
});

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddMemoryCache();

builder.Services.AddPersistenceLayer(builder.Configuration);

builder.Services.AddApplicationLayer();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
