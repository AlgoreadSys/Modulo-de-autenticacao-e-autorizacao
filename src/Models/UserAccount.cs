namespace Modulo_de_autorizacao_e_autenticacao.Models;

public record UserAccount(string email, string password)
{
    public string email { get; set; } = email;
    public string password { get; set; } = password;
};