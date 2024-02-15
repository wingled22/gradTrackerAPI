using gradTrackerAPI.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

//inject DB EF
builder.Services.AddDbContext<GradTrackerContext>(options =>
    options.UseSqlServer("Server=localhost;Database=GradTracker;User ID=SA;Password=VeryStr0ngP@ssw0rd;TrustServerCertificate=true;"));


builder.Services.AddControllers().AddJsonOptions(
    options => {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    }
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors();

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
