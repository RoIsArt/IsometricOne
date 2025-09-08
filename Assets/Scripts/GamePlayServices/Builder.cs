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
        private readonly IPointer _pointer;
        
        private CellData _buildData;
        
        private Cell _pointedCell;
        private Cell _planedCell;
        

        [Inject]
        public Builder(ICellFactory cellFactory, 
            IEventBus eventBus, 
            IStaticDataService staticData, 
            IPointer pointer)
        {
            _cellFactory = cellFactory;
            _eventBus = eventBus;
            _staticData = staticData;
            _pointer = pointer;

            _eventBus.Subscribe<OnStartBuildingCellEvent>(PrepareForBuilding);
        }

        public void Plan()
        {
            Cell pointedCell = _pointer.GetPointedCell();
            
            if (pointedCell)
            {
                if (_planedCell != pointedCell)
                    PlanCell(pointedCell);
            }
            else if(_planedCell)
                ResetPlan();
        }

        public void Build()
        {
            if(_planedCell == null) return;
            
            _cellFactory.Create(_buildData.type, _planedCell.Index); 
            _eventBus.Invoke(new OnCellBuildedEvent(_buildData.type));
            CompleteBulding();
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<OnStartBuildingCellEvent>(PrepareForBuilding);
        }

        private void PrepareForBuilding(OnStartBuildingCellEvent onStartBuildingCellEvent) => 
            _buildData = _staticData.ForCell(onStartBuildingCellEvent.CellType);

        private void CompleteBulding()
        {
            _buildData = null;
            _planedCell = null;
        }

        private void PlanCell(Cell pointedCell)
        {
            _planedCell?.SetBaseSprite();
            _planedCell = pointedCell.Type == CellType.Empty ? pointedCell : null;
            _planedCell?.SetSprite(_buildData.baseSprite);
        }

        private void ResetPlan()
        {
            _planedCell?.SetBaseSprite();
            _planedCell = null;
        }
    }
}