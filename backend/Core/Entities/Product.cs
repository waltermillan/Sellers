using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    [Table("Product")]
    public class Product
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("packaging_date")]
        public DateTime PackagingDate { get; set; }

        [Column("stock")]
        public int Stock { get; set; }

        [Column("price")]
        public decimal Price { get; set; }
    }
}
