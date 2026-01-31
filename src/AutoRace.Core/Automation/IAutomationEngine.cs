using System.Threading.Channels;
using AutoRace.Core.Domain.Models;

namespace AutoRace.Core.Automation;

public interface IAutomationEngine
{
    ChannelReader<AutomationEvent> Events { get; }

    Task StartAsync(Profile profile, CancellationToken cancellationToken = default);

    Task StopAsync(CancellationToken cancellationToken = default);
}
