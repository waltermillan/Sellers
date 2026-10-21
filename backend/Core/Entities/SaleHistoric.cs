using Core.Interfases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    [Table("T_SALE_HISTORIC")]
    public class SaleHistoric
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [Column("NUMBER_OF_SALES")]
        public int NumberOfSales { get; set; }
        [Column("STATUS")]
        public string Status { get; set; }
        [Column("PROCESSED_DATE")]
        public DateTime ProcessedDate { get; set; }
    }
}
