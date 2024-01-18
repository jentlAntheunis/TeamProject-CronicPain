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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    Formatting = Formatting.Indented,
    ContractResolver = new CamelCasePropertyNamesContractResolver()
};

//add services
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<ISpecialistService, SpecialistService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();

//add repositories
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<ISpecialistRepository, SpecialistRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAvatarRepository, AvatarRepository>();
builder.Services.AddScoped<IColorRepository, ColorRepository>();
builder.Services.AddScoped<IMovementSessionRepository, MovementSessionRepository>();
builder.Services.AddScoped<IMovementSuggestionRepository, MovementSuggestionRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IQuestionnaireRepository, QuestionnaireRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IScaleRepository, ScaleRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    // Define the FirebaseAuthentication security scheme
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});


builder.Services.AddDbContext<PebblesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PebblesDB")));



// Configure Firebase Authentication (right error codes)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("FirebaseAuthentication", options =>
    {
        var projectId = "pebbles-294c6";
        options.Authority = $"https://securetoken.google.com/{projectId}";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = $"https://securetoken.google.com/{projectId}",
            ValidateAudience = true,
            ValidAudience = projectId,
            ValidateLifetime = true
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                // Your Firebase authentication logic here
                // You can use FirebaseAuthenticationHandler methods directly
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:5173", "https://www.pebbles-health.be", "https://staging.pebbles-health.be")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
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

app.UseCors("CorsPolicy");

app.UseMiddleware<FirebaseTokenValidatorMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
