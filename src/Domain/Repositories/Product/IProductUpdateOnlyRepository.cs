namespace Domain.Repositories.Product;

public interface IProductUpdateOnlyRepository {

    Task<Entities.Product> GetById(long productId);

    void Update(Entities.Product product);

}
