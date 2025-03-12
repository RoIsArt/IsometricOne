using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CellData", menuName = "Cells/Data")]
public class CellData : ScriptableObject
{
    [SerializeField] private CellType _cellType;
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private Sprite _cellSprite;

    public CellType Type { get { return _cellType; } }
    public Cell Prefab { get { return _cellPrefab; } }
    public Sprite Sprite { get { return _cellSprite; } }
}
