using Xunit;
using Modulo_de_autorizacao_e_autenticacao.Models;

namespace UnitTests
{
    public class ReponseMessageTests
    {
        [Fact]
        public void ResponseMessage_CreatesObjectWithNullMessageByDefault()
        {
            // Act
            var responseMessage = new ResponseMessage();

            // Assert
            Assert.Null(responseMessage.message);
        }

        [Fact]
        public void ResponseMessage_CanSetMessageProperty()
        {
            // Arrange
            var responseMessage = new ResponseMessage();
            string expectedMessage = "Operation successful";

            // Act
            responseMessage.message = expectedMessage;

            // Assert
            Assert.Equal(expectedMessage, responseMessage.message);
        }

        [Fact]
        public void ResponseMessage_ObjectsWithSameMessage_AreEqual()
        {
            // Arrange
            var responseMessage1 = new ResponseMessage { message = "Success" };
            var responseMessage2 = new ResponseMessage { message = "Success" };

            // Act & Assert
            Assert.Equal(responseMessage1, responseMessage2);
        }

        [Fact]
        public void ResponseMessage_ObjectsWithDifferentMessage_AreNotEqual()
        {
            // Arrange
            var responseMessage1 = new ResponseMessage { message = "Success" };
            var responseMessage2 = new ResponseMessage { message = "Failure" };

            // Act & Assert
            Assert.NotEqual(responseMessage1, responseMessage2);
        }

        [Fact]
        public void ResponseMessage_CanChangeMessageProperty()
        {
            // Arrange
            var responseMessage = new ResponseMessage { message = "Initial Message" };
            string newMessage = "Updated Message";

            // Act
            responseMessage.message = newMessage;

            // Assert
            Assert.Equal(newMessage, responseMessage.message);
        }
    }
}
