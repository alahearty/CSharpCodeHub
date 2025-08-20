using Microsoft.EntityFrameworkCore;
using Npgsql;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using AdvancedGISWPF.Models;
using Serilog;
using Newtonsoft.Json;

namespace AdvancedGISWPF.Services;

// Database service for PostgreSQL/PostGIS operations
public class DatabaseService : IDisposable
{
    private readonly string _connectionString;
    private readonly GISDbContext _context;
    private bool _disposed = false;
    
    public bool IsConnected => _context?.Database.CanConnect() ?? false;
    
    public DatabaseService()
    {
        // Connection string - update with your PostgreSQL credentials
        _connectionString = "Host=localhost;Database=gis_database;Username=gis_user;Password=gis_password;Port=5432";
        
        try
        {
            // Configure Npgsql for PostGIS
            NpgsqlConnection.GlobalTypeMapper.UseNetTopologySuite();
            
            // Create DbContext
            var optionsBuilder = new DbContextOptionsBuilder<GISDbContext>();
            optionsBuilder.UseNpgsql(_connectionString,
                options => options.UseNetTopologySuite());
            _context = new GISDbContext(optionsBuilder.Options);
            
            Log.Information("DatabaseService initialized");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to initialize DatabaseService");
            throw;
        }
    }
    
    public async Task InitializeDatabaseAsync()
    {
        try
        {
            // Ensure database is created
            await _context.Database.EnsureCreatedAsync();
            
            // Check PostGIS extension
            await EnsurePostGISExtensionAsync();
            
            // Create sample data if database is empty
            if (!await _context.Layers.AnyAsync())
            {
                await CreateSampleDataAsync();
            }
            
            Log.Information("Database initialized successfully");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to initialize database");
            throw;
        }
    }
    
    private async Task EnsurePostGISExtensionAsync()
    {
        try
        {
            var sql = "CREATE EXTENSION IF NOT EXISTS postgis;";
            await _context.Database.ExecuteSqlRawAsync(sql);
            Log.Information("PostGIS extension ensured");
        }
        catch (Exception ex)
        {
            Log.Warning(ex, "PostGIS extension may already exist");
        }
    }
    
    private async Task CreateSampleDataAsync()
    {
        try
        {
            // Create sample layers
            var citiesLayer = new Layer
            {
                Name = "Cities",
                Description = "Major cities around the world",
                LayerType = "Point",
                IsVisible = true,
                Order = 1
            };
            
            var countriesLayer = new Layer
            {
                Name = "Countries",
                Description = "Country boundaries",
                LayerType = "Polygon",
                IsVisible = true,
                Order = 2
            };
            
            var riversLayer = new Layer
            {
                Name = "Rivers",
                Description = "Major rivers",
                LayerType = "LineString",
                IsVisible = true,
                Order = 3
            };
            
            _context.Layers.AddRange(citiesLayer, countriesLayer, riversLayer);
            await _context.SaveChangesAsync();
            
            // Create sample features
            await CreateSampleFeaturesAsync();
            
            Log.Information("Sample data created successfully");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to create sample data");
        }
    }
    
