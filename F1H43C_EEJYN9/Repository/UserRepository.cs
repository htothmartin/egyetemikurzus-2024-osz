
using System.Text.Json;
using System.Text.Json.Serialization;

using F1H43C_EEJYN9.Entities;
namespace F1H43C_EEJYN9.Repository;

public class UserRepository
{
    private const string DatabaseFile = "database.json";
    private List<User> _users;

    public UserRepository()
    {
        LoadUsers();
    }

    public List<User> Users
    {
        get { return _users; }
    }

    public User GetOrCreateUser(string username)
    {
        var user = _users.Find(u => u.Name.Equals(username, StringComparison.OrdinalIgnoreCase));

        if (user == null)
        {
            user = new User(username);
            _users.Add(user);
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
                _users = JsonSerializer.Deserialize<List<User>>(stream) ?? new List<User>();
            }
            else
            {
                _users = new List<User>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading users: {ex.Message}");
            _users = new List<User>();
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
            JsonSerializer.Serialize(stream, _users, options);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving users: {ex.Message}");
        }
    }
}
