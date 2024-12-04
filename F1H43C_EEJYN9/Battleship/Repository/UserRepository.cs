
using System.Text.Json;
using System.Text.Json.Serialization;

using F1H43C_EEJYN9.Entities;
namespace F1H43C_EEJYN9.Repository;

public class UserRepository
{
    private const string DatabaseFile = "database.json";
    public List<User> Users { get; private set; }

    public UserRepository()
    {
        LoadUsers();
    }
    
    public User GetOrCreateUser(string username)
    {
        var user = Users.Find(u => u.Name.Equals(username, StringComparison.OrdinalIgnoreCase));

        if (user == null)
        {
            user = new User(username);
            Users.Add(user);
            SaveUsers();
        }

        return user;
    }

    private void LoadUsers()
    {
        try
        {
            var path = Path.Combine(AppContext.BaseDirectory, DatabaseFile);
            if (File.Exists(path))
            {
                using FileStream stream = File.OpenRead(path);
                Users = JsonSerializer.Deserialize<List<User>>(stream) ?? new List<User>();
            }
            else
            {
                Users = new List<User>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading users: {ex.Message}");
            Users = new List<User>();
        }
    }

    public void SaveUsers()
    {
        try
        {
            var path = Path.Combine(AppContext.BaseDirectory, DatabaseFile);
            using FileStream stream = File.Create(path);
            Console.WriteLine(path);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                NumberHandling = JsonNumberHandling.WriteAsString
            };
            JsonSerializer.Serialize(stream, Users, options);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving users: {ex.Message}");
        }
    }
}
