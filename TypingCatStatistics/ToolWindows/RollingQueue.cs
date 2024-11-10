using System.Collections;
using System.Collections.Generic;

namespace TypingCatStatistics
{
    public class RollingQueue : IEnumerable<int>
    {
        public int Count => _queue.Count;
        public int Buffer => _buffer;
        public int Sum => _sum;
        public double Average => (double)_sum / _queue.Count;

        private readonly int _buffer;
        private readonly Queue<int> _queue;
        private int _sum;

        public RollingQueue(int buffer)
        {
            _buffer = buffer;
            _queue = new(_buffer);
        }

        public int Enqueue(int item)
        {
            _queue.Enqueue(item);
            _sum += item;
            if (_queue.Count >= _buffer)
            {
                var last = _queue.Dequeue();
                _sum -= last;
                return last;
            }
            return 0;
        }

        public int Dequeue() => _queue.Dequeue();

        public IEnumerator<int> GetEnumerator() => _queue.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
