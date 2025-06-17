using System.Collections.Generic;
using Cells;
using UnityEngine;

namespace DatasAndConfigs
{
    [CreateAssetMenu(fileName = "Cells", menuName = "Cells/CellData")]
    public class CellData : ScriptableObject
    {
        public CellType Type;
        public GameObject Prefab;
        public Sprite BaseSprite;
        public List<ConnectingSide> ConnectingSides;
        public int MinePerSecond;
    }
}
