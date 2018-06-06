using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class RoundRobinList<T>
    {
        private readonly IList<T> _list;
        private readonly int _size;
        private int _position;
        private object _lock;

        public RoundRobinList(IList<T> list)
        {
            if (!list.Any())
                throw new NullReferenceException("list");

            _list = new List<T>(list);
            _size = _list.Count;
            _position = 0;
            _lock = new object();
        }

        public T Next()
        {
            if (_size == 1)
                return _list[0];

            lock (_lock)
            {
                if (_position == _size)
                {
                    _position = 0;
                }
                return _list[_position++];
            }
        }
    }
}
