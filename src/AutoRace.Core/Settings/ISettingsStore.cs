namespace AutoRace.Core.Settings;

/// <summary>
/// Defines asynchronous load/save operations for persisted settings.
/// </summary>
public interface ISettingsStore<T>
{
    /// <summary>
    /// Gets the backing storage path.
    /// </summary>
    string Path { get; }

    /// <summary>
    /// Loads the settings value if present; returns null when missing.
    /// </summary>
    Task<T?> LoadAsync(CancellationToken ct = default);

    /// <summary>
    /// Saves the provided settings value to storage.
    /// </summary>
    Task SaveAsync(T value, CancellationToken ct = default);
}
