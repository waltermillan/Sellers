using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("T_SALE")]
public class Sale
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
}
