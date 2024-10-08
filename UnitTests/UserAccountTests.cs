using Xunit;
using Modulo_de_autorizacao_e_autenticacao.Models;

namespace UnitTests
{
    public class UserAccountTests
    {
        [Fact]
        public void UserAccount_CreatesObjectWithGivenValues()
        {
            // Arrange
            string expectedEmail = "user@example.com";
            string expectedPassword = "password123";

            // Act
            var userAccount = new UserAccount(expectedEmail, expectedPassword);

            // Assert
            Assert.Equal(expectedEmail, userAccount.email);
            Assert.Equal(expectedPassword, userAccount.password);
        }

        [Fact]
        public void UserAccount_ObjectsWithSameValues_AreEqual()
        {
            // Arrange
            var userAccount1 = new UserAccount("user@example.com", "password123");
            var userAccount2 = new UserAccount("user@example.com", "password123");

            // Act & Assert
            Assert.Equal(userAccount1, userAccount2);
        }

        [Fact]
        public void UserAccount_ObjectsWithDifferentValues_AreNotEqual()
        {
            // Arrange
            var userAccount1 = new UserAccount("user1@example.com", "password123");
            var userAccount2 = new UserAccount("user2@example.com", "password123");

            // Act & Assert
            Assert.NotEqual(userAccount1, userAccount2);
        }

        [Fact]
        public void UserAccount_CanChangeEmailProperty()
        {
            // Arrange
            var userAccount = new UserAccount("user@example.com", "password123");
            string newEmail = "newuser@example.com";

            // Act
            userAccount.email = newEmail;

            // Assert
            Assert.Equal(newEmail, userAccount.email);
        }

        [Fact]
        public void UserAccount_CanChangePasswordProperty()
        {
            // Arrange
            var userAccount = new UserAccount("user@example.com", "password123");
            string newPassword = "newpassword123";

            // Act
            userAccount.password = newPassword;

            // Assert
            Assert.Equal(newPassword, userAccount.password);
        }
    }
}