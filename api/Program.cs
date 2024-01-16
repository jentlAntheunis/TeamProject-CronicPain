using Microsoft.EntityFrameworkCore;

using Pebbles.Context;
using Pebbles.Models;
using Pebbles.Repositories;
using Pebbles.Services;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ISpecialistService, SpecialistService>();
builder.Services.AddScoped<ISpecialistRepository, SpecialistRepository>();

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Define the FirebaseAuthentication security scheme
    var firebaseSecurityScheme = new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        Name = "Authorization",
        In = ParameterLocation.Header,
        Description = "Bearer {your_access_token}",
        Scheme = "Bearer"
    };

    // Define the FirebaseAuthentication security requirement
    var firebaseSecurityRequirement = new OpenApiSecurityRequirement
    {
        {
            firebaseSecurityScheme, new string[] {}
        },
    };

    // Add the security definition and requirement to Swagger
    c.AddSecurityDefinition("FirebaseAuthentication", firebaseSecurityScheme);
    c.AddSecurityRequirement(firebaseSecurityRequirement);
});


builder.Services.AddDbContext<PebblesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PebblesDB")));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();   


// Configure Firebase Authentication (right error codes)
builder.Services.AddAuthentication("FirebaseAuthentication") // Use a custom authentication scheme name
    .AddJwtBearer("FirebaseAuthentication", options =>
    {
        options.Authority = "https://pebbles-294c6.firebaseapp.com";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://pebbles-294c6.firebaseapp.com",
            // Add other validation parameters as needed
        };
    });


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
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
        // Enable the "Authorize" button
        c.OAuthClientId("swagger-ui");
        c.OAuthAppName("Swagger UI");
    });
}

app.UseHttpsRedirection();

app.UseMiddleware<FirebaseTokenValidatorMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
