namespace AwesomeMusic.Services.Shared
{
    using System.Text.RegularExpressions;

    public class RegularExpressions
    {
        public static Regex IsEmailAddressValid = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        public static Regex HasLowerChar = new(@"[a-z]+");
        public static Regex HasUpperChar = new(@"[A-Z]+");
        public static Regex HasSpecialChar = new(@"[!@#?\]]+");
        public static Regex HasMinimum10Chars = new(@".{10,}");
    }
}
