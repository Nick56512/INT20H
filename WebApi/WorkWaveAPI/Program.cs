using DAL;
using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using WorkWaveAPI.ApiConfig;

namespace WorkWaveAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddScoped<IRepository<ProjectCategory>, ProjectCategoryRepository>();
            builder.Services.AddScoped<ProjectCategoryRepository, ProjectCategoryRepository>();

            builder.Services.AddScoped<IRepository<PortfolioProject>, PortfolioRepository>();
            builder.Services.AddScoped<PortfolioRepository, PortfolioRepository>();

            builder.Services.AddScoped<IRepository<Achievment>, AchievmentRepository>();
            builder.Services.AddScoped<AchievmentRepository, AchievmentRepository>();

            builder.Services.AddScoped<IRepository<Chat>, ChatRepository>();
            builder.Services.AddScoped<ChatRepository, ChatRepository>();

            builder.Services.AddScoped<IRepository<Member>, MemberRepository>();
            builder.Services.AddScoped<MemberRepository, MemberRepository>();

            builder.Services.AddScoped<IRepository<Message>, MessageRepository>();
            builder.Services.AddScoped<MessageRepository, MessageRepository>();

            builder.Services.AddScoped<IRepository<Project>, ProjectRepository>();
            builder.Services.AddScoped<ProjectRepository, ProjectRepository>();

            builder.Services.AddScoped<IRepository<Team>, TeamRepository>();
            builder.Services.AddScoped<TeamRepository, TeamRepository>();


            var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 9;
                options.Password.RequiredUniqueChars = 1;

                options.User.AllowedUserNameCharacters = "";
                options.User.RequireUniqueEmail = true;
            });
            builder.Services.AddIdentity<User, IdentityRole>()
                 .AddEntityFrameworkStores<ApplicationDbContext>()
                 .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymetricKey(),
                    ValidateIssuerSigningKey = true
                };
            });

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors((opt) =>
            {
                opt.AllowAnyOrigin();
                opt.AllowAnyMethod();
                opt.AllowAnyHeader();
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}