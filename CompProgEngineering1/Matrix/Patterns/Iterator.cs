using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix.Patterns
{
    abstract class Aggregate<T>
    {
        public abstract Iterator<T> CreateIterator();
        public abstract int Count { get; protected set; }
        public abstract T this[int index] { get; set; }
    }

    class ConcreteAggregate<T> : Aggregate<T>
    {
        private readonly List<T> _items = new List<T>();

        public override Iterator<T> CreateIterator()
        {
            return new ConcreteIterator<T>(this);
        }

        public override int Count
        {
            get { return _items.Count; }
            protected set { }
        }

        public override T this[int index]
        {
            get { return _items[index]; }
            set { _items.Insert(index, value); }
        }
    }

    abstract class Iterator<T>
    {
        public abstract T First();
        public abstract T Next();
        public abstract bool IsDone();
        public abstract T CurrentItem();
    }

    class ConcreteIterator<T> : Iterator<T>
    {
        private readonly Aggregate<T> _aggregate;
        private int _current;

        public ConcreteIterator(Aggregate<T> aggregate)
        {
            this._aggregate = aggregate;
        }

        public override T First()
        {
            return _aggregate[0];
        }

        public override T Next()
        {
            T ret = default(T);

            _current++;

            if (_current < _aggregate.Count)
            {
                ret = _aggregate[_current];
            }

            return ret;
        }

        public override T CurrentItem()
        {
            return _aggregate[_current];
        }

        public override bool IsDone()
        {
            return _current >= _aggregate.Count;
        }
    }
}
