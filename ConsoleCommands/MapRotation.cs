using System.Text;
using BattleBitApi.Helpers;

namespace BattleBitApi.ConsoleCommands;

public class MapRotation : ConsoleCommand
{
    public MapRotation() : base(
        name: "map",
        description: "Add, remove or list current maps.",
        usage: "map <remove (r), add (a), list (ls), reload (rl)> [map name]"
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
                    
                    var mapToAdd = string.Join(" ", args).ToUpperInvariant();
                    if (MapHelper.IsValidMap(mapToAdd))
                    {
                        if (!Server.MapRotation.AddToRotation(mapToAdd))
                        {
                            Logger.Info("Map already exists in the rotation.");
                            return;
                        }
                        
                        Program.ServerConfiguration.MapRotation.Add(mapToAdd);
                        Program.SaveConfiguration(Program.ServerConfiguration);
                        Logger.Info($"Added {mapToAdd} to the rotation.");
                    } 
                    else
                    {
                        Logger.Info("Invalid map.");
                    }
                    break;
                case "r":
                case "remove":
                    if (args.Length < 1)
                    {
                        Logger.Info($"Invalid arguments. Usage: {Usage}");
                        return;
                    }
                    
                    var mapToRemove = string.Join(" ", args).ToUpperInvariant();
                    if (MapHelper.IsValidMap(mapToRemove))
                    {
                        if (!Server.MapRotation.RemoveFromRotation(mapToRemove))
                        {
                            Logger.Info("Map does not exist in the rotation.");
                            return;
                        }
                        
                        Program.ServerConfiguration.MapRotation.Remove(mapToRemove);
                        Program.SaveConfiguration(Program.ServerConfiguration);
                        Logger.Info($"Removed {mapToRemove} from the rotation.");
                    } 
                    else
                    {
                        Logger.Info("Invalid map.");
                    }
                    break;
                case "ls":
                case "list":
                    var maps = new StringBuilder();
                    foreach (var map in Server.MapRotation.GetMapRotation())
                    {
                        maps.Append($"{map}, ");
                    }
                    
                    Logger.Info($"Maps: {maps.ToString().TrimEnd(' ', ',')}");
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