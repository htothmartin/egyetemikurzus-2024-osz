namespace F1H43C_EEJYN9;

public class UserManager
{
    public User CurrentUser { get; set; }
    private static UserManager? _instance;

    public static UserManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UserManager();
            }
            return _instance;
        }
    } 
    
    public bool UserLogin()
    {
        Console.Write("Enter your username: ");
        string username = Console.ReadLine().Trim();

        if (!UserValidator.IsValidUsername(username))
        {
            Console.WriteLine("Invalid username format. Must be at least 5 characters and contain only letters and numbers.");
            return false;
        }

        var user = UserRepository.Instance.GetOrCreateUser(username);
        CurrentUser = user;
        Console.Clear();
        Console.WriteLine($"Welcome, {user.Name}!\n");
        return true;
    }
    
}