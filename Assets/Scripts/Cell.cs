using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private GameObject _selected;

    private CellType _type;

    private bool _isHighlighted;

    public void Init(CellData data)
    {
        _type = data.Type;
        _isHighlighted = false;
    }

    public void Highlight()
    {
        _isHighlighted = _isHighlighted ? false : true;
        _selected.SetActive(_isHighlighted);
    }

    private void OnMouseEnter()
    {
        Highlight();
    }

    private void OnMouseExit()
    {
        Highlight();
    }
}

