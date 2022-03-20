using System;

namespace GameUtils
{
    public class QueueException : Exception
    {
        public QueueException()
        {
        }

        public QueueException(string message)
            : base(message)
        {
        }

        public QueueException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class QueueEmptyException : QueueException
    {
        public QueueEmptyException()
        {
        }

        public QueueEmptyException(string message)
            : base(message)
        {
        }

        public QueueEmptyException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class QueueFullException : QueueException
    {
        public QueueFullException()
        {
        }

        public QueueFullException(string message)
            : base(message)
        {
        }

        public QueueFullException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class CircularQueueStatics
    {
        internal static readonly Random Random = new Random();
    }

    public class CircularQueue<T> : CircularQueueStatics
    {
        private readonly T[] _queue;
        private int _head;
        private int _tail;

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
            _tail = array.Length - 1;
            Count = array.Length;
        }

        public int Count { get; private set; } // Note: it is believed impossible to effectively autogenerate this

        /*
        The problem with automatic generation of the Count property is that it is impossible to distinguish between an empty
        queue and a full queue without adding an extra slot to the array. In practice this really doesn't matter, but generating
        it really isn't the simpler solution here
        
        Sometimes you just have to try something to know just how it won't work
        - 3665
        */
        public int Size => _queue.Length;
        public bool Empty => Count == 0;
        public bool Full => Count == Size;
        public T Top => !Empty ? _queue[_head] : throw new InvalidOperationException("Queue is empty");

        public void Append(T item)
        {
            if (Full)
            {
                throw new QueueFullException("Queue is full");
            }

            _queue[_tail] = item;
            _tail = (_tail + 1) % Size;
            Count++;
        }

        public T Pop()
        {
            // Console.WriteLine($"Pop: {_head}, {_tail}");
            if (Empty)
            {
                throw new QueueEmptyException("Queue is empty");
            }

            var item = _queue[_head];
            _head = (_head + 1) % Size;
            Count--;
            return item;
        }

        public void Clear()
        {
            _head = 0;
            _tail = 0;
            Count = 0;
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