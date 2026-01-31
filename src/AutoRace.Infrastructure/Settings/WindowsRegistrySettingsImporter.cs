using Microsoft.Win32;

namespace AutoRace.Infrastructure.Settings;

/// <summary>
/// Reads settings values from the Windows registry.
/// </summary>
public sealed class WindowsRegistrySettingsImporter
{
    private readonly RegistryKey _baseKey;

    /// <summary>
    /// Initializes a new registry importer using the provided base key.
    /// </summary>
    public WindowsRegistrySettingsImporter(RegistryKey? baseKey = null)
    {
        _baseKey = baseKey ?? Registry.CurrentUser;
    }

    /// <summary>
    /// Attempts to read a string or expanded string value from the registry.
    /// </summary>
    public string? TryReadStringValue(string subKeyPath, string valueName)
    {
        if (string.IsNullOrWhiteSpace(subKeyPath) || string.IsNullOrWhiteSpace(valueName))
        {
            return null;
        }

        try
        {
            using var key = _baseKey.OpenSubKey(subKeyPath, writable: false);
            if (key is null)
            {
                return null;
            }

            var kind = key.GetValueKind(valueName);
            if (kind is not RegistryValueKind.String and not RegistryValueKind.ExpandString)
            {
                return null;
            }

            return key.GetValue(valueName) as string;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// Attempts to read a 32-bit integer (DWORD) value from the registry.
    /// </summary>
    public int? TryReadInt32Value(string subKeyPath, string valueName)
    {
        if (string.IsNullOrWhiteSpace(subKeyPath) || string.IsNullOrWhiteSpace(valueName))
        {
            return null;
        }

        try
        {
            using var key = _baseKey.OpenSubKey(subKeyPath, writable: false);
            if (key is null)
            {
                return null;
            }

            var kind = key.GetValueKind(valueName);
            if (kind != RegistryValueKind.DWord)
            {
                return null;
            }

            var value = key.GetValue(valueName);
            return value is int intValue ? intValue : null;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
