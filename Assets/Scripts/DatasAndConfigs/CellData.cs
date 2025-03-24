using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CellData", menuName = "Cells/Data")]
public class CellData : ScriptableObject
{
    [SerializeField] protected CellType _cellType;
    [SerializeField] protected Cell _cellPrefab;
    [SerializeField] protected Sprite _cellSprite;

    public CellType Type { get { return _cellType; } }
    public Cell Prefab { get { return _cellPrefab; } }
    public Sprite Sprite { get { return _cellSprite; } }
}
