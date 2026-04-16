using Microsoft.EntityFrameworkCore;
using OrderWebAPI.Models;
using OrderWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers + JSON cycle fix (VERY IMPORTANT)
builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

//  Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//  DbContext (ORDER DB)
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("OrderConnection")
    ));

//  Service Layer DI
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();