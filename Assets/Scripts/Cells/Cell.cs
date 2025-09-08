using DatasAndConfigs;
using GameEvents;
using GamePlayServices;
using UnityEngine;

namespace Cells
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Selector))]
    [RequireComponent(typeof(CellMouseObserver))]

    public class Cell : MonoBehaviour
    {
        private CellData _data;
        private SpriteRenderer _spriteRenderer;
        private CellMouseObserver _cellMouseObserver;

        public void Construct(CellData data, Vector2Int index, IEventBus eventBus)
        {
            _data = data;
            Index = index;
            
            _spriteRenderer = GetComponent<SpriteRenderer>();
            Selector = GetComponent<Selector>();
            _cellMouseObserver = GetComponent<CellMouseObserver>();
            
            _cellMouseObserver.Construct(eventBus, this);
            Connecter = new Connecter(data, this);
            
            SetBaseSprite();
        }

        public Selector Selector { get; private set; }
        public Vector2Int Index { get; private set; }
        public Connecter Connecter { get; private set; }
        public int MinePerSecond => _data.minePerSecond;
        public CellType Type => _data.type;


        public void SetSprite(Sprite sprite) =>
            _spriteRenderer.sprite = sprite;

        public void SetBaseSprite() =>
            SetSprite(_data.baseSprite);
    }
}
