using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    [Table("T_SALE_PROCESSED")]
    public class SaleProcessed
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }

        [Column("ID_BUYER")]
        public int IdBuyer { get; set; }

        [Column("ID_SELLER")]
        public int IdSeller { get; set; }

        [Column("ID_PRODUCT")]
        public int IdProduct { get; set; }

        [Column("SALE_DATE")]
        public DateTime Date { get; set; }

        [Column("AMOUNT")]
        public Decimal Amount { get; set; }

        [Column("QUANTITY")]
        public int Quantity { get; set; }
    }
}
