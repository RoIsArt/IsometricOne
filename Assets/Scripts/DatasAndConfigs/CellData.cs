using Cells;
using UnityEngine;

namespace DatasAndConfigs
{
    [CreateAssetMenu(fileName = "Cells", menuName = "Cells/CellData")]
    public class CellData : ScriptableObject
    {
        public CellType type;
        public GameObject prefab;
        public Sprite baseSprite;
        public int minePerSecond;
    }
}
