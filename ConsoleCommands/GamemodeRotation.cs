using System.Text;
using BattleBitApi.Helpers;

namespace BattleBitApi.ConsoleCommands;

public class GamemodeRotation : ConsoleCommand
{
    public GamemodeRotation() : base(
        name: "gm",
        description: "Add, remove or list current gamemodes.",
        usage: "map <remove (r), add (a), list (ls), reload (rl)> [gamemode name]"
    )
    {
        Action = args =>
        {
            if (args.Length < 1)
            {
                Logger.Info($"Invalid arguments. Usage: {Usage}");
                return;
            }
            
            string action = args[0];
            args = args.Skip(1).ToArray();
            
            switch (action)
            {
                case "a":
                case "add":
                    if (args.Length < 1)
                    {
                        Logger.Info($"Invalid arguments. Usage: {Usage}");
                        return;
                    }

                    var gamemodeToAdd = string.Join(" ", args).ToUpperInvariant();
                    if (GamemodeHelper.IsValidGamemode(gamemodeToAdd))
                    {
                        if (!Server.GamemodeRotation.AddToRotation(gamemodeToAdd))
                        {
                            
                            return;
                        }

                        Program.ServerConfiguration.GamemodeRotation.Add(gamemodeToAdd);
                        Program.SaveConfiguration(Program.ServerConfiguration);
                        Logger.Info($"Added {gamemodeToAdd} to the rotation.");
                    }
                    else
                    {
                        Logger.Info("Invalid gamemode.");
                    }

                    break;
                case "r":
                case "remove":
                    if (args.Length < 1)
                    {
                        Logger.Info($"Invalid arguments. Usage: {Usage}");
                        return;
                    }

                    var gamemodeToRemove = string.Join(" ", args).ToUpperInvariant();
                    if (GamemodeHelper.IsValidGamemode(gamemodeToRemove))
                    {
                        if (!Server.GamemodeRotation.RemoveFromRotation(gamemodeToRemove))
                        {
                            Logger.Info($"Gamemode {gamemodeToRemove} not found in the rotation.");
                            return;
                        }

                        Program.ServerConfiguration.GamemodeRotation.Remove(gamemodeToRemove);
                        Program.SaveConfiguration(Program.ServerConfiguration); 
                        Logger.Info($"Removed {gamemodeToRemove} from the rotation.");
                    }
                    else
                    {
                        Logger.Info("Invalid gamemode.");
                    }

                    break;
                case "ls":
                case "list":
                    var gamemodes = new StringBuilder();
                    foreach (var gamemode in Server.GamemodeRotation.GetGamemodeRotation())
                    {
                        gamemodes.Append($"{gamemode}, ");
                    }

                    Logger.Info($"Current gamemodes: {gamemodes.ToString().TrimEnd(' ', ',')}");
                    break;
                case "rl":
                case "reload":
                    Program.ReloadConfiguration();
                    
                    Logger.Info("Reloaded configuration.");
                    break;
                default:
                    Logger.Info($"Invalid action. Usage: {Usage}");
                    break;
            }
        };
    }
}