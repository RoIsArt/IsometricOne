using UnityEngine;

[CreateAssetMenu(fileName = "CellSizeConfig", menuName = "Cells/GridConfig")]
public class CellSizeConfig : ScriptableObject 
{
    [SerializeField] private Vector2Int _cellSize;
    public int CellWidth { get { return _cellSize.x / 2; } }
    public int CellHeight { get { return _cellSize.y / 2; } }
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
}

