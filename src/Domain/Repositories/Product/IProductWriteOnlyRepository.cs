namespace Domain.Repositories.Product;

public interface IProductWriteOnlyRepository {

    Task AddProduct(Entities.Product product);

    Task Delete(long productId);

}
