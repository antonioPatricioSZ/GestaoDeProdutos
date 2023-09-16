using Domain.Enum;

namespace Domain.Entities;

public class Category : BaseEntity {

    public CategoryEnum CategoryType { get; set; }
    public string Description { get; set; }
    public ICollection<Product> Products { get; set;}

}
