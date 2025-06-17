using System;
using DatasAndConfigs;
using GroundState;
using UnityEngine;

namespace Cells
{
    public class CellsGrid : MonoBehaviour
    {
        private GridConfig _config;
        private Cell[,] _cells;

        private IUpdatableGridState _currentState;

        public void Construct(GridConfig config)
        {
            _config = config;
            _cells = new Cell[_config.GridSize.x, _config.GridSize.y];
        }

        private void Update()
        {
            _currentState?.Update();
        }

        public Cell[,] Cells => _cells;

        public void SetState(IUpdatableGridState updatableGridState) => 
            _currentState = updatableGridState;

        public void AddCell(Cell cell)
        {
            var index = cell.Index;
            if (_cells[index.x, index.y] != null)
            {
                var position = _cells[index.x, index.y].transform.position;
                cell.transform.position = position;
                RemoveCell(_cells[index.x, index.y]);
            }
            else
                cell.transform.position = GetCellPosition(index);
            
            cell.transform.parent = this.transform;
            _cells[index.x, index.y] = cell;
        }

        private void RemoveCell(Cell cell)
        {
            Destroy(cell.gameObject);
        }

        private Vector2 GetCellPosition(Vector2Int indexInArray)
        {
            return indexInArray.x * _config.RightBasis + indexInArray.y * _config.LeftBasis;
        }

        public Cell GetCell(Vector2Int index)
        {
            return _cells[index.x, index.y];
        }
    }
}
