using Modulo_de_autorizacao_e_autenticacao.Models;
using Xunit;

namespace UnitTests
{
    public class UserAccountTests
    {
        [Fact]
        public void UserAccount_ShouldInitializeCorrectly()
        {
            var email = "test@example.com";
            var password = "password123";

            var userAccount = new UserAccount(email, password);

            Assert.Equal(email, userAccount.email);
            Assert.Equal(password, userAccount.password);
        }

        [Fact]
        public void UserAccount_ShouldAllowPropertySet()
        {
            var userAccount = new UserAccount("test@example.com", "password123")
            {
                email = "newemail@example.com",
                password = "newpassword123"
            };

            Assert.Equal("newemail@example.com", userAccount.email);
            Assert.Equal("newpassword123", userAccount.password);
        }
    }
}

