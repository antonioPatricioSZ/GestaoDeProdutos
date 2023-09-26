namespace Domain.Entities;

public class Product : BaseEntity {

    public Product() {
        PriceSale = PricePurchase * 1.20m;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public decimal PricePurchase { get; set; }
    public decimal PriceSale { get; set; } 
    public long CategoryId { get; set; }

}
