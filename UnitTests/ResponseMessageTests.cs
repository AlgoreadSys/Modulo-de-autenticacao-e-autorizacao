using Modulo_de_autorizacao_e_autenticacao.Models;
using Xunit;

namespace UnitTests
{
    public class ResponseMessageTests
    {
        [Fact]
        public void ResponseMessage_ShouldInitializeCorrectly()
        {
            var responseMessage = new ResponseMessage
            {
                message = "Test message"
            };

            Assert.Equal("Test message", responseMessage.message);
        }

        [Fact]
        public void ResponseMessage_ShouldAllowPropertySet()
        {
            var responseMessage = new ResponseMessage
            {
                message = "Initial message"
            };

            responseMessage.message = "Updated message";

            Assert.Equal("Updated message", responseMessage.message);
        }
    }
}

