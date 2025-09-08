using System;
using Cells;
using DatasAndConfigs;
using GameEvents;
using GroundState;
using Infrastructure.Services;

namespace GamePlayServices
{
    public class Highlighter : IHighlighter, IDisposable
    {
        private readonly IEventBus _eventBus;
        private readonly IPointer _pointer;
        private Cell[,] _cells;

        private Cell _highlightedCell;
        private bool _isBuilding;

        [Inject]
        public Highlighter(IEventBus eventBus, IPointer pointer)
        {
            _eventBus = eventBus;
            _pointer = pointer;
            
            _eventBus.Subscribe<OnCellsGridCreatedEvent>(Initialize);
        }
        
        public void Highlight()
        {
            if(_isBuilding) return;
            
            Cell pointedCell = _pointer.GetPointedCell();
            
            if (pointedCell)
            {
                if (_highlightedCell != pointedCell)
                    MoveSelector(pointedCell);
            }
            else if(_highlightedCell)
                ResetSelector();
        }
        
        public void StartBuild()
        {
            _isBuilding = true;
            HighlightEmptyCells();
        }

        public void EndBuild()
        {
            ClearHighlights();
            _isBuilding = false;
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<OnCellsGridCreatedEvent>(Initialize);
        }
        
        private void Initialize(OnCellsGridCreatedEvent onCellsGridCreatedEvent)
        {
            _cells = onCellsGridCreatedEvent.CellsGrid.Cells;
        }

        private void MoveSelector(Cell cell)
        {
            _highlightedCell?.Selector.Hide();
            _highlightedCell = cell;
            _highlightedCell.Selector.Show();
        }

        private void ResetSelector()
        {
            _highlightedCell.Selector.Hide();
            _highlightedCell = null;
        }
        
        private void HighlightEmptyCells()
        {
            foreach (Cell cell in _cells)
            {
                if(cell.Type == CellType.Empty)
                    cell.Selector.Show();
            }
        }

        private void ClearHighlights()
        {
            foreach (Cell cell in _cells)
            {
                cell.Selector.Hide();
            }
        }
    }
}
