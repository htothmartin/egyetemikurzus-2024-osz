namespace F1H43C_EEJYN9.Entities
{
    public class User
    {
        public string Name { get; private set; }
        public DateTime LastLogin { get; set; }
        public Dictionary<int, int> GamesPlayed { get; set; }
        public Dictionary<string, string> Preferences { get; set; }

        public User(string name)
        {
            Name = name;
            LastLogin = DateTime.Now;
            GamesPlayed = new Dictionary<int, int>();
            Preferences = new Dictionary<string, string>();
        }
    }

    public static class UserValidator
    {
        public static bool IsValidUsername(string username)
        {
            return username.Length >= 5 && char.IsLetter(username[0]) && username.All(c => char.IsLetterOrDigit(c));
        }
    }
}
