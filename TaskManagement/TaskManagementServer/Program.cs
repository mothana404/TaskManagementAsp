using Microsoft.AspNetCore.Authentication.JwtBearer;
using TaskManagementServer.Services.IService;
using TaskManagementServer.Services.Auth;
using Microsoft.IdentityModel.Tokens;
using TaskManagementServer.Services;
using TaskManagementServer.Enums;
using TaskManagementServer.Data;
using System.Security.Claims;
using System.Text;

namespace TaskManagementServer;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<ApplicationDbContext>();
        builder.Services.AddScoped<AuthService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ITeamService, TeamService>();
        builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
        builder.Services.AddScoped<ITaskService, TaskService>();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            var config = builder.Configuration.GetSection("Jwt");
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["Issuer"],
                ValidAudience = config["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Key"]!)),
                RoleClaimType = ClaimTypes.Role
            };
        });

        builder.Services.AddAuthorizationBuilder()
            .AddPolicy("Admin", policy =>
                policy.RequireRole(UserRoles.Admin.ToString()))
            .AddPolicy("Manager", policy =>
                policy.RequireRole(
                    UserRoles.Admin.ToString(),
                    UserRoles.Manager.ToString()))
            .AddPolicy("User", policy =>
                policy.RequireRole(
                    UserRoles.Admin.ToString(),
                    UserRoles.Manager.ToString(),
                    UserRoles.Member.ToString()));

        var app = builder.Build();

        app.UseCors();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
