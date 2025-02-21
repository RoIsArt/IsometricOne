using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CellsGridConfig", menuName = "Cells/GridConfig")]
public class CellsGridConfig : ScriptableObject
{
    [Space]
    [Header("Grid config")]
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private Vector2Int _cellSize;

    [Space]
    [Header("Cell datas")]
    [SerializeField] private List<CellData> _cellDatas;

    public Vector2Int CellSize { get { return _cellSize; } }
    public int CellWidth { get { return _cellSize.x/2; } }
    public int CellHeight { get { return _cellSize.y/2; } }
    public Vector2 RightBasis
    {
        get
        {
            return new Vector2(CellWidth, CellHeight);
        }
    }
    public Vector2 LeftBasis
    {
        get
        {
            return new Vector2(-CellWidth, CellHeight);
        }
    }
    public Vector2Int GridSize { get { return _gridSize; } }
    public List<CellData> CellDatas { get { return _cellDatas; } }
}
