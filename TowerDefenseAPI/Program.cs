using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TowerDefenseAPI;
using TowerDefenseAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(@"Data Source=DESKTOP-RR8JV1G\SQLEXPRESS01; Database = TowerDefense;TrustServerCertificate=True; Integrated Security=SSPI;"));

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.InitializeRepositories();
builder.Services.InitializeServices();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
