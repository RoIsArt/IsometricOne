using UnityEngine;
using UnityEngine.Serialization;

namespace DatasAndConfigs
{
    [CreateAssetMenu(fileName = "CellsGridConfig", menuName = "Cells/GridConfig")]
    public class GridConfig : ScriptableObject
    {
        [FormerlySerializedAs("GridSize")] public Vector2Int gridSize;
        [FormerlySerializedAs("CellSize")] public Vector2Int cellSize;
        [FormerlySerializedAs("ShiftX")] public int shiftX;
        [FormerlySerializedAs("ShiftY")] public int shiftY;

        public Vector2 RightBasis => new(CellWidth + shiftX, CellHeight + shiftY);
        public Vector2 LeftBasis => new(-CellWidth - shiftX, CellHeight + shiftY);
        public Vector2 Shift => new(0, -(float)((gridSize.x - 1) / 2.0 * (cellSize.y + shiftY * 2)));
        private int CellWidth => cellSize.x / 2;
        private int CellHeight => cellSize.y / 2;
    }
}