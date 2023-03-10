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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        //public CategoryRepository(ApplicationDbContext context) :base(context)
        //{
        //    _context = context;
        //}

        public void Update(Category category)
        {
           // _context.Categories.Update(category);
           var categoryDb=_context.Categories.Where(x=>x.Id==category.Id).FirstOrDefault();
            if (categoryDb!=null)
            {
                categoryDb.Name=category.Name;
                categoryDb.DisplayOrder=category.DisplayOrder;
                categoryDb.CreatedDateTime = category.CreatedDateTime;
            }
        }
    }
}
