namespace Clean_E_Commerce_Project.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductsRepository { get; }
        ICategoryRepository Categories { get; }
        IReviewRepository ReviewsRepository { get; }    
        //IOrderRepository Orders { get; }
        //ICartItemRepository CartItems { get; }
        //IUserRepository Users { get; }
        Task<int> SaveChangesAsync();
    }
}
