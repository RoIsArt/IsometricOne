using UnityEngine;

[CreateAssetMenu(fileName = "CellData", menuName = "Cells/Data")]
public abstract class CellData : ScriptableObject
{
    public CellType Type;
    public GameObject Prefab;
    public Sprite BaseSprite;
}
