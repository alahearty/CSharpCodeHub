namespace InventoryManagementSystem.Models;

// Product model for inventory management
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Cost { get; set; }
    public int Quantity { get; set; }
    public int MinStockLevel { get; set; }
    public int MaxStockLevel { get; set; }
    public string Unit { get; set; } = string.Empty;
    public int SupplierId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public bool IsActive { get; set; }

    // Calculated properties
    public decimal TotalValue => Price * Quantity;
    public decimal ProfitMargin => Price - Cost;
    public decimal ProfitMarginPercentage => Cost > 0 ? ((Price - Cost) / Cost) * 100 : 0;
    public bool IsLowStock => Quantity <= MinStockLevel;
    public bool IsOutOfStock => Quantity <= 0;
    public bool IsOverStocked => Quantity > MaxStockLevel;

    public Product()
    {
        CreatedDate = DateTime.Now;
        LastModifiedDate = DateTime.Now;
        IsActive = true;
    }

    public Product(int id, string name, string description, string category, string sku, 
                  decimal price, decimal cost, int quantity, int minStockLevel, int maxStockLevel, 
                  string unit, int supplierId)
    {
        Id = id;
        Name = name;
        Description = description;
        Category = category;
        SKU = sku;
        Price = price;
        Cost = cost;
        Quantity = quantity;
        MinStockLevel = minStockLevel;
        MaxStockLevel = maxStockLevel;
        Unit = unit;
        SupplierId = supplierId;
        CreatedDate = DateTime.Now;
        LastModifiedDate = DateTime.Now;
        IsActive = true;
    }

    public void UpdateStock(int quantityChange, string reason = "")
    {
        var oldQuantity = Quantity;
        Quantity = Math.Max(0, Quantity + quantityChange);
        
        if (Quantity != oldQuantity)
        {
            LastModifiedDate = DateTime.Now;
            
            var changeType = quantityChange > 0 ? "added" : "removed";
            var changeAmount = Math.Abs(quantityChange);
            
            Console.WriteLine($"📦 Stock updated: {changeAmount} {Unit} {changeType} to {Name}");
            Console.WriteLine($"   New quantity: {Quantity} {Unit}");
            
            if (IsLowStock)
            {
                Console.WriteLine($"⚠️  Warning: {Name} is running low on stock!");
            }
            
            if (IsOutOfStock)
            {
                Console.WriteLine($"❌ Alert: {Name} is out of stock!");
            }
        }
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 0)
        {
            throw new ArgumentException("Price cannot be negative");
        }

        var oldPrice = Price;
        Price = newPrice;
        LastModifiedDate = DateTime.Now;
        
        Console.WriteLine($"💰 Price updated for {Name}: ${oldPrice:F2} → ${Price:F2}");
        Console.WriteLine($"   New profit margin: {ProfitMarginPercentage:F1}%");
    }

    public void UpdateCost(decimal newCost)
    {
        if (newCost < 0)
        {
            throw new ArgumentException("Cost cannot be negative");
        }

        var oldCost = Cost;
        Cost = newCost;
        LastModifiedDate = DateTime.Now;
        
        Console.WriteLine($"💵 Cost updated for {Name}: ${oldCost:F2} → ${Cost:F2}");
        Console.WriteLine($"   New profit margin: {ProfitMarginPercentage:F1}%");
    }

    public string GetStockStatus()
    {
        if (IsOutOfStock)
            return "Out of Stock";
        if (IsLowStock)
            return "Low Stock";
        if (IsOverStocked)
            return "Over Stocked";
        return "Normal";
    }

    public string GetStockStatusIcon()
    {
        return GetStockStatus() switch
        {
            "Out of Stock" => "❌",
            "Low Stock" => "⚠️",
            "Over Stocked" => "📈",
            _ => "✅"
        };
    }

    public override string ToString()
    {
        return $"ID: {Id} | {Name} | {Category} | Qty: {Quantity} {Unit} | Price: ${Price:F2} | Status: {GetStockStatusIcon()} {GetStockStatus()}";
    }
}
