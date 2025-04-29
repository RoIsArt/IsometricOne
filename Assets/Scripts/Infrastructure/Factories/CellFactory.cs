using System.Collections.Generic;
using UnityEngine;

public class CellFactory
{
    private CellsDataConfig _cellsDataConfig;

    public CellFactory(CellsDataConfig cellsDataConfig)
    {
        _cellsDataConfig = cellsDataConfig;
    }

    public GameObject CreateCell(CellData cellData, Vector2Int index)
    {
        var data = _cellsDataConfig.CellDatas[cellData.Type];
        var cell = GameObject.Instantiate(data.Prefab);
        cell.GetComponent<Cell>().Construct(data, index);
        return cell;
    }
}
