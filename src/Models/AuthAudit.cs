using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Modulo_de_autorizacao_e_autenticacao.Models;

[Table("authAudit")]
public class AuthAudit : BaseModel
{
    [PrimaryKey()]
    public int id { get; set; }
    
    [Column()]
    public DateTimeOffset created_at { get; set; }
    
    [Column()]
    public Guid auth_id { get; set; }
    
    [Column()]
    public string? audit_log { get; set; }
    
}