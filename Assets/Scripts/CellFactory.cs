using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellFactory : IService
{
    private List<CellData> _cellVariants;

    public void Init()
    {
        var cellsGrid = ServiceLocator.Instance.Get<CellsGrid>();
        _cellVariants = cellsGrid.Config.CellDatas;
    }

    public Cell CreateCell(CellType cellType, Vector2Int index)
    {
        CellData cellVariantData = GetCellData(cellType);

        if(cellVariantData == null)
        {
            throw new ArgumentNullException("Cell data is not found");
        }

        Cell cell = GameObject.Instantiate(cellVariantData.Prefab);
        cell.Init(cellVariantData, index);
        return cell;
    }

    private CellData GetCellData(CellType cellType)
    {
        return _cellVariants.FirstOrDefault(x => x.Type == cellType);
    }
}
