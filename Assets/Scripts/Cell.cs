using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Cell : MonoBehaviour
{
    [SerializeField] private GameObject _selected;

    private CellData _data;
    private Vector2Int _index;
    private SpriteRenderer _sprite;

    public void Construct(CellData data, Vector2Int index)
    {
        _data = data;
        _index = index;
        _sprite = GetComponent<SpriteRenderer>();
    }

    public Vector2Int Index { get { return _index; } }
    public GameObject Selected { get { return _selected; } }
    public CellData Data { get { return _data; } }

    public void SetSprite(Sprite sprite) 
    { 
        _sprite.sprite = sprite; 
    }
}

