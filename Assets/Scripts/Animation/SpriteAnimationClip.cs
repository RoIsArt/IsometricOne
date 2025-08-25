using System;
using UnityEngine;
using UnityEngine.Events;

namespace Animation
{
    [Serializable]
    public class SpriteAnimationClip
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private bool _isLoop;

        public string Name { get { return _name; } }
        public Sprite[] Sprites { get { return _sprites; } }
        public bool IsLoop { get { return _isLoop;} }
    }
}
