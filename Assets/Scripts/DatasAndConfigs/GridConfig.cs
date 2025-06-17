using UnityEngine;

namespace DatasAndConfigs
{
    [CreateAssetMenu(fileName = "CellsGridConfig", menuName = "Cells/GridConfig")]
    public class GridConfig : ScriptableObject
    {
        public Vector2Int GridSize;
        public Vector2Int CellSize;
        public int ShiftX;
        public int ShiftY;

        public Vector2 RightBasis => new(CellWidth + ShiftX, CellHeight + ShiftY);
        public Vector2 LeftBasis => new(-CellWidth - ShiftX, CellHeight + ShiftY);
        public Vector2 Shift => new(0, -(float)((GridSize.x - 1) / 2.0 * (CellSize.y + ShiftY * 2)));
        private int CellWidth => CellSize.x / 2;
        private int CellHeight => CellSize.y / 2;
    }
}