using Dhoojol.Domain.Entities.Users;
using Dhoojol.Domain.Entities.Products;
using Dhoojol.Infrastructure.EfCore.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhoojol.Infrastructure.EfCore.Repositories.Products
{
    public interface IProductRepository : IRepository<Product>
    {

    }

    public class ProductRepository : EfRepository<Product>, IProductRepository
    {
        private readonly DhoojolContext _dbContext;

        public ProductRepository(DhoojolContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
