using Fiorella.Data.Context;
using Fiorella.Repository.Implementation;
using Fiorella.Repository.Interface;
using Fiorella.UnitofWork;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Fiorella.Dto;
using Fiorella.Validation;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
       System.IO.Path.Combine("C:\\Users\\HP\\Desktop\\Back-End\\ASP.NET CORELogFiles", "Application", "diagnostics.txt"),
       rollingInterval: RollingInterval.Day,
       fileSizeLimitBytes: 10 * 1024 * 1024,
       retainedFileCountLimit: 2,
       rollOnFileSizeLimit: true,
       shared: true,
       flushToDiskInterval: TimeSpan.FromSeconds(1))
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddHttpLogging(c =>
    {
        c.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponseStatusCode
        | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponseHeaders
        | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponseBody;
        // ------------------------------------------Here we show our result directly on console--------------------------

        c.ResponseHeaders.Add("CustomerGUID");
    });

    builder.Host.UseSerilog(); // <-- Add this line

    builder.Services.AddControllers().AddFluentValidation();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));
    
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("mycors", 
            builder => builder.WithOrigins("http://localhost:3000").WithMethods("PUT", "DELETE", "GET"));
    });

    builder.Services.AddScoped<IPictureRepository, PictureRepository>();
    builder.Services.AddTransient(typeof(IRepository<,>), typeof(EfRepository<,>));
    builder.Services.AddTransient<IUnitofWork, UnitofWork>();
    builder.Services.AddScoped<IValidator<PictureDto>, PictureValidator>();

    var app = builder.Build();

    
    app.UseCors(builder => builder
    .WithOrigins("http://localhost:3000")
    .AllowAnyMethod()
    .AllowCredentials()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true));

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.Use(async (context, next) =>
    {
        context.Response.OnStarting(() =>
        {
            context.Response.Headers["CustomerGUID"] = "123456";
            return Task.CompletedTask;
        });
        await next();
    });

    app.UseHttpLogging();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

