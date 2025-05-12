using DatasAndConfigs;
using GameEvents;
using UnityEngine;

namespace Cells
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(SelectorChanger))]

    public abstract class Cell : MonoBehaviour
    {
        private CellData _data;
        private Vector2Int _cellIndex;
        private SpriteRenderer _spriteRenderer;
        private IEventBus _eventBus;
        private SelectorChanger _selector;

        private bool _isPointed;
        
        public void Construct(CellData data, Vector2Int index, IEventBus eventBus)
        {
            _data = data;
            _cellIndex = index;
            _eventBus = eventBus;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _selector = GetComponent<SelectorChanger>();
        }
        
        public CellType Type => _data.Type;
        public Vector2Int Index => _cellIndex;
        public SelectorChanger Selector => _selector;

        public void SetSprite(Sprite sprite) =>
            _spriteRenderer.sprite = sprite;

        public void SetBaseSprite() =>
            SetSprite(_data.BaseSprite);

        public void Point()
        {
            if (!_isPointed)
            {
                _eventBus.Invoke((new OnCellMouseEnterEvent(this)));
                _isPointed = true;
            }
            else
            {
                _eventBus.Invoke((new OnCellMouseExitEvent(this)));
                _isPointed = false;
            }
        }
    }
}
