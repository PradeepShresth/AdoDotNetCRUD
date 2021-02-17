using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_ADO.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        [DisplayName("Product Name")]
        public string Name { get; set; }
    }
}
