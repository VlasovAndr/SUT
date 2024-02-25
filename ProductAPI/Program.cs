using Microsoft.EntityFrameworkCore;
using ProductAPI;
using ProductAPI.Data;
using ProductAPI.Repository;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString") ?? "Data Source=Product.db";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    c.SchemaFilter<EnumSchemaFilter>();
});
builder.Services.AddDbContext<ProductDbContext>(x => x.UseSqlite(connectionString));
builder.Services.AddTransient<IProductRepository, ProductRepository>();

var app = builder.Build();
// var a = app.Services.GetRequiredService<ProductDbContext>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
	// a.Database.EnsureCreated();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
