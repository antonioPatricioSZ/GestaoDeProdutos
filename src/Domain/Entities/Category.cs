namespace Domain.Entities;

public class Category : BaseEntity {

    public string CategoryName { get; set; }
    public string Description { get; set; }
    public ICollection<Product> Products { get; set;}
    // Esta propriedade representa a coleção de produtos associados a esta categoria
}
