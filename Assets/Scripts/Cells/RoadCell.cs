using DatasAndConfigs;
using GameEvents;
using UnityEngine;

namespace Cells
{
    public class RoadCell : ConnectingCell
    {
        private int _minePerSecond;

        public void Construct(RoadCellData data, Vector2Int index, IEventBus eventBus)
        {
            base.Construct(data, index, eventBus);
            _minePerSecond = data.MinePerSecond;
        }
    }
}
