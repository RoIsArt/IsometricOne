using GameEvents;
using Infrastructure.Services;

public interface IRouteConstructor : IService
{
    public void CheckConnection(OnCellBuildedEvent onCellBuildedEvent);
}