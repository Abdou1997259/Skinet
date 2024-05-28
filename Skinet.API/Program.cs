using Microsoft.EntityFrameworkCore;
using Skinet.API.Extentions;
using Infrastructure.Data;
using Skinet.API.Extentions;
 using Core.Interfaces;
using Skinet.API.Helpers;
using Skinet.API.MiddleWare;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Skinet.API.Errors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Skinet.API.Authentication;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers(options =>
{
    options.Filters.Add<PermissionBasedAuthorizationFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<StoreContext>(p=>p.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddApplicaitonServices();
builder.Services.AddSwaggerDocumention();
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});
var jwt=builder.Configuration.GetSection("Jwt").Get<Jwt>();
builder.Services.AddSingleton(jwt);
builder.Services.AddAuthentication()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwt.Issuer,
        ValidateAudience = true,
        ValidAudience = jwt.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key))
    };


});
builder.Services.AddSingleton<IAuthorizationHandler, AgeAuthrizationHandler>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MoreThan25", builder=>builder.AddRequirements(new AgeGreaterThan25Requirement()));
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services=scope.ServiceProvider;
    var loggerFacotry=services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<StoreContext>();
        await context.Database.MigrateAsync();
        await StoreContextSeed.SeedAsync(context, loggerFacotry);
    }
    catch (Exception ex)
    {
        var logger = loggerFacotry.CreateLogger<Program>();
        logger.LogError(ex, "An error occured during migration");

    }

}

app.UseMiddleware<ExceptionMiddleWare>();
// Configure the HTTP request pipeline.
app.UseSwaggerDocumention();
app.UseStatusCodePagesWithReExecute("/error/{0}");

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
