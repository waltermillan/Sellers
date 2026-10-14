using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    [Table("T_PRODUCT")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("PACKAGING_DATE")]
        public DateTime PackagingDate { get; set; }

        [Column("STOCK")]
        public int Stock { get; set; }

        [Column("PRICE")]
        public decimal Price { get; set; }
    }
}
