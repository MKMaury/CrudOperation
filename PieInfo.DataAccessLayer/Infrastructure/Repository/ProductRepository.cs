using Microsoft.EntityFrameworkCore;
using PieInfo.Data;
using PieInfo.DataAccessLayer.Infrastructure.IRepository;
using PieInfo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieInfo.DataAccessLayer.Infrastructure.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        //public CategoryRepository(ApplicationDbContext context) :base(context)
        //{
        //    _context = context;
        //}

        public void Update(Product product)
        {
            //_context.Products.Update(product);
            var updateDb=_context.Products.Where(x=>x.Id== product.Id).FirstOrDefault();
            if (updateDb!=null)
            {   
                updateDb.Price= product.Price;
                updateDb.Description= product.Description;
                updateDb.Name= product.Name;    
                if(product.ImageUrl!=null) {
                    updateDb.ImageUrl = product.ImageUrl;
                }
              
            }
        }
    }
}
