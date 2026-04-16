using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Models;
using ProductWebAPI.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();

builder.Services.AddDbContext<ProductContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductCon")));
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IBrandService, BrandService>();
builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<IBrandService, BrandService>();
//builder.Services.AddSingleton<IBrandService, BrandService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
