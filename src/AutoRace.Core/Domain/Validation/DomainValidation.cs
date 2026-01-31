using AutoRace.Core.Domain.Models;

namespace AutoRace.Core.Domain.Validation;

public static class DomainValidation
{
    private const int ProfileNameMaxLength = 64;
    private const int TargetWindowMaxLength = 128;
    private static readonly TimeSpan MaxTimingDuration = TimeSpan.FromMinutes(10);

    public static IReadOnlyList<string> Validate(Profile? profile)
    {
        var errors = new List<string>();

        if (profile is null)
        {
            errors.Add("Profile is required.");
            return errors;
        }

        if (string.IsNullOrWhiteSpace(profile.Name))
        {
            errors.Add("Profile name is required.");
        }
        else if (profile.Name.Length > ProfileNameMaxLength)
        {
            errors.Add($"Profile name must be 1-{ProfileNameMaxLength} characters.");
        }

        errors.AddRange(Validate(profile.Settings));

        return errors;
    }

    public static IReadOnlyList<string> Validate(ProfileSettings? settings)
    {
        var errors = new List<string>();

        if (settings is null)
        {
            errors.Add("Profile settings are required.");
            return errors;
        }

        if (string.IsNullOrWhiteSpace(settings.TargetWindow))
        {
            errors.Add("Target window is required.");
        }
        else if (settings.TargetWindow.Length > TargetWindowMaxLength)
        {
            errors.Add($"Target window must be 1-{TargetWindowMaxLength} characters.");
        }

        if (settings.DetectionRules is not null)
        {
            for (var i = 0; i < settings.DetectionRules.Count; i++)
            {
                var rule = settings.DetectionRules[i];
                if (rule is null)
                {
                    errors.Add($"Detection rule at index {i} is required.");
                    continue;
                }

                foreach (var error in Validate(rule))
                {
                    errors.Add($"Detection rule at index {i}: {error}");
                }
            }
        }

        if (settings.ActivationKey is not null)
        {
            foreach (var error in Validate(settings.ActivationKey))
            {
                errors.Add($"Activation key: {error}");
            }
        }

        if (settings.Timing is not null)
        {
            foreach (var error in Validate(settings.Timing))
            {
                errors.Add($"Timing: {error}");
            }
        }

        return errors;
    }

    public static IReadOnlyList<string> Validate(KeyBinding? keyBinding)
    {
        var errors = new List<string>();

        if (keyBinding is null)
        {
            errors.Add("Key binding is required.");
            return errors;
        }

        if (string.IsNullOrWhiteSpace(keyBinding.Key))
        {
            errors.Add("Key is required.");
        }

        if (keyBinding.Modifiers is not null)
        {
            for (var i = 0; i < keyBinding.Modifiers.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(keyBinding.Modifiers[i]))
                {
                    errors.Add($"Modifier at index {i} is required.");
                }
            }
        }

        return errors;
    }

    public static IReadOnlyList<string> Validate(TimingProfile? timingProfile)
    {
        var errors = new List<string>();

        if (timingProfile is null)
        {
            errors.Add("Timing profile is required.");
            return errors;
        }

        ValidateDuration(timingProfile.PreStartDelay, "Pre-start delay", errors);
        ValidateDuration(timingProfile.BetweenActionsDelay, "Between-actions delay", errors);
        ValidateDuration(timingProfile.PostStopDelay, "Post-stop delay", errors);

        return errors;
    }

    public static IReadOnlyList<string> Validate(DetectionRule? detectionRule)
    {
        var errors = new List<string>();

        if (detectionRule is null)
        {
            errors.Add("Detection rule is required.");
            return errors;
        }

        if (string.IsNullOrWhiteSpace(detectionRule.Name))
        {
            errors.Add("Name is required.");
        }

        if (detectionRule.Threshold < 0 || detectionRule.Threshold > 1)
        {
            errors.Add("Threshold must be between 0 and 1.");
        }

        return errors;
    }

    public static void ValidateOrThrow(Profile? profile)
        => ThrowIfInvalid(Validate(profile));

    public static void ValidateOrThrow(ProfileSettings? settings)
        => ThrowIfInvalid(Validate(settings));

    public static void ValidateOrThrow(KeyBinding? keyBinding)
        => ThrowIfInvalid(Validate(keyBinding));

    public static void ValidateOrThrow(TimingProfile? timingProfile)
        => ThrowIfInvalid(Validate(timingProfile));

    public static void ValidateOrThrow(DetectionRule? detectionRule)
        => ThrowIfInvalid(Validate(detectionRule));

    private static void ValidateDuration(TimeSpan duration, string name, ICollection<string> errors)
    {
        if (duration < TimeSpan.Zero)
        {
            errors.Add($"{name} must be non-negative.");
            return;
        }

        if (duration >= MaxTimingDuration)
        {
            errors.Add($"{name} must be less than {MaxTimingDuration.TotalMinutes:0} minutes.");
        }
    }

    private static void ThrowIfInvalid(IReadOnlyList<string> errors)
    {
        if (errors.Count > 0)
        {
            throw new DomainValidationException(errors);
        }
    }
}
