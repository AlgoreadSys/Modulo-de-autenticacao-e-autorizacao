using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Módulo_de_autorização_e_autenticação.Models;
using Supabase;
using Client = Supabase.Client;

var builder = WebApplication.CreateBuilder(args);

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

app.MapPost("/loginaccount", async ([FromBody]UserAccount userAccount) =>
    {
        var responseMessage = new ResponseMessage();
        
        try
        { 
            await supabase.Auth.SignIn(userAccount.email, userAccount.password);
            NewAudit("Logado!");
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

app.Run();

string? ExtractErrorMessage(string errorMessage)
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
        await supabase.From<authAudit>()
            .Insert(new authAudit
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