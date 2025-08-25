using System.Collections.Generic;
using Animation;
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
        public List<SideName> Sides;
        public int MinePerSecond;

        public List<SpriteAnimationClip> Clips = new List<SpriteAnimationClip>();
    }
}
