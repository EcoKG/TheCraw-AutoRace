namespace AutoRace.Core.Domain.Models;

/// <summary>
/// Timing configuration for delays during automation.
/// </summary>
public sealed record TimingProfile(
    TimeSpan PreStartDelay = default,
    TimeSpan BetweenActionsDelay = default,
    TimeSpan PostStopDelay = default);
