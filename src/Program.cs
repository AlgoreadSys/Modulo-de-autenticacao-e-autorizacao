using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Modulo_de_autorizacao_e_autenticacao.Models;
using Supabase;
using Supabase.Gotrue;
using Client = Supabase.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Permitir conexões externas na porta 5152
        builder.WebHost.UseUrls("http://0.0.0.0:5152");

        // Adicionar política de CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder => builder
                    .WithOrigins("frontend-eta-dusky-33.vercel.app", "www.algoread.com.br") // Permite apenas essa origem
                    .AllowAnyMethod()                     // Permite todos os métodos (GET, POST, etc.)
                    .AllowAnyHeader()                     // Permite todos os cabeçalhos
                    .AllowCredentials());                 // Permite envio de cookies ou credenciais
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        var url = "https://eabruaevckpvepqqqmfv.supabase.co";
        var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImVhYnJ1YWV2Y2twdmVwcXFxbWZ2Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MjU4MjczOTgsImV4cCI6MjA0MTQwMzM5OH0.Eczsx_ij7fybFr3cZFBP0jtmmxbZMZYwP-FeuBjciyE";

        var options = new SupabaseOptions
        {
            AutoConnectRealtime = true
        };

        var supabase = new Client(url, key, options);
        await supabase.InitializeAsync();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Usar a política de CORS definida
        app.UseCors("AllowSpecificOrigin");

        app.MapPost("/createaccount", async (UserAccount userAccount) =>
        {
            var responseMessage = new ResponseMessage();

            try
            {
                await supabase.Auth.SignUp(userAccount.email, userAccount.password);
                return Results.Created("Account created successfully", userAccount);
            }
            catch (Exception e)
            {
                responseMessage.message = ExtractErrorMessage(e.Message);
                return Results.BadRequest(responseMessage);
            }
        })
            .Produces<UserAccount>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("PostCreateAccount")
            .WithTags("Account")
            .WithOpenApi();

        app.MapPost("/loginaccount", async ([FromBody] UserAccount userAccount) =>
        {
            var responseMessage = new ResponseMessage();

            try
            {
                await supabase.Auth.SignIn(userAccount.email, userAccount.password);
                NewAudit("logged in!");
                responseMessage.message = "Login successfully";
                return Results.Ok(responseMessage);
            }
            catch (Exception e)
            {
                responseMessage.message = ExtractErrorMessage(e.Message);
                return Results.BadRequest(responseMessage);
            }
        })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("PostLoginAccount")
            .WithTags("Account")
            .WithOpenApi();

        app.MapPost("/logout", async () =>
        {
            var responseMessage = new ResponseMessage();

            try
            {
                await supabase.Auth.SignOut();
                responseMessage.message = "Logout successfully";
                return Results.Ok(responseMessage);
            }
            catch (Exception e)
            {
                responseMessage.message = ExtractErrorMessage(e.Message);
                return Results.BadRequest(responseMessage);
            }
        })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("PostLogout")
            .WithTags("Account")
            .WithOpenApi();

        app.MapPost("/passwordrecovery", async ([FromBody] string email) =>
        {
            var responseMessage = new ResponseMessage();

            try
            {
                await supabase.Auth.ResetPasswordForEmail(email);
                responseMessage.message = "Confirm in your email!";
                return Results.Ok(responseMessage);
            }
            catch (Exception e)
            {
                responseMessage.message = ExtractErrorMessage(e.Message);
                return Results.BadRequest(responseMessage);
            }
        })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("PostPasswordRecovery")
            .WithTags("Account")
            .WithOpenApi();

        app.MapPut("/changepassword", async ([FromBody] string newPassword) =>
        {
            var responseMessage = new ResponseMessage();

            try
            {
                var changePassword = new UserAttributes { Password = newPassword };
                await supabase.Auth.Update(changePassword);
                NewAudit("Password Changed!");
                responseMessage.message = "Password changed successfully";
                return Results.Ok(responseMessage);
            }
            catch (Exception e)
            {
                responseMessage.message = ExtractErrorMessage(e.Message);
                return Results.BadRequest(responseMessage);
            }
        })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("PutChangePassword")
            .WithTags("Account")
            .WithOpenApi();

        app.Run();

        static string? ExtractErrorMessage(string errorMessage)
        {
            try
            {
                var jsonDocument = JsonDocument.Parse(errorMessage);
                if (jsonDocument.RootElement.TryGetProperty("msg", out var msgElement))
                {
                    return msgElement.GetString();
                }
            }
            catch (JsonException)
            {
                return "Error processing the error message.";
            }

            return "Unknown error message.";
        }

        async void NewAudit(string auditLog)
        {
            try
            {
                await supabase.From<AuthAudit>()
                    .Insert(new AuthAudit
                    {
                        created_at = DateTimeOffset.Now,
                        auth_id = Guid.Parse(supabase.Auth.CurrentUser!.Id!),
                        audit_log = auditLog
                    });
            }
            catch (Exception e)
            {
                Console.WriteLine(ExtractErrorMessage(e.Message));
                throw;
            }
        }
    }

    
}

