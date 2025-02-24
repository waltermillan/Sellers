using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("Sale")]
public class Sale
{
    [Column("id")]
    public int Id { get; set; }

    [Column("id_buyer")]
    public int IdBuyer { get; set; }

    [Column("id_seller")]
    public int IdSeller { get; set; }

    [Column("id_product")]
    public int IdProduct { get; set; }

    [Column("date")]
    public string  Date { get; set; }
}
