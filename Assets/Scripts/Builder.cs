using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : IService
{
    private CellsGrid _cellsGrid;
    private CellFactory _cellFactory;

    private Cell _cellForBuild;
    public void Init()
    {
        _cellsGrid = ServiceLocator.Instance.Get<CellsGrid>();
        _cellFactory = ServiceLocator.Instance.Get<CellFactory>();
    }

    public void PrerpareForBuilding()
    {

    }

    public void BuildCell()
    {

    }

}
