using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private GameObject _selected;

    private CellType _type;
    private Vector2Int _index;

    public Vector2Int Index { get { return _index; } }
    public GameObject Selected { get { return _selected; } }
    public CellType Type { get { return _type; } }
    public void Init(CellData data, Vector2Int index)
    {
        _type = data.Type;
        _index = index;
    }
}

