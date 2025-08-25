using System;
using System.Collections.Generic;
using Cells;
using DatasAndConfigs;
using GameEvents;
using UnityEngine;

namespace Animation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimator : MonoBehaviour
    {
        private const string IDLE_KEY = "Idle";
        private const string CONNECT_KEY = "Connect";

        [SerializeField] private int _framePerSecond;

        private List<SpriteAnimationClip> _clips;
        private SpriteRenderer _spriteRenderer;
        private SpriteAnimationClip _currentClip;
        private int _currentSpriteIndex;
        private float _frequence;
        private float _nextSpriteTime;

        private IEventBus _eventBus;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _frequence = 1 / _framePerSecond;
            _nextSpriteTime = Time.time + _frequence;
            SetClip(IDLE_KEY);
        }

        private void Update()
        {
            if (!(Time.time > _nextSpriteTime)) return;

            _spriteRenderer.sprite = _currentClip.Sprites[_currentSpriteIndex];
            
            if (_currentClip.IsLoop)
                _currentSpriteIndex = (_currentSpriteIndex + 1) % _currentClip.Sprites.Length;
            else if (_currentSpriteIndex == _currentClip.Sprites.Length - 1) 
                SetClip(IDLE_KEY);
            
            _nextSpriteTime = Time.time + _frequence;
        }

        public void Construct(IEventBus eventBus, CellData data)
        {
            _eventBus = eventBus;
            _clips = new List<SpriteAnimationClip>();
            foreach (SpriteAnimationClip spriteAnimationClip in data.Clips)
            {
                _clips.Add(spriteAnimationClip);
            } ;
        }

        public void SetClip(string clipName)
        {
            foreach (SpriteAnimationClip clip in _clips)
            {
                if (clip.Name == clipName)
                {
                    _currentClip = clip;
                    _currentSpriteIndex = 0;
                    return;
                }
            }
        }
    }
}