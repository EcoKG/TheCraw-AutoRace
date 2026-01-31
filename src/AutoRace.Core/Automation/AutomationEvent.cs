namespace AutoRace.Core.Automation;

public enum AutomationEventKind
{
    Started,
    Progress,
    Stopped,
    Faulted,
}

public sealed record AutomationEvent(
    AutomationEventKind Kind,
    DateTimeOffset At,
    string? Message = null,
    int? Progress = null,
    Exception? Exception = null);
