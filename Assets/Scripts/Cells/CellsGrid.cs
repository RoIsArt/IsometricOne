using System;
using DatasAndConfigs;
using UnityEngine;

namespace Cells
{
    public class CellsGrid : MonoBehaviour
    {
        private GridConfig _config;
        private Cell[,] _cells;

        public void Construct(GridConfig config)
        {
            _config = config;
            _cells = new Cell[_config.GridSize.x, _config.GridSize.y];
        }

        public Cell[,] Cells => _cells;

        public Cell this[Vector2Int index]
        {
            get
            {
                if (!CheckOutOfRangeIndex(index))
                {
                    return _cells[index.x, index.y];
                }
                else
                {
                    return null;
                }
            }
        }

        public void AddCellInGrid(Cell cell)
        {
            var index = cell.Index;
            if(_cells[index.x, index.y] != null)
                GameObject.Destroy(_cells[index.x, index.y].gameObject);
            _cells[index.x, index.y] = cell;
            
            PlacedCellOnPosition(cell.gameObject, index);
        }

        private void PlacedCellOnPosition(GameObject cell, Vector2Int index)
        {
            if (CheckOutOfRangeIndex(index))
            {
                throw new ArgumentOutOfRangeException($"{index} out of range grid size");
            }
        
            cell.transform.position = GetCellPosition(index);
        }
        private bool CheckOutOfRangeIndex(Vector2Int index)
        {
            return index.x < 0 || index.x >= _cells.GetLength(0) 
                               || index.y < 0 || index.y >= _cells.GetLength(1);
        }
        private Vector2 GetCellPosition(Vector2Int indexInArray)
        {
            return indexInArray.x * _config.RightBasis + indexInArray.y * _config.LeftBasis;
        }
    }
}
