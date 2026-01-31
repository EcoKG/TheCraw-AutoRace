namespace AutoRace.Core.Domain.Models;

/// <summary>
/// Represents a rule used to detect a condition during automation.
/// </summary>
public sealed record DetectionRule(
    string Name,
    double Threshold = 0.5);
