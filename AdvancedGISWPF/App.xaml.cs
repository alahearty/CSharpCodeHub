using System.Windows;
using Serilog;
using System.IO;
using AdvancedGISWPF.Services;

namespace AdvancedGISWPF;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        // Configure Serilog for logging
        ConfigureLogging();
        
        // Initialize database connection
        InitializeDatabase();
        
        base.OnStartup(e);
        
        Log.Information("Advanced GIS WPF Application started successfully");
    }

    private void ConfigureLogging()
    {
        var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "gis-app-.log");
        
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File(logPath, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30)
            .WriteTo.Debug()
            .CreateLogger();
    }

    private void InitializeDatabase()
    {
        try
        {
            var dbService = new DatabaseService();
            dbService.InitializeDatabase();
            Log.Information("Database initialized successfully");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to initialize database");
            MessageBox.Show($"Database initialization failed: {ex.Message}", "Database Error", 
                          MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    protected override void OnExit(ExitEventArgs e)
    {
        Log.Information("Application shutting down");
        Log.CloseAndFlush();
        base.OnExit(e);
    }
}
