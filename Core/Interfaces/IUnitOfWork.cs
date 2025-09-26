namespace Clean_E_Commerce_Project.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductsRepository { get; }
        ICategoryRepository Categories { get; }
        IReviewRepository ReviewsRepository { get; }

        ICartRepository CartsRepository { get; }
        //IOrderRepository Orders { get; }
        //IUserRepository Users { get; }
        Task<int> SaveChangesAsync();
    }
}
