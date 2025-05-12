using System;
using UnityEngine;

namespace Animation
{
    [Serializable]
    public class AnimationClip
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private Action _onComplete;
        [SerializeField] private bool _isLoop;

        public string Name { get { return _name; } }
        public Sprite[] Sprites { get { return _sprites; } }
        public bool IsLoop { get { return _isLoop;} }
        public Action OnComplete { get { return _onComplete; } }
    }
}
