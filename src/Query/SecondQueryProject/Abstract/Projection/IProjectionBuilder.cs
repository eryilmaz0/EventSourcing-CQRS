﻿using EventSourcing.Shared.IntegrationEvent;

namespace SecondQueryProject.Abstract.Projection;

public interface IProjectionBuilder<T> where T : ReadModel.ReadModel
{
    public Task ProjectModelAsync(IIntegrationEvent @event);
}