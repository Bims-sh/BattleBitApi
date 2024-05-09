namespace BattleBitApi.Helpers;

public class GamemodeHelper
{
    private static List<string> GameModes { get; } = new()
    {
        "TDM",
        "FFA",
        "DOMI",
        "CTF",
    };
    
    public static bool IsValidGameMode(string gm)
    {
        return GameModes.Contains(gm.ToUpperInvariant());
    }
}