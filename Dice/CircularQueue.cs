using System;

namespace Dice
{
    public class CircularQueueStatics
    {
        internal static readonly Random Random = new Random();
    }
    
    public class CircularQueue<T> : CircularQueueStatics
    {
        private int _head;
        private int _tail;
        private readonly T[] _queue;
        
        public int Count => (_head <= _tail) ? _tail - _head : Size - _head + _tail;
        public int Size => _queue.Length;
        public bool Empty => _head == _tail;
        public bool Full => (_tail + 1) % Size == _head;
        public T Top => !Empty ? _queue[_head] : throw new InvalidOperationException("Queue is empty");

        public CircularQueue(int size)
        {
            _queue = new T[size];
            _head = 0;
            _tail = 0;
        }

        public CircularQueue(T[] array)
        {
            _queue = array;
            _head = 0;
            _tail = array.Length;
        }

        public void Append(T item)
        {
            if (Full)
            {
                throw new Exception("Queue is full");
            }

            _queue[_tail] = item;
            _tail = (_tail + 1) % Size;
        }

        public T Pop()
        {
            if (Empty)
            {
                throw new Exception("Queue is empty");
            }

            var item = _queue[_head];
            _head = (_head + 1) % Size;
            return item;
        }
        
        public void Clear()
        {
            _head = 0;
            _tail = 0;
        }

        public void Shuffle()
        {
            for (var i = 0; i < Count; i++)
            {
                var r = Random.Next(i, Count);
                
                var iIndex = (_head + i) % Size;
                var rIndex = (_head + r) % Size;
                
                (_queue[iIndex], _queue[rIndex]) = (_queue[rIndex], _queue[iIndex]);
            }
        }
    }
}