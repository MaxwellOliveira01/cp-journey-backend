using Microsoft.Extensions.Configuration;
using Npgsql;

var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

var connectionString = config["ConnectionStrings:DefaultConnection"];

const string pdfPath = @"C:\Users\MaxwellReis\Desktop\cp_backend\tutoriais-1-mdp-cerrado.pdf";

var pdfBytes = await File.ReadAllBytesAsync(pdfPath);

await using var connection = new NpgsqlConnection(connectionString);

await connection.OpenAsync();

var command = new NpgsqlCommand(
    "UPDATE \"Contests\" SET \"TutorialPdf\" = @pdf WHERE \"Id\" = 1", connection);

command.Parameters.Add("@pdf", NpgsqlTypes.NpgsqlDbType.Bytea).Value = pdfBytes;

var rowsAffected = command.ExecuteNonQuery();

Console.WriteLine($"{rowsAffected} row(s) updated.");