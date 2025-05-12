using UnityEngine;

namespace DatasAndConfigs
{
    [CreateAssetMenu(fileName = "CellsGridConfig", menuName = "Cells/GridConfig")]
    public class GridConfig : ScriptableObject
    {
        public Vector2Int GridSize;
        public Vector2Int SourcePosition;
        public Vector2Int CellSize;

        public Vector2 RightBasis => new(CellWidth, CellHeight);
        public Vector2 LeftBasis => new(-CellWidth, CellHeight);
        private int CellWidth => CellSize.x / 2;
        private int CellHeight => CellSize.y / 2;
    }
}