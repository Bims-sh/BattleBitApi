using BattleBitAPI.Common;
using BattleBitApi.Api;

namespace BattleBitApi.Events;

public class ServerSettings : Event
{
    public override Task OnConnected()
    {
        foreach (var Map in Server.MapRotation.GetMapRotation())
        {
            Server.MapRotation.RemoveFromRotation(Map);
        }
        
        foreach (var GameMode in Server.GamemodeRotation.GetGamemodeRotation())
        {
            Server.GamemodeRotation.RemoveFromRotation(GameMode);
        }
        
        foreach (var Map in Program.ServerConfiguration.MapRotation)
        {
            Server.MapRotation.AddToRotation(Map);
        }
        
        foreach (var GameMode in Program.ServerConfiguration.GameModeRotation)
        {
            Server.GamemodeRotation.AddToRotation(GameMode);
        }
        
        if (!Server.MapRotation.GetMapRotation().Any())
        {
            Program.ReloadConfiguration();
        }
        
        if (!Server.GamemodeRotation.GetGamemodeRotation().Any())
        {
            Program.ReloadConfiguration();
        }

        var serverRotation = Server.MapRotation.GetMapRotation();
        Program.Logger.Info($"Loaded Map Rotation: {string.Join(", ", serverRotation)}");
        
        var gamemodeRotation = Server.GamemodeRotation.GetGamemodeRotation();
        Program.Logger.Info($"Loaded Gamemode Rotation: {string.Join(", ", gamemodeRotation)}");
        
        return Task.CompletedTask;
    }
}