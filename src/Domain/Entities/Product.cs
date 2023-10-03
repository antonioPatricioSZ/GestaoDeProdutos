namespace Domain.Entities;

public class Product : BaseEntity {

    public string Name { get; set; }
    public string Description { get; set; }
    public decimal PricePurchase { get; set; }
    public decimal PriceSale { get; set; } 
    public long CategoryId { get; set; }
    // Esta propriedade representa o ID da categoria à qual este produto pertence

    public Category Category { get; set; }

    // Esta propriedade representa a categoria à qual este produto pertence

}
