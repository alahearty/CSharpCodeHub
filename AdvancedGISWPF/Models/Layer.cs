using System.ComponentModel.DataAnnotations;

namespace AdvancedGISWPF.Models;

// Layer model for GIS layers
public class Layer
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string LayerType { get; set; } = string.Empty;
    
    [MaxLength(100)]
    public string Category { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string Subcategory { get; set; } = string.Empty;
    
    // Layer properties
    public bool IsVisible { get; set; } = true;
    public bool IsEditable { get; set; } = false;
    public bool IsSelectable { get; set; } = true;
    public int Order { get; set; } = 0;
    
    // Style properties
    public string? FillColor { get; set; }
    public string? StrokeColor { get; set; }
    public double? StrokeWidth { get; set; }
    public double? Opacity { get; set; }
    public string? SymbolType { get; set; }
    public double? SymbolSize { get; set; }
    
    // Data source properties
    public string? DataSource { get; set; }
    public string? ConnectionString { get; set; }
    public string? Query { get; set; }
    
    // Metadata
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string ModifiedBy { get; set; } = string.Empty;
    
    // Spatial extent
    public double? MinX { get; set; }
    public double? MinY { get; set; }
    public double? MaxX { get; set; }
    public double? MaxY { get; set; }
    
    // Coordinate system
    public int SRID { get; set; } = 4326; // WGS84 by default
    public string? CoordinateSystem { get; set; }
    
    // Statistics
    public int FeatureCount { get; set; } = 0;
    public string? LastUpdated { get; set; }
    
    // Navigation properties
    public virtual ICollection<SpatialFeature> SpatialFeatures { get; set; } = new List<SpatialFeature>();
    
    public Layer()
    {
        CreatedDate = DateTime.Now;
        ModifiedDate = DateTime.Now;
    }
    
    public Layer(string name, string layerType)
    {
        Name = name;
        LayerType = layerType;
        CreatedDate = DateTime.Now;
        ModifiedDate = DateTime.Now;
    }
    
    public override string ToString()
    {
        return $"{Name} ({LayerType})";
    }
    
    public string GetSummary()
    {
        var summary = $"{Name} - {LayerType}";
        
        if (!string.IsNullOrEmpty(Description))
            summary += $"\n{Description}";
        
        summary += $"\nFeatures: {FeatureCount}";
        summary += $"\nVisible: {IsVisible}";
        summary += $"\nEditable: {IsEditable}";
        
        if (MinX.HasValue && MinY.HasValue && MaxX.HasValue && MaxY.HasValue)
        {
            summary += $"\nExtent: ({MinX:F6}, {MinY:F6}) to ({MaxX:F6}, {MaxY:F6})";
        }
        
        return summary;
    }
    
    public void UpdateExtent()
    {
        if (SpatialFeatures.Any())
        {
            var minX = SpatialFeatures.Min(f => f.MinX);
            var minY = SpatialFeatures.Min(f => f.MinY);
            var maxX = SpatialFeatures.Max(f => f.MaxX);
            var maxY = SpatialFeatures.Max(f => f.MaxY);
            
            MinX = minX;
            MinY = minY;
            MaxX = maxX;
            MaxY = maxY;
        }
    }
    
    public bool HasFeatures => SpatialFeatures.Any();
    
    public bool IsPointLayer => LayerType.Equals("Point", StringComparison.OrdinalIgnoreCase);
    public bool IsLineLayer => LayerType.Equals("LineString", StringComparison.OrdinalIgnoreCase);
    public bool IsPolygonLayer => LayerType.Equals("Polygon", StringComparison.OrdinalIgnoreCase);
    
    public string GetLayerIcon()
    {
        return LayerType.ToLower() switch
        {
            "point" => "📍",
            "linestring" => "🛣️",
            "polygon" => "🗺️",
            "multipoint" => "📍",
            "multilinestring" => "🛣️",
            "multipolygon" => "🗺️",
            _ => "📄"
        };
    }
}
