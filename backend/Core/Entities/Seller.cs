using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("T_SELLER")]
    public class Seller
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("NAME")]
        public string Name { get; set; }
        [Column("BIRTHDAY")]
        public DateTime Birthday { get; set; }
    }
}
