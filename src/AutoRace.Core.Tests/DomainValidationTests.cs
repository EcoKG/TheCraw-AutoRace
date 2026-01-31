using AutoRace.Core.Domain.Models;
using AutoRace.Core.Domain.Validation;

namespace AutoRace.Core.Tests;

public sealed class DomainValidationTests
{
    [Fact]
    public void Profile_Name_Is_Required_And_Bounded()
    {
        var settings = new ProfileSettings("Game Window");

        var emptyName = new Profile("", settings);
        Assert.NotEmpty(DomainValidation.Validate(emptyName));

        var whitespaceName = new Profile("   ", settings);
        Assert.NotEmpty(DomainValidation.Validate(whitespaceName));

        var tooLongName = new Profile(new string('a', 65), settings);
        Assert.NotEmpty(DomainValidation.Validate(tooLongName));

        var validName = new Profile(new string('a', 64), settings);
        Assert.Empty(DomainValidation.Validate(validName));
    }

    [Fact]
    public void ProfileSettings_TargetWindow_Is_Required_And_Bounded()
    {
        var empty = new ProfileSettings("");
        Assert.NotEmpty(DomainValidation.Validate(empty));

        var whitespace = new ProfileSettings("   ");
        Assert.NotEmpty(DomainValidation.Validate(whitespace));

        var tooLong = new ProfileSettings(new string('b', 129));
        Assert.NotEmpty(DomainValidation.Validate(tooLong));

        var valid = new ProfileSettings(new string('b', 128));
        Assert.Empty(DomainValidation.Validate(valid));
    }

    [Fact]
    public void KeyBinding_Requires_Key_And_Modifiers_Are_Not_Whitespace()
    {
        var emptyKey = new KeyBinding("");
        Assert.NotEmpty(DomainValidation.Validate(emptyKey));

        var whitespaceKey = new KeyBinding("   ");
        Assert.NotEmpty(DomainValidation.Validate(whitespaceKey));

        var invalidModifier = new KeyBinding("K", new[] { " " });
        Assert.NotEmpty(DomainValidation.Validate(invalidModifier));

        var valid = new KeyBinding("K", new[] { "Ctrl", "Shift" });
        Assert.Empty(DomainValidation.Validate(valid));
    }

    [Fact]
    public void TimingProfile_Durations_Must_Be_NonNegative_And_Reasonable()
    {
        var negative = new TimingProfile(TimeSpan.FromSeconds(-1));
        Assert.NotEmpty(DomainValidation.Validate(negative));

        var tooLong = new TimingProfile(TimeSpan.FromMinutes(10));
        Assert.NotEmpty(DomainValidation.Validate(tooLong));

        var valid = new TimingProfile(TimeSpan.FromMinutes(9));
        Assert.Empty(DomainValidation.Validate(valid));
    }

    [Fact]
    public void DetectionRule_Requires_Name_And_Threshold_Between_Zero_And_One()
    {
        var missingName = new DetectionRule("", 0.5);
        Assert.NotEmpty(DomainValidation.Validate(missingName));

        var low = new DetectionRule("Rule", -0.1);
        Assert.NotEmpty(DomainValidation.Validate(low));

        var high = new DetectionRule("Rule", 1.1);
        Assert.NotEmpty(DomainValidation.Validate(high));

        var validMin = new DetectionRule("Rule", 0);
        Assert.Empty(DomainValidation.Validate(validMin));

        var validMax = new DetectionRule("Rule", 1);
        Assert.Empty(DomainValidation.Validate(validMax));
    }

    [Fact]
    public void ValidateOrThrow_Throws_With_Errors()
    {
        var profile = new Profile("", new ProfileSettings("Game Window"));
        Assert.Throws<DomainValidationException>(() => DomainValidation.ValidateOrThrow(profile));
    }
}
