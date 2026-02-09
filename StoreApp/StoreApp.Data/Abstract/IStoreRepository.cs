namespace StoreApp.Data.Abstract;

using StoreApp.Data.Concrete;

public interface IStoreRepository
{
    IQueryable<Product> Products { get; }
}
