using System.Collections;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CellsGridConfig", menuName = "Cells/GridConfig")]
public class GridConfig : ScriptableObject
{
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private Vector2Int _sourcePosition;

    public Vector2Int GridSize { get { return _gridSize; } }
    public Vector2Int SourcePosition { get { return _sourcePosition; } }
}