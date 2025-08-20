using Microsoft.EntityFrameworkCore;
using AdvancedGISWPF.Models;

namespace AdvancedGISWPF.Data;

// Entity Framework DbContext for GIS database
public class GISDbContext : DbContext
{
    public DbSet<Layer> Layers { get; set; }
    public DbSet<SpatialFeature> SpatialFeatures { get; set; }
    
    public GISDbContext(DbContextOptions<GISDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configure Layer entity
        modelBuilder.Entity<Layer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.LayerType).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.Subcategory).HasMaxLength(50);
            
            // Indexes
            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasIndex(e => e.LayerType);
            entity.HasIndex(e => e.Category);
            entity.HasIndex(e => e.Order);
            
            // Relationships
            entity.HasMany(e => e.SpatialFeatures)
                  .WithOne(e => e.Layer)
                  .HasForeignKey(e => e.LayerId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        
        // Configure SpatialFeature entity
        modelBuilder.Entity<SpatialFeature>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.FeatureType).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.Subcategory).HasMaxLength(50);
            
            // PostGIS geometry field
            entity.Property(e => e.Geometry).HasColumnType("geometry");
            
            // JSON attributes field
            entity.Property(e => e.Attributes).HasColumnType("jsonb");
            
            // Indexes
            entity.HasIndex(e => e.Name);
            entity.HasIndex(e => e.FeatureType);
            entity.HasIndex(e => e.Category);
            entity.HasIndex(e => e.LayerId);
            
            // Spatial indexes (PostGIS specific)
            entity.HasIndex(e => e.Geometry).HasMethod("gist");
            
            // Relationships
            entity.HasOne(e => e.Layer)
                  .WithMany(e => e.SpatialFeatures)
                  .HasForeignKey(e => e.LayerId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        
        // Seed data
        SeedData(modelBuilder);
    }
    
    private void SeedData(ModelBuilder modelBuilder)
    {
        // This method can be used to seed initial data if needed
        // For now, we'll let the DatabaseService handle sample data creation
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Fallback configuration if not configured externally
            optionsBuilder.UseNpgsql("Host=localhost;Database=gis_database;Username=gis_user;Password=gis_password;Port=5432",
                options => options.UseNetTopologySuite());
        }
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Update timestamps before saving
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is Layer || e.Entity is SpatialFeature)
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
        
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                if (entry.Entity is Layer layer)
                {
                    layer.CreatedDate = DateTime.Now;
                    layer.ModifiedDate = DateTime.Now;
                }
                else if (entry.Entity is SpatialFeature feature)
                {
                    feature.CreatedDate = DateTime.Now;
                    feature.ModifiedDate = DateTime.Now;
                }
            }
            else if (entry.State == EntityState.Modified)
            {
                if (entry.Entity is Layer layer)
                {
                    layer.ModifiedDate = DateTime.Now;
                }
                else if (entry.Entity is SpatialFeature feature)
                {
                    feature.ModifiedDate = DateTime.Now;
                }
            }
        }
        
        return await base.SaveChangesAsync(cancellationToken);
    }
}
