using System;
using Cells;
using DatasAndConfigs;
using GameEvents;
using Infrastructure.Factories;
using Infrastructure.Services;
using UnityEngine;

namespace GamePlayServices
{
    public class Builder : IBuilder, IDisposable
    {
        private readonly ICellFactory _cellFactory;
        private readonly IEventBus _eventBus;
        private readonly IStaticDataService _staticData;

        private CellData _buildData;
        private Cell _pointedCell;
    
        [Inject]
        public Builder(ICellFactory cellFactory, IEventBus eventBus, IStaticDataService staticData)
        {
            _cellFactory = cellFactory;
            _eventBus = eventBus;
            _staticData = staticData;

            _eventBus.Subscribe<OnStartBuildingCellEvent>(PrepareForBuilding);
            _eventBus.Subscribe<OnCellBuildedEvent>(CompleteBulding);
        }

        public Cell Build()
        { 
            var cell = _cellFactory.Create(_buildData.Type, _pointedCell.Index);
            _eventBus.Invoke(new OnCellBuildedEvent(cell));
            return cell;
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<OnStartBuildingCellEvent>(PrepareForBuilding);
            _eventBus.Unsubscribe<OnCellMouseEnterEvent>(SetPointedCell);
            _eventBus.Unsubscribe<OnCellBuildedEvent>(CompleteBulding);
        }

        private void PrepareForBuilding(OnStartBuildingCellEvent onStartBuildingCellEvent)
        {
            _buildData = _staticData.ForCell(onStartBuildingCellEvent.CellType);
        }

        private void SetPointedCell(OnCellMouseEnterEvent onCellMouseEnterEvent)
        {
            _pointedCell = onCellMouseEnterEvent.Cell;
        }

        private void CompleteBulding(OnCellBuildedEvent onCellBuildedEvent)
        {
            _buildData = null;
            _eventBus.Invoke(new OnCellMouseEnterEvent(null));
        }
    }
}