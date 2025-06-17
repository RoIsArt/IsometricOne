using System.Collections.Generic;
using System.Linq;
using DatasAndConfigs;
using GameEvents;
using GamePlayServices;
using UnityEngine;

namespace Cells
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(SelectorChanger))]

    public class Cell : MonoBehaviour
    {
        public IEventBus EventBus;
        
        private CellData _data;
        private SpriteRenderer _spriteRenderer;
        private SelectorChanger _selector;
        private CellMouseObserver _cellMouseObserver;

        public void Construct(CellData data, Vector2Int index, IEventBus eventBus)
        {
            _data = data;
            Index = index;
            EventBus = eventBus;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _selector = GetComponent<SelectorChanger>();
            _cellMouseObserver = GetComponent<CellMouseObserver>();
            _cellMouseObserver.Construct(eventBus, this);
        }

        public Vector2Int Index { get; private set; }
        public int MinePerSecond => _data.MinePerSecond;
        public CellType Type => _data.Type;

        public void SetSprite(Sprite sprite) =>
            _spriteRenderer.sprite = sprite;

        public void SetBaseSprite() =>
            SetSprite(_data.BaseSprite);

        public void SetSelector(bool activity) =>
            _selector.SetActive(activity);
        

        public bool ContainSide(SideName sideName) => 
            _data.ConnectingSides.Count > 0 && _data.ConnectingSides.Any(connectingSide => connectingSide.SideName == sideName);

        public ConnectingSide GetConnectingSide(SideName fromSideName) => 
            _data.ConnectingSides.FirstOrDefault(x => x.SideName == fromSideName);

        public ConnectingSide GetUnconnetSide() => 
            _data.ConnectingSides.FirstOrDefault((x => !x.IsConnected));
    }
}
