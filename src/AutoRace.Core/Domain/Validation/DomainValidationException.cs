namespace AutoRace.Core.Domain.Validation;

public sealed class DomainValidationException : Exception
{
    public DomainValidationException(IReadOnlyList<string> errors)
        : base("Domain validation failed.")
    {
        Errors = errors;
    }

    public IReadOnlyList<string> Errors { get; }
}