    private async Task CreateSampleFeaturesAsync()
    {
        try
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            
            // Sample cities (points)
            var cities = new[]
            {
                new SpatialFeature("New York", "City", geometryFactory.CreatePoint(new Coordinate(-74.006, 40.7128))),
                new SpatialFeature("London", "City", geometryFactory.CreatePoint(new Coordinate(-0.1276, 51.5074))),
                new SpatialFeature("Tokyo", "City", geometryFactory.CreatePoint(new Coordinate(139.6917, 35.6895))),
                new SpatialFeature("Sydney", "City", geometryFactory.CreatePoint(new Coordinate(151.2093, -33.8688))),
                new SpatialFeature("Cairo", "City", geometryFactory.CreatePoint(new Coordinate(31.2357, 30.0444)))
            };
            
            // Sample countries (polygons) - simplified
            var countries = new[]
            {
                CreateSimplifiedCountry("United States", new[] { new Coordinate(-125, 25), new Coordinate(-66, 25), new Coordinate(-66, 50), new Coordinate(-125, 50), new Coordinate(-125, 25) }),
                CreateSimplifiedCountry("United Kingdom", new[] { new Coordinate(-8, 50), new Coordinate(2, 50), new Coordinate(2, 60), new Coordinate(-8, 60), new Coordinate(-8, 50) }),
                CreateSimplifiedCountry("Japan", new[] { new Coordinate(130, 30), new Coordinate(145, 30), new Coordinate(145, 45), new Coordinate(130, 45), new Coordinate(130, 30) })
            };
            
            // Sample rivers (linestrings)
            var rivers = new[]
            {
                new SpatialFeature("Mississippi River", "River", geometryFactory.CreateLineString(new[] { new Coordinate(-90.1, 29.9), new Coordinate(-87.6, 30.7), new Coordinate(-85.0, 32.3) })),
                new SpatialFeature("Thames River", "River", geometryFactory.CreateLineString(new[] { new Coordinate(-0.5, 51.5), new Coordinate(-0.1, 51.5), new Coordinate(0.5, 51.5) })),
                new SpatialFeature("Nile River", "River", geometryFactory.CreateLineString(new[] { new Coordinate(31.2, 30.0), new Coordinate(32.9, 31.2), new Coordinate(34.8, 32.5) }))
            };
            
            // Assign to layers
            var citiesLayer = await _context.Layers.FirstAsync(l => l.Name == "Cities");
            var countriesLayer = await _context.Layers.FirstAsync(l => l.Name == "Countries");
            var riversLayer = await _context.Layers.FirstAsync(l => l.Name == "Rivers");
            
            foreach (var city in cities)
            {
                city.LayerId = citiesLayer.Id;
                city.Category = "Urban";
                city.Attributes = JsonConvert.SerializeObject(new { Population = "8.4M", Country = "USA" });
            }
            
            foreach (var country in countries)
            {
                country.LayerId = countriesLayer.Id;
                country.Category = "Administrative";
                country.Attributes = JsonConvert.SerializeObject(new { Population = "67M", Area = "242,495 km²" });
            }
            
            foreach (var river in rivers)
            {
                river.LayerId = riversLayer.Id;
                river.Category = "Hydrography";
                river.Attributes = JsonConvert.SerializeObject(new { Length = "6,275 km", Discharge = "16,800 m³/s" });
            }
            
            _context.SpatialFeatures.AddRange(cities);
            _context.SpatialFeatures.AddRange(countries);
            _context.SpatialFeatures.AddRange(rivers);
            
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to create sample features");
        }
    }
    
    private SpatialFeature CreateSimplifiedCountry(string name, Coordinate[] coordinates)
    {
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
        var linearRing = geometryFactory.CreateLinearRing(coordinates);
        var polygon = geometryFactory.CreatePolygon(linearRing);
        return new SpatialFeature(name, "Country", polygon);
    }
    
    // Spatial queries
    public async Task<List<SpatialFeature>> GetFeaturesInBoundingBoxAsync(double minX, double minY, double maxX, double maxY)
    {
        try
        {
            var features = await _context.SpatialFeatures
                .Where(f => f.MinX <= maxX && f.MaxX >= minX && f.MinY <= maxY && f.MaxY >= minY)
                .Include(f => f.Layer)
                .ToListAsync();
            
            Log.Information($"Retrieved {features.Count} features in bounding box");
            return features;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to retrieve features in bounding box");
            return new List<SpatialFeature>();
        }
    }
    
    public async Task<List<SpatialFeature>> GetFeaturesByTypeAsync(string featureType)
    {
        try
        {
            var features = await _context.SpatialFeatures
                .Where(f => f.FeatureType == featureType)
                .Include(f => f.Layer)
                .ToListAsync();
            
            Log.Information($"Retrieved {features.Count} features of type {featureType}");
            return features;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to retrieve features by type");
            return new List<SpatialFeature>();
        }
    }
    
    public async Task<List<SpatialFeature>> GetFeaturesByLayerAsync(int layerId)
    {
        try
        {
            var features = await _context.SpatialFeatures
                .Where(f => f.LayerId == layerId)
                .Include(f => f.Layer)
                .ToListAsync();
            
            Log.Information($"Retrieved {features.Count} features from layer {layerId}");
            return features;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to retrieve features by layer");
            return new List<SpatialFeature>();
        }
    }
    
    // Spatial analysis
    public async Task<double> CalculateDistanceAsync(int feature1Id, int feature2Id)
    {
        try
        {
            var feature1 = await _context.SpatialFeatures.FindAsync(feature1Id);
            var feature2 = await _context.SpatialFeatures.FindAsync(feature2Id);
            
            if (feature1?.Geometry != null && feature2?.Geometry != null)
            {
                return feature1.DistanceTo(feature2.Geometry);
            }
            
            return double.MaxValue;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to calculate distance between features");
            return double.MaxValue;
        }
    }
    
    public async Task<Geometry?> CreateBufferAsync(int featureId, double distance)
    {
        try
        {
            var feature = await _context.SpatialFeatures.FindAsync(featureId);
            return feature?.Buffer(distance);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to create buffer for feature");
            return null;
        }
    }
    
    // Layer operations
    public async Task<List<Layer>> GetAllLayersAsync()
    {
        try
        {
            var layers = await _context.Layers
                .OrderBy(l => l.Order)
                .ToListAsync();
            
            Log.Information($"Retrieved {layers.Count} layers");
            return layers;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to retrieve layers");
            return new List<Layer>();
        }
    }
    
    public async Task<Layer?> GetLayerByIdAsync(int layerId)
    {
        try
        {
            return await _context.Layers.FindAsync(layerId);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to retrieve layer by ID");
            return null;
        }
    }
    
    // Statistics
    public async Task<Dictionary<string, object>> GetDatabaseStatisticsAsync()
    {
        try
        {
            var stats = new Dictionary<string, object>();
            stats["TotalLayers"] = await _context.Layers.CountAsync();
            stats["TotalFeatures"] = await _context.SpatialFeatures.CountAsync();
            stats["FeatureTypes"] = await _context.SpatialFeatures
                .GroupBy(f => f.FeatureType)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .ToListAsync();
            stats["GeometryTypes"] = await _context.SpatialFeatures
                .Where(f => f.HasGeometry)
                .GroupBy(f => f.GeometryType)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .ToListAsync();
            
            Log.Information("Database statistics retrieved successfully");
            return stats;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to retrieve database statistics");
            return new Dictionary<string, object>();
        }
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context?.Dispose();
            _disposed = true;
        }
    }
}
