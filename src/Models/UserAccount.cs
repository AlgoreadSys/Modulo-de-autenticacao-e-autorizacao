namespace Módulo_de_autorização_e_autenticação.Models;

public record UserAccount(string email, string password)
{
    public string email { get; set; } = email;
    public string password { get; set; } = password;
};