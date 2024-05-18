using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Interfaces;
using Skinet.API.Helpers;
using Skinet.API.MiddleWare;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Skinet.API.Errors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreContext>(p=>p.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var error = actionContext.ModelState
        .Where(e => e.Value.Errors.Count > 0)
        .SelectMany(x => x.Value.Errors)
        .Select(x => x.ErrorMessage).ToArray();
        var errorResponse = new ApiValidationErrorReponse
        {
            Errors=error
        };
        return new BadRequestObjectResult(errorResponse);
      };


});
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GeneritcRespostory<>));  
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
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
app.UseStatusCodePagesWithReExecute("/error/{0}");

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
