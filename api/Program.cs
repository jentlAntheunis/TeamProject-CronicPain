using Microsoft.EntityFrameworkCore;

using Pebbles.Context;
using Pebbles.Models;
using Pebbles.Repositories;
using Pebbles.Services;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ISpecialistService, SpecialistService>();
builder.Services.AddScoped<ISpecialistRepository, SpecialistRepository>();

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PebblesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PebblesDB")));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();   

// Initialize Firebase Admin SDK
var serviceAccountPath = "ServiceAccountCredentials.json";

var firebaseCredential = GoogleCredential.FromFile(serviceAccountPath);

FirebaseApp.Create(new AppOptions
{
    Credential = firebaseCredential,
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

app.UseMiddleware<FirebaseTokenValidatorMiddleware>();

app.Run();
