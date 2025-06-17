using Cells;
using GameEvents;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class CellFactory : ICellFactory
    {
        private readonly IEventBus _eventBus;
        private readonly IStaticDataService _staticDataService;
        
        [Inject]
        public CellFactory(IStaticDataService staticDataService, IEventBus eventBus)
        {
            _staticDataService = staticDataService;
            _eventBus = eventBus;
        }
        
        public GameObject Create(CellType type, Vector2Int index)
        {
            var data = _staticDataService.ForCell(type);
            var cell = GameObject.Instantiate(data.Prefab);
            cell.GetComponent<Cell>().Construct(data, index, _eventBus);
            return cell;
        }
    }
}
