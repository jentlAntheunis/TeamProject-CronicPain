using Microsoft.EntityFrameworkCore;

using Pebbles.Context;
using Pebbles.Models;
using Pebbles.Repositories;
using Pebbles.Services;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

//add services
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<ISpecialistService, SpecialistService>();
builder.Services.AddScoped<IUserService, UserService>();

//add repositories
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<ISpecialistRepository, SpecialistRepository>();
builder.Services.AddScoped<IColorRepository, ColorRepository>();
builder.Services.AddScoped<IAvatarRepository, AvatarRepository>();
builder.Services.AddScoped<IPatientSpecialistRepository, PatientSpecialistRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PebblesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PebblesDB")));


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
