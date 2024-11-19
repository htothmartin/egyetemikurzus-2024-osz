using F1H43C_EEJYN9.Entities;
using F1H43C_EEJYN9.Repository;

namespace F1H43C_EEJYN9.Core;

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
        Console.Write("Add meg a felhasználóneved: ");
        string username = Console.ReadLine().Trim();

        if (!UserValidator.IsValidUsername(username))
        {
            Console.WriteLine("Érvénytelen felhasználónév formátum. Legalább 5 karakter hosszú kell legyen, és csak betűket és számokat tartalmazhat.");
            return false;
        }

        var user = UserRepository.Instance.GetOrCreateUser(username);
        CurrentUser = user;
        Console.Clear();
        return true;
    }
    
}