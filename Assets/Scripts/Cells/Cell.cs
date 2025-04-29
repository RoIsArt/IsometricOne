using System;
using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Cell : MonoBehaviour
{
    [SerializeField] protected GameObject _selected;

    protected CellData _data;
    protected Vector2Int _index;
    protected SpriteRenderer _spriteRenderer;

    public void Construct(CellData data, Vector2Int index)
    {
        _data = data;
        _index = index;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public Vector2Int Index { get { return _index; } }
    public GameObject Selected { get { return _selected; } }
    public CellData Data { get { return _data; } }

    public void SetSprite(Sprite sprite) =>
        _spriteRenderer.sprite = sprite;

    public void SetBaseSprite() =>
        _spriteRenderer.sprite = _data.BaseSprite;
}
