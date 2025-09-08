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

        public Cell[,] Cells => _cells;
        public Cell SourceCell => GetCell(_sourceCellPosition);
        public int Width => _config.gridSize.x;
        public int Height => _config.gridSize.y;

        public void Construct(GridConfig config, Vector2Int sourceCellPosition)
        {
            _config = config;
            _cells = new Cell[Width, Height];
            _sourceCellPosition = sourceCellPosition;
        }

        private void Update() => _currentState?.Update();

        public void SetState(IUpdatableGridState updatableGridState) => 
            _currentState = updatableGridState;

        public void AddCell(Cell cell)
        {
            if (_cells[cell.Index.x, cell.Index.y] != null)
            {
                Vector3 position = _cells[cell.Index.x, cell.Index.y].transform.position;
                cell.transform.position = position;
                RemoveCell(_cells[cell.Index.x, cell.Index.y]);
            }
            else
                cell.transform.position = GetCellPosition(cell.Index);
            
            cell.transform.parent = this.transform;
            _cells[cell.Index.x, cell.Index.y] = cell;
        }
        
        public Cell GetCell(Vector2Int index) => 
            IsIndexValid(index) ? null : _cells[index.x, index.y];

        private bool IsIndexValid(Vector2Int index) =>
            index.x >= 0 && index.x < Width && 
            index.y >= 0 && index.y < Height;

        private void RemoveCell(Cell cell) => 
            Destroy(cell.gameObject);

        private Vector2 GetCellPosition(Vector2Int index) => 
            index.x * _config.RightBasis + index.y * _config.LeftBasis;
    }
}
