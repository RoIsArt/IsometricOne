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
        private Vector2Int _sourceCellPosition;
        private IUpdatableGridState _currentState;

        public Cell SourceCell =>
            GetCell(_sourceCellPosition);

        public void Construct(GridConfig config, Vector2Int sourceCellPosition)
        {
            _config = config;
            _cells = new Cell[_config.GridSize.x, _config.GridSize.y];
            _sourceCellPosition = sourceCellPosition;
        }

        private void Update()
        {
            _currentState?.Update();
        }
        
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
        public Cell GetCell(Vector2Int index)
        {
            if (index.x < 0 || index.x > _cells.GetLength(0) ||
                index.y < 0 || index.y > _cells.GetLength(1))
                return null;
            
            return _cells[index.x, index.y];
        }

        public Cell[,] GetAllCell()
        {
            return _cells;
        }

        private void RemoveCell(Cell cell)
        {
            Destroy(cell.gameObject);
        }

        private Vector2 GetCellPosition(Vector2Int index)
        {
            return index.x * _config.RightBasis + index.y * _config.LeftBasis;
        }

    }
}
