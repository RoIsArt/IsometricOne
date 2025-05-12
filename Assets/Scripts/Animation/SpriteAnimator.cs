using System;
using GameEvents;
using UnityEngine;

namespace Animation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimator : MonoBehaviour, IDisposable
    {
        private const string IDLE_KEY = "Idle";
        private const string CONNECT_KEY = "Connect";

        [SerializeField] private AnimationClip[] _clips;
        [SerializeField] private int _framePerSecond;

        private SpriteRenderer _spriteRenderer;
        private AnimationClip _currentClip;
        private int _currentSpriteIndex;
        private float _frequence;
        private float _nextSpriteTime;

        private EventBus _eventBus;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _frequence = 1 / _framePerSecond;
            _nextSpriteTime = Time.time + _frequence;
            _currentSpriteIndex = 0;
            SetClip(IDLE_KEY);
            _spriteRenderer.sprite = _currentClip.Sprites[_currentSpriteIndex];
        }

        private void Update()
        {
            if(Time.time > _nextSpriteTime)
            {
                _currentSpriteIndex = (_currentSpriteIndex + 1) % _currentClip.Sprites.Length;
                _spriteRenderer.sprite = _currentClip.Sprites[_currentSpriteIndex];

                if( _currentSpriteIndex == _currentClip.Sprites.Length - 1 && _currentClip.IsLoop) 
                {
                    _currentSpriteIndex = 0;
                }
                else
                {
                    _currentClip.OnComplete?.Invoke();
                    SetClip(IDLE_KEY);
                }

                _nextSpriteTime = Time.time + _frequence;
            }
        }

        public void Conctruct(EventBus eventBus)
        {
            _eventBus = eventBus;

            _eventBus.Subscribe<OnRouteIsReady>(SetClip);
        }

        public void SetClip(string clipName)
        {
            foreach (var clip in _clips)
            {
                if (clip.Name == clipName)
                {
                    _currentClip = clip;
                    return;
                }
                else throw new Exception("Clip not found");
            }
        }

        public void SetClip(OnRouteIsReady onRouteIsReady)
        {
            var route = onRouteIsReady.Route;
            foreach (var cell in route.Cells)
            {
                var animator = cell.GetComponentInParent<SpriteAnimator>();
                animator.SetClip(CONNECT_KEY);
            }
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<OnRouteIsReady>(SetClip);
        }
    }
}