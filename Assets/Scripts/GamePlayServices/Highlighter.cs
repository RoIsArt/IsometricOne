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
        private readonly IStaticDataService _staticData;
        private Cell[,] _cells;

        private Cell _pointedCell;
        private CellData _buildData;

        [Inject]
        public Highlighter(IEventBus eventBus, IStaticDataService staticData)
        {
            _eventBus = eventBus;
            _staticData = staticData;

            _eventBus.Subscribe<OnGridInitializedEvent>(AssignGrid);
            _eventBus.Subscribe<OnCellMouseEnterEvent>(Highlight);
            _eventBus.Subscribe<OnCellMouseExitEvent>(RemoveHighlight);
            _eventBus.Subscribe<OnStartBuildingCellEvent>(SetCellForBuild);
        }

        public void ClearCellForBuild()
        {
            _buildData = null;
        }


        public void Highlight(OnCellMouseEnterEvent onCellMouseEnterEvent)
        {
            _pointedCell = onCellMouseEnterEvent.Cell;

            if (_buildData != null)
            {
                if(_pointedCell.Type == CellType.Empty)
                    _pointedCell.SetSprite(_buildData.BaseSprite);
            }
            else 
                _pointedCell.Selector.Activate();
            
        }
        
        public void RemoveHighlight(OnCellMouseExitEvent onCellMouseExitEvent)
        {
            if (_buildData != null)
                _pointedCell.SetBaseSprite();
            else if (_pointedCell == onCellMouseExitEvent.Cell) 
                onCellMouseExitEvent.Cell.Selector.Deactivate();
            
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
            _eventBus.Unsubscribe<OnGridInitializedEvent>(AssignGrid);
            _eventBus.Unsubscribe<OnCellMouseEnterEvent>(Highlight);
            _eventBus.Unsubscribe<OnCellMouseExitEvent>(RemoveHighlight);
            _eventBus.Unsubscribe<OnStartBuildingCellEvent>(SetCellForBuild);
        }
        
        private void HighlightEmptyCells()
        {
            foreach (Cell cell in _cells)
            {
                if(cell.Type == CellType.Empty)
                    cell.Selector.Activate();
            }
        }

        private void ClearHighlights()
        {
            foreach (Cell cell in _cells)
            {
                cell.Selector.Deactivate();
            }
        }
        
        private void SetCellForBuild(OnStartBuildingCellEvent onStartBuildingCellEvent)
        {
            _buildData = _staticData.ForCell(onStartBuildingCellEvent.CellType);
        }
        
        private void AssignGrid(OnGridInitializedEvent onGridInitializedEvent)
        {
            _cells = onGridInitializedEvent.CellsGrid.Cells;
        }
    }
}
