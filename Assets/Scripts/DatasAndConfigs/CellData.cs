using System.Collections.Generic;
using Cells;
using UnityEngine;
using UnityEngine.Serialization;

namespace DatasAndConfigs
{
    [CreateAssetMenu(fileName = "Cells", menuName = "Cells/CellData")]
    public class CellData : ScriptableObject
    {
        public CellType Type;
        public GameObject Prefab;
        public Sprite BaseSprite;
        [FormerlySerializedAs("ConnectingSides")] public List<SideName> Sides;
        public int MinePerSecond;
    }
}
