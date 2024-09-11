using System.Text.Json;
using Módulo_de_autorização_e_autenticação.Models;
using Supabase;
using Client = Supabase.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapPost("/createaccount", async (UserAccount userAccount) =>
    {
        var responseMessage = new ResponseMessage();
        
        try
        { 
            await supabase.Auth.SignUp(userAccount.email, userAccount.password);
            responseMessage.message = "Account created success";
            Results.Ok(responseMessage);
        }
        catch (Exception e)
        {
            responseMessage.message = ExtractErrorMessage(e.Message);
            Results.BadRequest(responseMessage);
        }
    })
    .Produces<UserAccount>(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status400BadRequest)
    .WithName("PostCreateAccount")
    .WithTags("Account")
    .WithOpenApi();

/*
app.MapPost("/login", (UserAccount userAccount) =>
    {
        supabase.Auth.SignIn(userAccount.email, userAccount.password);
        return supabase.Auth.CurrentUser;
        
    })
    .WithName("PostLogin")
    .WithOpenApi();
*/

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