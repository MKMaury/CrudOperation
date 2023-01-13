using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PieInfo.Models.ViewModel
{
    public class ProductVM
    {
      public Product  Products{ get; set; }
        [ValidateNever]
       public IEnumerable<Product> products { get; set; } =new List<Product>();
        [ValidateNever]
        public  IEnumerable<SelectListItem> categories { get; set;}
    }
}
