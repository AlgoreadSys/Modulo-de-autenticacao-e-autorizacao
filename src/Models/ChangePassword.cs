namespace Módulo_de_autorização_e_autenticação.Models;

public record ChangePassword()
{
    public string? accessToken { get; set; }
    public string? Password { get; set; }
};