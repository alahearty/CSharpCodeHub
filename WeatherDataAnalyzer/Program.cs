using WeatherDataAnalyzer.Core;
using WeatherDataAnalyzer.Services;
using WeatherDataAnalyzer.Models;

namespace WeatherDataAnalyzer;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("🌤️  Weather Data Analyzer System");
        Console.WriteLine("==================================\n");

        var weatherService = new WeatherService();
        var dataAnalyzer = new WeatherDataAnalyzer();
        var forecastService = new ForecastService();
        var reportService = new WeatherReportService();
        
        bool isRunning = true;
        
        while (isRunning)
        {
            ShowMainMenu();
            var choice = Console.ReadLine()?.Trim();
            
            switch (choice)
            {
                case "1":
                    weatherService.AddWeatherData();
                    break;
                case "2":
                    weatherService.ViewWeatherData();
                    break;
                case "3":
                    weatherService.ListAllData();
                    break;
                case "4":
                    dataAnalyzer.AnalyzeTemperatureTrends();
                    break;
                case "5":
                    dataAnalyzer.AnalyzePrecipitationPatterns();
                    break;
                case "6":
                    dataAnalyzer.FindExtremeWeather();
                    break;
                case "7":
                    dataAnalyzer.CalculateSeasonalAverages();
                    break;
                case "8":
                    forecastService.GenerateForecast();
                    break;
                case "9":
                    reportService.GenerateWeatherReport();
                    break;
                case "10":
                    reportService.GenerateClimateSummary();
                    break;
                case "0":
                    isRunning = false;
                    Console.WriteLine("👋 Goodbye!");
                    break;
                default:
                    Console.WriteLine("❌ Invalid choice. Please try again.");
                    break;
            }
            
            if (isRunning)
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    static void ShowMainMenu()
    {
        Console.WriteLine("🌤️  WEATHER DATA ANALYZER");
        Console.WriteLine("===========================");
        Console.WriteLine("1. ➕ Add Weather Data");
        Console.WriteLine("2. 👁️  View Weather Data");
        Console.WriteLine("3. 📋 List All Data");
        Console.WriteLine("4. 📈 Temperature Trends");
        Console.WriteLine("5. 🌧️  Precipitation Patterns");
        Console.WriteLine("6. ⚡ Extreme Weather");
        Console.WriteLine("7. 🍂 Seasonal Averages");
        Console.WriteLine("8. 🔮 Generate Forecast");
        Console.WriteLine("9. 📊 Weather Report");
        Console.WriteLine("10. 🌍 Climate Summary");
        Console.WriteLine("0. 🚪 Exit");
        Console.WriteLine("===========================");
        Console.Write("Choose option (0-10): ");
    }
}
