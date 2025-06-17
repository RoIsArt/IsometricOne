using System;
using Cells;
using GameEvents;
using Infrastructure.Services;

namespace GamePlayServices
{
    public class Pointer : IPointer, IDisposable
    {
        private readonly IEventBus _eventBus;

        private Cell _pointedCell;

        [Inject]
        public Pointer(IEventBus eventBus)
        {
            _eventBus = eventBus;

            _eventBus.Subscribe<OnCellMouseEnterEvent>(PointCell);
            _eventBus.Subscribe<OnCellMouseExitEvent>(ResetPoint);
        }

        public Cell GetPointedCell() => 
            _pointedCell;
        
        private void PointCell(OnCellMouseEnterEvent onCellMouseEnterEvent)
        {
            _pointedCell = onCellMouseEnterEvent.Cell;
        }

        private void ResetPoint(OnCellMouseExitEvent onCellMouseExitEvent)
        {
            _pointedCell = null;
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<OnCellMouseEnterEvent>(PointCell);
            _eventBus.Unsubscribe<OnCellMouseExitEvent>(ResetPoint);
        }
    }
}