using Microsoft.EntityFrameworkCore;
using CretaceousParkApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Specify another port or url: builder.WebHost.UseUrls("http://*:8080")
// Alternative: specify in launchSettings.json
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApiContext>(
    dbContextOptions => dbContextOptions.UseMySql(
        builder.Configuration["ConnectionStrings:DefaultConnection"],
        ServerVersion.AutoDetect(builder.Configuration["ConnectionStrings:DefaultConnection"])
    )
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    /* http://localhost:<port>/swagger/v1/swagger.json */
}
else
{
    /* Development: redirect https can cause problems for Postman */
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
