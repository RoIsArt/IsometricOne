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
        private readonly IStaticDataService _staticData;
        private readonly IEventBus _eventBus;
        private Cell[,] _cells;

        private Cell _pointedCell;
        private CellData _buildData;

        [Inject]
        public Highlighter(IStaticDataService staticData, IEventBus eventBus)
        {
            _staticData = staticData;
            _eventBus = eventBus;
            
            _eventBus.Subscribe<OnCellMouseEnterEvent>(Highlight);
            _eventBus.Subscribe<OnCellMouseExitEvent>(RemoveHighlight);
            _eventBus.Subscribe<OnCellsGridCreatedEvent>(Initialize);
            _eventBus.Subscribe<OnStartBuildingCellEvent>(PrepareForBuild);
        }
        
        public void Highlight(OnCellMouseEnterEvent onCellMouseEnterEvent)
        {
            _pointedCell = onCellMouseEnterEvent.Cell;

            if (_buildData != null)
            {
                if(_pointedCell?.Type == CellType.Empty)
                    _pointedCell?.SetSprite(_buildData.BaseSprite);
            }
            else 
                _pointedCell?.SetSelector(true);
        }
        
        public void RemoveHighlight(OnCellMouseExitEvent onCellMouseExitEvent)
        {
            if (_buildData != null)
                _pointedCell?.SetBaseSprite();
            else if (_pointedCell == onCellMouseExitEvent.Cell)
                onCellMouseExitEvent.Cell.SetSelector(false);
            
            _pointedCell = null;
        }
        
        public void StartBuild()
        {
            HighlightEmptyCells();
        }

        public void EndBuild()
        {
            ClearHighlights();
            ClearCellForBuild();
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<OnCellMouseEnterEvent>(Highlight);
            _eventBus.Unsubscribe<OnCellMouseExitEvent>(RemoveHighlight);
        }
        
        private void Initialize(OnCellsGridCreatedEvent onCellsGridCreatedEvent)
        {
            _cells = onCellsGridCreatedEvent.CellsGrid.Cells;
        }
        
        private void HighlightEmptyCells()
        {
            foreach (Cell cell in _cells)
            {
                if(cell.Type == CellType.Empty)
                    cell.SetSelector(true);
            }
        }

        private void ClearHighlights()
        {
            foreach (Cell cell in _cells)
            {
                cell.SetSelector(false);
            }
        }
        
        private void PrepareForBuild(OnStartBuildingCellEvent onStartBuildingCellEvent)
        {
            _buildData = _staticData.ForCell(onStartBuildingCellEvent.CellType);
        }
        
        private void ClearCellForBuild()
        {
            _buildData = null;
        }
    }
}
