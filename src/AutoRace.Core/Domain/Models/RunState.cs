using System;

namespace AutoRace.Core.Domain.Models;

/// <summary>
/// Represents the current state of an automation run.
/// </summary>
public sealed record RunState(
    RunStatus Status,
    DateTimeOffset StartedAt,
    DateTimeOffset? StoppedAt,
    string? LastMessage = null)
{
    public static RunState Idle(string? message = null)
        => new(RunStatus.Idle, DateTimeOffset.MinValue, null, message);

    public static RunState Running(DateTimeOffset startedAt, string? message = null)
        => new(RunStatus.Running, startedAt, null, message);

    public static RunState Stopped(DateTimeOffset startedAt, DateTimeOffset stoppedAt, string? message = null)
        => new(RunStatus.Stopped, startedAt, stoppedAt, message);
}

public enum RunStatus
{
    Idle = 0,
    Running = 1,
    Stopped = 2,
    Faulted = 3,
}
