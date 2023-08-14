var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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
