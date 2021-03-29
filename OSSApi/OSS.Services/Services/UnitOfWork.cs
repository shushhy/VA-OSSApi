namespace OSS.Services.Services {
    public class UnitOfWork : IUnitOfWork {

        public ICustomerService Customers { get; }
        public IProductService Products { get; }
        public IOrdersService Orders { get; }

        public UnitOfWork(ICustomerService customerService, IProductService productService, IOrdersService ordersService) {
            Customers = customerService;
            Products = productService;
            Orders = ordersService;
        }
    }
}
