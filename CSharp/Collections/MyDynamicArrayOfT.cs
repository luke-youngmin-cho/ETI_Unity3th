using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    internal class MyDynamicArray<T> : IEnumerable<T>
    {
        public int Capacity
        {
            get
            {
                return _items.Length;
            }
        }

        public int Count
        {
            get
            {
                return _count;
            }
        }
        private int _count;

        public T this[int index]
        {
            get
            {
                if (index >= _count)
                    throw new IndexOutOfRangeException();
                return _items[index];
            }
            set
            {
                if (index >= _count)
                    throw new IndexOutOfRangeException();
                _items[index] = value;
            }
        }

        private const int DEFAULT_SIZE = 1;
        private T[] _items = new T[DEFAULT_SIZE];

        public void Add(T item)
        {
            //공간이 부족하다면
            if (_count >= _items.Length)
            {
                T[] tmp = new T[_count * 2];
                Array.Copy(_items, 0, tmp, 0, _count);
                _items = tmp;
            }

            _items[_count++] = item;
        }

        public int FindIndex(Predicate<T> match)
        {
            for (int i = 0; i < _count; i++)
            {
                if (match(_items[i]))
                    return i;
            }
            return -1;
        }

        public T Find(Predicate<T> match)
        {
            for (int i = 0; i < _count; i++)
            {
                if (match(_items[i]))
                    return _items[i];
            }
            return default;
        }

        public void RemoveAt(int index)
        {
            if (index >= _count)
                throw new IndexOutOfRangeException();

            for (int i = index; i < _count - 1; i++)
                _items[i] = _items[i + 1];

            _count--;
        }

        public bool Remove(T item)
        {
            for (int i = 0; i < _count; i++)
            {
                if (Comparer<T>.Default.Compare(_items[i], item) == 0)
                {
                    RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        // 책을 읽어주는 자
        public struct Enumerator : IEnumerator<T>
        {
            // 현재 페이지 내용
            public T Current => _data._items[_index];

            object IEnumerator.Current => _data._items[_index];

            private MyDynamicArray<T> _data; // 책
            private int _index; // 현재 페이지

            // 책을 읽어주는자에게 어떤 책을 읽어달라고할지 책을 넘겨줘야 함..
            public Enumerator(MyDynamicArray<T> data)
            {
                _data = data;
                _index = -1; // 책 표지 덮어서 줌 
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                _index++;
                return _index < _data._count; // 넘기려는 책 페이지가 전체 장수보다 작으면 OK 아니면 False
            }

            public void Reset()
            {
                _index = -1;
            }
        }
    }


    class A
    {
        public int num = 1;
    }

    class B : A
    {
        new public int num = 3;
    }
    
    class C
    {
        public int age = 2;
    }

    class Test
    {
        void main()
        {
            A a = new A();
            B b = new B();
            Console.WriteLine(a.num); // 1
            Console.WriteLine(b.num); // 3

            // 공변성
            // 하위타입을 기반타입으로 참조할 수 있는 성질
            // 원리 : 참조타입은 첫번째 주소로부터 정의되어있는 데이터들만큼 메모리를 읽어 사용하는것이며, 상속받은 클래스가 있을 경우
            // 상속받은 기반타입의 데이터부터 할당하기 때문에 기반타입으로 데이터를 읽었을때 데이터가 소실되거나 잘못된 데이터가 읽어질 일이 없음.
            A a2 = b;
            Console.WriteLine(a2.num); // 1
        }
    }
}
