using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellFactory
{
    private List<CellData> _cellVariants;

    public CellFactory(List<CellData> cellVariants)
    {
        _cellVariants = cellVariants;
    }

    public Cell CreateCell(CellType cellType)
    {
        var cellVariantData = GetCellData(cellType);

        if(cellVariantData == null)
        {
            throw new ArgumentNullException("Cell data is not found");
        }

        var cell = GameObject.Instantiate(cellVariantData.Prefab);
        cell.Init(cellVariantData);
        return cell;
    }

    private CellData GetCellData(CellType cellType)
    {
        return _cellVariants.FirstOrDefault(x => x.Type == cellType);
    }
}
