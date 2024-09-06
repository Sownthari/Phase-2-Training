using DessertAPIRepo.Interface;
using DessertAPIRepo.Models;
using DessertAPIRepo.Repository;
using DessertAPIRepo.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DessertContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DessertDB")));
builder.Services.AddScoped<IDessert, DessertRepository>();
builder.Services.AddScoped<IFlavour, FlavourRepository>();
builder.Services.AddScoped<DessertService>();
builder.Services.AddScoped<FlavourService>();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

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
