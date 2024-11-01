using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Modulo_de_autorizacao_e_autenticacao.Models;

[Table("authAudit")]
public class AuthAudit : BaseModel
{
    [PrimaryKey()]
    public long id { get; set; }
    
    [Column()]
    public DateTimeOffset created_at { get; set; }
    
    [Column()]
    public Guid auth_id { get; set; }
    
    [Column()]
    public string? audit_log { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is AuthAudit other)
        {
            return id == other.id &&
                   created_at == other.created_at &&
                   auth_id == other.auth_id &&
                   audit_log == other.audit_log;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(id, created_at, auth_id, audit_log);
    }

}