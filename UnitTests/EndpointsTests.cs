using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Modulo_de_autorizacao_e_autenticacao.Models;
using Modulo_de_autorizacao_e_autenticacao;
using Xunit;

namespace UnitTests
{
    public class AuthEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public AuthEndpointsTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        /*
        [Fact]
        public async Task CreateAccount_ShouldReturnCreated_WhenValidUser()
        {
            var userAccount = new UserAccount("felipewilliam843@gmail.com", "1b-Aaaaa");
            var response = await _client.PostAsJsonAsync("/createaccount", userAccount);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var createdUser = await response.Content.ReadFromJsonAsync<UserAccount>();
            createdUser.Should().NotBeNull();
        }*/

        [Fact]
        public async Task CreateAccount_ShouldReturnBadRequest_WhenInvalidUser()
        {
            var userAccount = new UserAccount("invalidemail", "");
            var response = await _client.PostAsJsonAsync("/createaccount", userAccount);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var errorMessage = await response.Content.ReadFromJsonAsync<ResponseMessage>();
            errorMessage.Should().NotBeNull();
        }

        [Fact]
        public async Task Login_ShouldReturnOk_WhenValidCredentials()
        {
            var userAccount = new UserAccount("a@gmail.com", "1b-Aaaaa");
            var response = await _client.PostAsJsonAsync("/loginaccount", userAccount);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessage>();
            responseMessage.Should().NotBeNull();
            responseMessage!.message.Should().Be("Login successfully");
        }

        [Fact]
        public async Task Login_ShouldReturnBadRequest_WhenInvalidCredentials()
        {
            var userAccount = new UserAccount("wronguser@example.com", "wrongpassword");
            var response = await _client.PostAsJsonAsync("/loginaccount", userAccount);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessage>();
            responseMessage.Should().NotBeNull();
        }

        [Fact]
        public async Task Logout_ShouldReturnOk()
        {
            var response = await _client.PostAsync("/logout", null);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessage>();
            responseMessage.Should().NotBeNull();
            responseMessage!.message.Should().Be("Logout successfully");
        }

        /*
        [Fact]
        public async Task PasswordRecovery_ShouldReturnOk_WhenEmailExists()
        {
            var email = "felipewilliam843@gmail.com";
            var response = await _client.PostAsJsonAsync("/passwordrecovery", email);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessage>();
            responseMessage.Should().NotBeNull();
            responseMessage!.message.Should().Be("Confirm in your email!");
        }/*

        /*
        [Fact]
        public async Task ChangePassword_ShouldReturnOk_WhenPasswordChangedSuccessfully()
        {
            var newPassword = "1b-Aaaaa";
            var response = await _client.PutAsJsonAsync("/changepassword", newPassword);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessage>();
            responseMessage.Should().NotBeNull();
            responseMessage!.message.Should().Be("Password changed successfully");
        }*/
    }
}
