namespace AutoRace.Core.Domain.Models;

/// <summary>
/// A user-configurable configuration for running AutoRace.
/// Keep this small and stable; UI/services can expand it later.
/// </summary>
public sealed record Profile(
    string Name,
    ProfileSettings Settings);

public sealed record ProfileSettings(
    /// <summary>
    /// Human-friendly identifier for which game/window this profile targets.
    /// (e.g., window title substring)
    /// </summary>
    string TargetWindow,

    /// <summary>
    /// Optional notes for the user.
    /// </summary>
    string? Notes = null);
