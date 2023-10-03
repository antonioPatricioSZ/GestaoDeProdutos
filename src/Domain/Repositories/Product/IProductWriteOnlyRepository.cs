namespace Domain.Repositories.Product;

public interface IProductWriteOnlyRepository {

    Task<bool> AddProduct(Entities.Product product, long categoryId);

    Task Delete(long productId);

}
