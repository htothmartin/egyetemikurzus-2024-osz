using F1H43C_EEJYN9.Entities;
using F1H43C_EEJYN9.Repository;

namespace F1H43C_EEJYN9.Core;

public class UserManager
{
    public User CurrentUser { get; set; }
    private static UserManager? _instance;
    private UserRepository _userRepository;

    public static UserManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UserManager(new UserRepository());
            }
            return _instance;
        }
    }

    private UserManager(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void ShowStatistics()
    {
        bool orderDescending = false;
        while (true)
        {
            Console.Clear();
            List<GameData> orderedData;
            if (orderDescending)
            {
                orderedData = CurrentUser.GamesPlayed.OrderByDescending(g => g.Date).ToList();
            }
            else
            {
                orderedData = CurrentUser.GamesPlayed.OrderBy(g => g.Date).ToList();
            }
            foreach (GameData row in orderedData)
            {
                Console.WriteLine($"A játék dátuma: {row.Date}, Győztes: {row.Winner}");
            }
            int WinCount = orderedData.Count((gameData) => gameData.Winner == CurrentUser.Name);
            Console.WriteLine($"Győzelmek száma: {WinCount}");
            Console.WriteLine();
            Console.WriteLine($"Rendezés: {(orderDescending?"Csökkenő":"Növekvő")}");
            Console.WriteLine("Nyomd meg az 'o' gombot a rendezés megváltoztatásához.");
            Console.WriteLine("Nyomd meg 'q' gombot a kilépéshez.");
            
            var Key = Console.ReadKey(true).Key;
            if (Key == ConsoleKey.O)
            {
                orderDescending = !orderDescending;
            }
            if (Key == ConsoleKey.Q)
            {
                return;
            }
            
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

        var user = _userRepository.GetOrCreateUser(username);
        CurrentUser = user;
        Console.Clear();
        return true;
    }
    
    public void CloseAllFiles()
    {
        _userRepository.SaveUsers();
        Console.WriteLine("Minden fájl bezárva.");
        Console.Clear();
        // Itt kerülnek bezárásra a megnyitott fájlok, ha vannak
    }
    
}