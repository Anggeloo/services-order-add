using Microsoft.EntityFrameworkCore;
using services_order_add.Database;
using services_order_add.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://*:5000");

var connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING");

if (string.IsNullOrEmpty(connectionString))
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
}

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("MYSQL_CONNECTION_STRING environment variable and DefaultConnection in appsettings.json are both missing.");
}

builder.Services.AddDbContext<DBContext>(options =>
    options.UseMySQL(connectionString));

builder.Services.AddScoped<OrderServices>();  // Registrar el servicio

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();


app.Run();
