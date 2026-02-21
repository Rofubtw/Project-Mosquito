using Fusion;

public readonly struct RoomViewModel
{
    public readonly string Name;
    public readonly int Current;
    public readonly int Max;
    public readonly bool IsOpen;
    public readonly bool HasPassword;

    // Builds a lightweight view model out of SessionInfo
    public RoomViewModel(SessionInfo info)
    {
        Name = info.Name;
        Current = info.PlayerCount;
        Max = info.MaxPlayers;
        IsOpen = info.IsOpen;
        HasPassword = info.Properties.TryGetValue("password", out var passProp) && !string.IsNullOrEmpty(passProp);
    }
}

public struct RoomCreateResult
{
    public bool Success;                // True if room creation succeeded
    public string Error;                 // Human-readable error message
    public StartGameResult FusionResult; // Underlying Fusion result (if any)

    public static RoomCreateResult Ok(StartGameResult fusionResult)
    {
        return new RoomCreateResult
        {
            Success = true,
            Error = null,
            FusionResult = fusionResult
        };
    }

    public static RoomCreateResult Fail(string error, StartGameResult fusionResult = default)
    {
        return new RoomCreateResult
        {
            Success = false,
            Error = error,
            FusionResult = fusionResult
        };
    }
}

public struct RoomJoinResult
{
    public bool Success;                // True if join/create succeeded
    public string Error;                 // Human-readable error message
    public StartGameResult FusionResult; // Underlying Fusion result (if any)

    public static RoomJoinResult Ok(StartGameResult fusionResult)
    {
        return new RoomJoinResult
        {
            Success = true,
            Error = null,
            FusionResult = fusionResult
        };
    }

    public static RoomJoinResult Fail(string error, StartGameResult fusionResult = default)
    {
        return new RoomJoinResult
        {
            Success = false,
            Error = error,
            FusionResult = fusionResult
        };
    }
}