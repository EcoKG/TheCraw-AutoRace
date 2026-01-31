using System.Text.Json;
using AutoRace.Core.Settings;

namespace AutoRace.Infrastructure.Settings;

/// <summary>
/// Persists settings to a JSON file on disk.
/// </summary>
public sealed class JsonFileSettingsStore<T> : ISettingsStore<T>
{
    private readonly JsonSerializerOptions _serializerOptions;

    /// <summary>
    /// Initializes a new instance of the JSON settings store.
    /// </summary>
    public JsonFileSettingsStore(string path, JsonSerializerOptions? serializerOptions = null)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Settings path must be provided.", nameof(path));
        }

        Path = path;
        _serializerOptions = serializerOptions ?? new JsonSerializerOptions { WriteIndented = true };
    }

    /// <inheritdoc />
    public string Path { get; }

    /// <inheritdoc />
    public async Task<T?> LoadAsync(CancellationToken ct = default)
    {
        if (!File.Exists(Path))
        {
            return default;
        }

        await using var stream = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.Read);
        return await JsonSerializer.DeserializeAsync<T>(stream, _serializerOptions, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task SaveAsync(T value, CancellationToken ct = default)
    {
        var directory = System.IO.Path.GetDirectoryName(Path);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        await using var stream = new FileStream(Path, FileMode.Create, FileAccess.Write, FileShare.None);
        await JsonSerializer.SerializeAsync(stream, value, _serializerOptions, ct).ConfigureAwait(false);
        await stream.FlushAsync(ct).ConfigureAwait(false);
    }
}
