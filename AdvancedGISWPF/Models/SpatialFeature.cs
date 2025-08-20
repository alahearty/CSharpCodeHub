using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Media;

namespace AdvancedGISWPF.Models;

// Spatial feature model for GIS data
public class SpatialFeature
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
    public string FeatureType { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Category { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Subcategory { get; set; } = string.Empty;

    // PostGIS geometry field
    [Column(TypeName = "geometry")]
    public Geometry? Geometry { get; set; }

    // Spatial reference system ID
    public int SRID { get; set; } = 4326; // WGS84 by default

    // Bounding box coordinates
    public double MinX { get; set; }
    public double MinY { get; set; }
    public double MaxX { get; set; }
    public double MaxY { get; set; }

    // Metadata
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string ModifiedBy { get; set; } = string.Empty;

    // Attributes stored as JSON
    [Column(TypeName = "jsonb")]
    public string? Attributes { get; set; }

    // Calculated properties
    public bool HasGeometry => Geometry != null;
    public string GeometryType => Geometry?.GeometryType ?? "None";
    public int CoordinateCount => Geometry?.Coordinates?.Length ?? 0;
    public double Area => Geometry?.Area ?? 0;
    public double Length => Geometry?.Length ?? 0;

    // Style properties
    public string? FillColor { get; set; }
    public string? StrokeColor { get; set; }
    public double? StrokeWidth { get; set; }
    public double? Opacity { get; set; }

    // Visibility and selection
    public bool IsVisible { get; set; } = true;
    public bool IsSelected { get; set; } = false;
    public bool IsHighlighted { get; set; } = false;

    // Layer information
    public int LayerId { get; set; }
    public virtual Layer? Layer { get; set; }

    public SpatialFeature()
    {
        CreatedDate = DateTime.Now;
        ModifiedDate = DateTime.Now;
    }

    public SpatialFeature(string name, string featureType, Geometry? geometry = null)
    {
        Name = name;
        FeatureType = featureType;
        Geometry = geometry;
        CreatedDate = DateTime.Now;
        ModifiedDate = DateTime.Now;

        if (geometry != null)
        {
            UpdateBoundingBox();
        }
    }

    public void UpdateBoundingBox()
    {
        if (Geometry != null)
        {
            var envelope = Geometry.Envelope;
            MinX = envelope.MinX;
            MinY = envelope.MinY;
            MaxX = envelope.MaxX;
            MaxY = envelope.MaxY;
        }
    }

    public bool Intersects(Geometry other)
    {
        return Geometry?.Intersects(other) ?? false;
    }

    public bool Contains(Geometry other)
    {
        return Geometry?.Contains(other) ?? false;
    }

    public double DistanceTo(Geometry other)
    {
        return Geometry?.Distance(other) ?? double.MaxValue;
    }

    public Geometry? Buffer(double distance)
    {
        return Geometry?.Buffer(distance);
    }

    public Geometry? Intersection(Geometry other)
    {
        return Geometry?.Intersection(other);
    }

    public Geometry? Union(Geometry other)
    {
        return Geometry?.Union(other);
    }

    public override string ToString()
    {
        return $"{Name} ({FeatureType}) - {GeometryType}";
    }

    public string GetSummary()
    {
        var summary = $"{Name} - {FeatureType}";

        if (HasGeometry)
        {
            summary += $"\nGeometry: {GeometryType}";
            summary += $"\nCoordinates: {CoordinateCount}";

            if (Area > 0)
                summary += $"\nArea: {Area:F2}";

            if (Length > 0)
                summary += $"\nLength: {Length:F2}";
        }

        return summary;
    }
}
