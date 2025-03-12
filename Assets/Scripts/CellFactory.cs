using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CellFactory
{
    private List<CellData> _cellDatas;

    public CellFactory(CellsGridConfig cellsGridConfig)
    {
        _cellDatas = cellsGridConfig.CellDatas;
    }

    public Cell CreateCell(CellType cellType, Vector2Int index)
    {
        CellData cellVariantData = GetCellData(cellType);

        if(cellVariantData == null)
        {
            throw new ArgumentNullException("Cell data is not found");
        }

        Cell cell = GameObject.Instantiate(cellVariantData.Prefab);
        cell.Construct(cellVariantData, index);
        return cell;
    }

    private CellData GetCellData(CellType cellType)
    {
        return _cellDatas.FirstOrDefault(x => x.Type == cellType);
    }
}
