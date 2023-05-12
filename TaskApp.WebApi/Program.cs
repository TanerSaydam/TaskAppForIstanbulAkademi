using Microsoft.EntityFrameworkCore;
using TaskApp.WebApi.Context;

var builder = WebApplication.CreateBuilder(args);

#region DbContext
string connectionString = builder.Configuration.GetConnectionString("MySql");

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(connectionString));
#endregion

#region Presentation
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
