namespace Modulo_de_autorizacao_e_autenticacao.Models;

public record ChangePassword()
{
    public string? accessToken { get; set; }
    public string? Password { get; set; }
};