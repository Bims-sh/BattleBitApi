namespace BattleBitApi.Helpers;

public class GamemodeHelper
{
    private static List<string> Gamemodes { get; } = new()
    {
        "TDM",
        "AAA",
        "RUSH",
        "CONQ",
        "DOMI",
        "ELIM",
        "INFCONQ",
        "FRONTLINE",
        "GUNGAMEFFA",
        "FFA",
        "GUNGAMETEAM",
        "SUCIDERUSH",
        "CATCH",
        "INFECTED",
        "CASHRUN",
        "VOXELFORTIFY",
        "VOXELTRENCH",
        "CTF",
        "INVASION",
        "ONELIFE"
    };
    
    public static bool IsValidGamemode(string gm)
    {
        return Gamemodes.Contains(gm.ToUpperInvariant());
    }
}