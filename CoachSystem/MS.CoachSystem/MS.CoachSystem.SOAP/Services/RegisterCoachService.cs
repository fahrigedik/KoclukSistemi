using System.Net.Http;
using System.Text;
using System.Text.Json;
using MS.CoachSystem.SOAP.Contracts;

public class RegisterCoachService : IRegisterCoachService
{
    public string RegisterCoachUser(
        string userName,
        string email,
        string name,
        string surname,
        string password,
        string city,
        string birthDate,
        string tckn,
        int gender)
    {
        string apiUrl = "https://localhost:7076/api/User/CreateCoachUser";

        var payload = new
        {
            userName = userName,
            email = email,
            name = name,
            surname = surname,
            password = password,
            city = city,
            birthDate = birthDate,
            tckn = tckn,
            gender = gender
        };

        using var client = new HttpClient();
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        var response = client.PostAsync(apiUrl, content).Result;

        if (response.IsSuccessStatusCode)
        {
            return "Coach user created successfully!";
        }
        else
        {
            return $"Failed to create coach user. Status Code: {response.StatusCode}";
        }
    }
}