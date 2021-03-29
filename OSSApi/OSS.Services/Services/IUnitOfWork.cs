namespace OSS.Services.Services {
    public interface IUnitOfWork {
        // Customer
        ICustomerService Customers { get; }
        IProductService Products { get; }
        IOrdersService Orders { get; }
    }
}
