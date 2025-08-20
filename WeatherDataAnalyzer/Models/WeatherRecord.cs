namespace WeatherDataAnalyzer.Models;

// Weather data record model
public class WeatherRecord
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; } = string.Empty;
    public double Temperature { get; set; }
    public double Humidity { get; set; }
    public double Pressure { get; set; }
    public double WindSpeed { get; set; }
    public string WindDirection { get; set; } = string.Empty;
    public double Precipitation { get; set; }
    public string WeatherCondition { get; set; } = string.Empty;
    public double Visibility { get; set; }
    public double UVIndex { get; set; }
    public DateTime RecordedAt { get; set; }

    // Calculated properties
    public string TemperatureUnit => "°C";
    public string HumidityUnit => "%";
    public string PressureUnit => "hPa";
    public string WindSpeedUnit => "km/h";
    public string PrecipitationUnit => "mm";
    public string VisibilityUnit => "km";

    public string Season
    {
        get
        {
            var month = Date.Month;
            return month switch
            {
                12 or 1 or 2 => "Winter",
                3 or 4 or 5 => "Spring",
                6 or 7 or 8 => "Summer",
                9 or 10 or 11 => "Autumn",
                _ => "Unknown"
            };
        }
    }

    public string TemperatureDescription
    {
        get
        {
            return Temperature switch
            {
                < -10 => "Very Cold",
                < 0 => "Cold",
                < 10 => "Cool",
                < 20 => "Mild",
                < 25 => "Warm",
                < 30 => "Hot",
                _ => "Very Hot"
            };
        }
    }

    public string HumidityDescription
    {
        get
        {
            return Humidity switch
            {
                < 30 => "Very Dry",
                < 40 => "Dry",
                < 60 => "Normal",
                < 70 => "Humid",
                < 80 => "Very Humid",
                _ => "Extremely Humid"
            };
        }
    }

    public string WindDescription
    {
        get
        {
            return WindSpeed switch
            {
                < 5 => "Calm",
                < 10 => "Light Breeze",
                < 20 => "Gentle Breeze",
                < 30 => "Moderate Breeze",
                < 40 => "Strong Breeze",
                < 50 => "High Wind",
                _ => "Storm"
            };
        }
    }

    public bool IsExtremeTemperature => Temperature < -20 || Temperature > 40;
    public bool IsExtremeHumidity => Humidity < 10 || Humidity > 95;
    public bool IsExtremeWind => WindSpeed > 60;
    public bool IsExtremePrecipitation => Precipitation > 50;

    public WeatherRecord()
    {
        RecordedAt = DateTime.Now;
    }

    public WeatherRecord(int id, DateTime date, string location, double temperature, 
                        double humidity, double pressure, double windSpeed, string windDirection,
                        double precipitation, string weatherCondition, double visibility, double uvIndex)
    {
        Id = id;
        Date = date;
        Location = location;
        Temperature = temperature;
        Humidity = humidity;
        Pressure = pressure;
        WindSpeed = windSpeed;
        WindDirection = windDirection;
        Precipitation = precipitation;
        WeatherCondition = weatherCondition;
        Visibility = visibility;
        UVIndex = uvIndex;
        RecordedAt = DateTime.Now;
    }

    public string GetWeatherSummary()
    {
        var summary = $"{Date:yyyy-MM-dd} at {Location}: ";
        summary += $"{Temperature:F1}{TemperatureUnit}, ";
        summary += $"{Humidity:F0}{HumidityUnit} humidity, ";
        summary += $"{WindSpeed:F1}{WindSpeedUnit} wind";
        
        if (Precipitation > 0)
        {
            summary += $", {Precipitation:F1}{PrecipitationUnit} rain";
        }
        
        return summary;
    }

    public string GetDetailedInfo()
    {
        return $"""
        📅 Date: {Date:yyyy-MM-dd HH:mm}
        📍 Location: {Location}
        🌡️  Temperature: {Temperature:F1}{TemperatureUnit} ({TemperatureDescription})
        💧 Humidity: {Humidity:F0}{HumidityUnit} ({HumidityDescription})
        📊 Pressure: {Pressure:F1}{PressureUnit}
        💨 Wind: {WindSpeed:F1}{WindSpeedUnit} {WindDirection} ({WindDescription})
        🌧️  Precipitation: {Precipitation:F1}{PrecipitationUnit}
        ☁️  Condition: {WeatherCondition}
        👁️  Visibility: {Visibility:F1}{VisibilityUnit}
        ☀️  UV Index: {UVIndex:F1}
        🍂 Season: {Season}
        ⏰ Recorded: {RecordedAt:yyyy-MM-dd HH:mm:ss}
        """;
    }

    public override string ToString()
    {
        var icon = GetWeatherIcon();
        return $"{icon} {Date:MM/dd} | {Location} | {Temperature:F1}°C | {WeatherCondition} | {Humidity:F0}%";
    }

    private string GetWeatherIcon()
    {
        return WeatherCondition.ToLower() switch
        {
            var condition when condition.Contains("sunny") || condition.Contains("clear") => "☀️",
            var condition when condition.Contains("cloudy") || condition.Contains("overcast") => "☁️",
            var condition when condition.Contains("rain") || condition.Contains("drizzle") => "🌧️",
            var condition when condition.Contains("snow") || condition.Contains("blizzard") => "❄️",
            var condition when condition.Contains("storm") || condition.Contains("thunder") => "⛈️",
            var condition when condition.Contains("fog") || condition.Contains("mist") => "🌫️",
            var condition when condition.Contains("wind") || condition.Contains("breeze") => "💨",
            _ => "🌤️"
        };
    }
}
