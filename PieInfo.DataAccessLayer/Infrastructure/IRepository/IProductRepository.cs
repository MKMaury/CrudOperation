using PieInfo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieInfo.DataAccessLayer.Infrastructure.IRepository
{
    public interface IProductRepository:IRepository<Product>
    {
        void Update(Product Product);
    }
}
