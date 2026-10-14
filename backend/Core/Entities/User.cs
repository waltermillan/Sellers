using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("T_USER")]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID")]
    public int Id { get; set; }
    [Required]
    [Column("USERNAME")]
    public string UserName { get; set; }
    [Required]
    [Column("PASSWORD_HASH")]
    public string PasswordHash { get; set; }
    [Required]
    [Column("CREATED_AT")]
    public DateTime CreatedAt { get; set; }
    [Required]
    [Column("UPDATED_AT")]
    public DateTime UpdatedAt { get; set; }
}
