﻿using Application.Persistence.Interfaces;
using Common;
using Domain.Interfaces.Entities;
using Infrastructure.Eventing.Common.Projections;
using Infrastructure.Eventing.Interfaces.Projections;

namespace Infrastructure.Hosting.Common.ApplicationServices.Eventing.Projections;

/// <summary>
///     Defines an in-process service that subscribes to one or more <see cref="IEventNotifyingStore" />
///     instances, listens to them raise change events, and relays them to
///     registered read model projections synchronously.
/// </summary>
public sealed class InProcessSynchronousProjectionRelay : EventStreamHandlerBase, IEventNotifyingStoreProjectionRelay
{
    private readonly IReadModelProjector _projector;

    public InProcessSynchronousProjectionRelay(IRecorder recorder, IEventSourcedChangeEventMigrator migrator,
        IProjectionCheckpointRepository checkpointStore, IEnumerable<IReadModelProjection> projections,
        params IEventNotifyingStore[] eventingStores) : base(recorder, eventingStores)
    {
        _projector = new ReadModelProjector(recorder, checkpointStore, migrator, projections.ToArray());
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
        {
            (_projector as IDisposable)?.Dispose();
        }
    }

    protected override async Task<Result<Error>> HandleStreamEventsAsync(string streamName,
        List<EventStreamChangeEvent> eventStream, CancellationToken cancellationToken)
    {
        return await _projector.WriteEventStreamAsync(streamName, eventStream, cancellationToken);
    }
}