using System;

namespace Properties
{
    public class Property<T> where T : struct
    {
        private T _value;
        private Action<T> _onChange;

        public Property(T value)
        {
            Value = value;
        }

        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                _onChange?.Invoke(value);
            }
        }

        public void AddListener(Action<T> onChange)
        {
            _onChange += onChange;
        }

        public void RemoveListener(Action<T> onChange)
        {
            _onChange -= onChange;
        }
    }
}
