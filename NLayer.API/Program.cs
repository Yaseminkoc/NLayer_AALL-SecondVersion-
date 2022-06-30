using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLayer.DataAccess.Concrete.EntityFramework;
using System.Text;
using Microsoft.Identity.Client;
using Microsoft.OpenApi.Models;
using NLayer.Bussiness.Abstract;
using NLayer.Bussiness.Concrete;
using NLayer.DataAccess.Abstract;
using NLog;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(),"/nlog.config"));
builder.Services.AddSingleton<ILoggerService, LoggerService>();


var _loginOrigin = "_localorigin";
builder.Services.AddDbContext<EfContext>(options =>
    options.UseSqlServer("Data Source=LAPTOP-LPHCITMN\\SQLEXPRESS;Initial Catalog=SchoolDatabase;Encrypt=False;Connect Timeout=30;Trusted_Connection=True")
    );

     builder.Services.AddControllers().AddNewtonsoftJson();
     builder.Services.AddEndpointsApiExplorer();
     builder.Services.AddSwaggerGen();
     builder.Services.AddAuthentication(x =>
     {
         x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
         x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
     }).AddJwtBearer(options =>
     {
         var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value);
         var issuer = "TestIssuer";
         var audience = "TestAudience";
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuerSigningKey = true,
             IssuerSigningKey = new SymmetricSecurityKey(key),
             ValidateIssuer = true,
             ValidateAudience = true,
             RequireExpirationTime = true,
             ValidIssuer = issuer,
             ValidAudience = audience
         };
     });


     builder.Services.AddCors(opt =>
     {
         opt.AddPolicy(_loginOrigin, builder =>
         {
             builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
         });
     });
     builder.Services.AddTransient<IUserDal, EfUserDal>();
     builder.Services.AddTransient<IUserService, UserManager>();
     builder.Services.AddTransient<IRoleDal, EfRoleDal>();
     builder.Services.AddTransient<IRoleService, RoleManager>();
     builder.Services.AddTransient<IUserRolesDal, EfUserRolesDal>();
     var app = builder.Build();

     if (app.Environment.IsDevelopment())
     {
         app.UseSwagger();
         app.UseSwaggerUI();
     }
   
    app.UseCors(_loginOrigin);
     app.UseAuthentication();
     app.UseAuthorization();

     app.MapControllers();

     app.Run();

