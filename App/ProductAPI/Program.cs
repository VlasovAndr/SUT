using Microsoft.EntityFrameworkCore;
using ProductAPI;
using ProductAPI.Data;
using ProductAPI.Repository;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=Product.db";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<EnumSchemaFilter>();
});
builder.Services.AddDbContext<ProductDbContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddTransient<IProductRepository, ProductRepository>();

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

using (var scope = app.Services.CreateScope())
{
    var container = scope.ServiceProvider;
    var db = container.GetRequiredService<ProductDbContext>();

    db.Database.EnsureCreated();

    if (!db.Products.Any())
    {
        try
        {
            db.Seed();
        }
        catch (Exception ex)
        {
            var logger = container.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred seeding the database. Error: {Message}", ex.Message);
        }
    }
}

app.Run();

public partial class Program { }