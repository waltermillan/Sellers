using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("User")]
public class User
{
    [Column("id")]
    public int Id { get; set; }
    [Column("username")]
    public string UserName { get; set; }
    [Column("password_hash")]
    public string PasswordHash { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
