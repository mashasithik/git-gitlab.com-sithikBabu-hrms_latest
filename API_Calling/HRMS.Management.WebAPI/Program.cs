using HRMS.Management.CommonLayer;
using Serilog;
using System.Configuration;

// Assing the Connection string
DBContext dBContext = DBContext.Instance;

var configuration = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json")
.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
.Build();
// Configure Serilog early on so that even the bootstrapping phase can be logged
Log.Logger = new LoggerConfiguration()
.ReadFrom.Configuration(configuration)
.CreateLogger();

Environment.SetEnvironmentVariable("BASEDIR", AppContext.BaseDirectory);
Log.Information("Starting web application");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    // Get the connection string from configuration
    // MySQL
    //string connectionString = builder.Configuration.GetConnectionString("HRMSManagementDB") ?? "YourDefaultConnectionString";
    // MSSQL
    string connectionString = builder.Configuration.GetConnectionString("HRMSManagementSQLDB") ?? "Server=LAPTOP-T5JC6V3P/SQLEXPRESS;Database=HRMS_Addeco;Integrated Security=SSPI;";
    dBContext.ConnectionString = connectionString;    

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //CORS 
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
    });

    ////Add support to logging with SERILOG
    //builder.Host.UseSerilog((context, configuration) =>
    //    configuration.ReadFrom.Configuration(context.Configuration));

    //Log.Information("Starting web application");
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseDeveloperExceptionPage();
    }

    app.UseCors();
    
    //Add support to logging request with SERILOG
    //app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthorization();
    app.MapControllers();
#pragma warning disable ASP0014 // Suggest using top level route registrations
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
#pragma warning restore ASP0014 // Suggest using top level route registrations

    app.Run();

    return 0;
}
catch(Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}


