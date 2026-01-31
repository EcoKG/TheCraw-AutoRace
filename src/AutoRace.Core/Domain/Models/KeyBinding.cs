namespace AutoRace.Core.Domain.Models;

/// <summary>
/// Represents a key with optional modifier keys.
/// </summary>
public sealed record KeyBinding(
    string Key,
    IReadOnlyList<string>? Modifiers = null);
