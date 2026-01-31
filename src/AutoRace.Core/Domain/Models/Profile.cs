namespace AutoRace.Core.Domain.Models;

/// <summary>
/// A user-configurable configuration for running AutoRace.
/// Keep this small and stable; UI/services can expand it later.
/// </summary>
public sealed record Profile(
    string Name,
    ProfileSettings Settings);

public sealed record ProfileSettings
{
    /// <summary>
    /// Human-friendly identifier for which game/window this profile targets.
    /// (e.g., window title substring)
    /// </summary>
    public string TargetWindow { get; init; }

    /// <summary>
    /// Optional notes for the user.
    /// </summary>
    public string? Notes { get; init; }

    /// <summary>
    /// Optional rules used to detect automation conditions.
    /// </summary>
    public IReadOnlyList<DetectionRule> DetectionRules { get; init; }

    /// <summary>
    /// Optional key binding used to start/stop automation.
    /// </summary>
    public KeyBinding? ActivationKey { get; init; }

    /// <summary>
    /// Timing configuration.
    /// </summary>
    public TimingProfile Timing { get; init; }

    public ProfileSettings(
        string targetWindow,
        string? notes = null,
        IReadOnlyList<DetectionRule>? detectionRules = null,
        KeyBinding? activationKey = null,
        TimingProfile? timing = null)
    {
        TargetWindow = targetWindow;
        Notes = notes;
        DetectionRules = detectionRules ?? Array.Empty<DetectionRule>();
        ActivationKey = activationKey;
        Timing = timing ?? new TimingProfile();
    }
}
