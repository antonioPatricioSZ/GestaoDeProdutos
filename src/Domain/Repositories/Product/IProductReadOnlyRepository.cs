namespace Domain.Repositories.Product;


public interface IProductReadOnlyRepository {

    Task<IList<Entities.Product>> GetAllProducts();

    Task<Entities.Product> GetProductById(long productId);

}
