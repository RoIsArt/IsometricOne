using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CellsGridConfig", menuName = "Cells/GridConfig")]
public class CellsGridConfig : ScriptableObject
{
    [Space]
    [Header("Grid config")]
    [SerializeField] private Vector2Int _gridSize;

    [Space]
    [Header("Cell config")]
    [SerializeField] private Vector3 _cellSize;
    [SerializeField] private Vector2 _rightBasis;
    [SerializeField] private Vector2 _leftBasis;

    [Space]
    [Header("Cell datas")]
    [SerializeField] private List<CellData> _cellDatas;

    public Vector3 CellSize { get { return _cellSize; } }
    public Vector2 RightBasis
    {
        get
        {
            return new Vector2(_cellSize.x * _rightBasis.x, -_cellSize.y * _rightBasis.y);
        }
    }
    public Vector2 LeftBasis
    {
        get
        {
            return new Vector2(-_cellSize.x * _leftBasis.x, -_cellSize.y * _leftBasis.y);
        }
    }
    public Vector2Int GridSize { get { return _gridSize; } }
    public List<CellData> CellDatas { get { return _cellDatas; } }
}
