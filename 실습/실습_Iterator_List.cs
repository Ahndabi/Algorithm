using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 실습
{
	internal class 실습_Iterator_List
	{
		// 반복자 내용 제외한 부분은 깃허브에서 가져왔습니다.
		internal class List<T> : IEnumerable<T>
		{
			private const int DefaultCapacity = 4;

			private T[] items;
			private int size;

			public List()
			{
				items = new T[DefaultCapacity];
				size = 0;
			}

			public int Capacity { get { return items.Length; } }
			public int Count { get { return size; } }

			public T this[int index]
			{
				get
				{
					if (index < 0 || index >= size)
						throw new IndexOutOfRangeException();

					return items[index];
				}
				set
				{
					if (index < 0 || index >= size)
						throw new IndexOutOfRangeException();

					items[index] = value;
				}
			}

			public void Add(T item)
			{
				if (size >= items.Length)
					Grow();

				items[size++] = item;
			}

			public void Clear()
			{
				items = new T[DefaultCapacity];
				size = 0;
			}

			public T? Find(Predicate<T> match)
			{
				if (match == null) throw new ArgumentNullException();

				for (int i = 0; i < size; i++)
				{
					if (match(items[i]))
						return items[i];
				}

				return default(T);
			}

			public int FindIndex(Predicate<T> match)
			{
				return FindIndex(0, size, match);
			}

			public int FindIndex(int startIndex, int count, Predicate<T> match)
			{
				if (startIndex > size)
					throw new ArgumentOutOfRangeException();
				if (count < 0 || startIndex > size - count)
					throw new ArgumentOutOfRangeException();
				if (match == null)
					throw new ArgumentNullException();

				int endIndex = startIndex + count;
				for (int i = startIndex; i < endIndex; i++)
				{
					if (match(items[i])) return i;
				}
				return -1;
			}

			public int IndexOf(T item)
			{
				return Array.IndexOf(items, item, 0, size);
			}

			public bool Remove(T item)
			{
				int index = IndexOf(item);
				if (index >= 0)
				{
					RemoveAt(index);
					return true;
				}
				return false;
			}

			public void RemoveAt(int index)
			{
				if (index < 0 || index >= size)
					throw new IndexOutOfRangeException();

				size--;
				Array.Copy(items, index + 1, items, index, size - index);
			}

			private void Grow()
			{
				int newCapacity = items.Length * 2;
				T[] newItems = new T[newCapacity];
				Array.Copy(items, 0, newItems, 0, size);
				items = newItems;
			}

			public IEnumerator<T> GetEnumerator()
			{
				return new Enumerator(this);
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return new Enumerator(this);
			}

			public struct Enumerator : IEnumerator<T>
			{
				// 리스트의 반복자는 현재 돌고있는 리스트, 인덱스, 현재 값을 가지고 있어야 합니다.
				T current;
				List<T> list;
				int index;

				internal Enumerator(List<T> list)
				{
					this.list = list;
					this.index = 0;					// 인덱스는 0으로 초기화 합니다.
					this.current = default(T);		// 현재 값은 기본값으로 설정합니다.
				}

				public T Current { get { return current; } }	

				object IEnumerator.Current { get { return current; } }

				public void Dispose()
				{
				}

				public bool MoveNext()
				{
					while(index < list.Count)
					{
						current = list[index];  // 현재 값은 현재 인덱스의 리스트 값입니다.
						index++;    // 인덱스를 증가시킵니다.
						return true;
					}
					return false;
				}

				public void Reset()
				{
					index = 0;
					current = default(T);
				}
			}
		}

	}
}
