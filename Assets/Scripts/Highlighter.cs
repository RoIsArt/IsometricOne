using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Highlighter : IService, IDisposable
{
    private Cell _pointedCell;

    private EventBus _eventBus;

    public void Init()
    {
        _eventBus = ServiceLocator.Instance.Get<EventBus>();
        _eventBus.Subscribe<OnCellPointedEvent>(SetPointedCell);
    }

    private void SetPointedCell(OnCellPointedEvent pointedCell)
    {
        if (pointedCell.Cell == null)
        {
            ClearHighlighting();
            _pointedCell = null;
        }
        else
        {
            HighlightCell();
            _pointedCell = pointedCell.Cell;
            HighlightCell();
        }
    }


    public void HighlightCell()
    {
        if (_pointedCell != null)
        {
            bool isActive = !_pointedCell.Selected.activeSelf;
            _pointedCell.Selected.SetActive(isActive);
        }      
    }

    public void ClearHighlighting()
    {
        _pointedCell?.Selected.SetActive(false);
    }

    public void Dispose()
    {
        _eventBus.Unsubscribe<OnCellPointedEvent>(SetPointedCell);
    }
}
