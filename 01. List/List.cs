using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure			// list 라고 하면 헷갈릴까봐 DataStructure 라고 바꿈
{
	internal class List<T>
	{
		private const int DefaultCapacity = 10;

		private T[] items;       // 자료형 T 배열을 가지고 있다.
		private int size;

		public List()
		{
			this.items = new T[DefaultCapacity];
			this.size = 0;
		}

		public int Capacity { get { return items.Length; } }

		public int Count {  get { return size; } }

		public T this[int index]		// 인덱서 구현 list[0] = 이런식으로 접근 가능하게 해줌
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
			if(size < items.Length)
			{
				items[size++] = item;
			}
			else
			{
				Grow();
				items[size++] = item;
			}
		}

		public bool Remove(T item)		// Remove는 반환형이 bool임!	제거했으면 true, 제거 못했으면 false
		{
			int index = IndexOf(item); 
			if(index >= 0)
			{   
				// 찾았을 경우
				RemoveAt(index);		// 그 위치에 있는 거 지워
				return true;
			}
			else
			{
				// 못찾은 경우
				return false;
			}
		}

		public void RemoveAt(int index)		// index 번째 지워줘!
		{
			if (index < 0 || index >= size)					// 범위를 벗어나면
				throw new IndexOutOfRangeException();		// 예외처리

			size--;     // 지웠으니까 size 하나 줄어들지
			Array.Copy(items, index + 1, items, index, size - index);
			// items의 index+1부터, items의 index부터 size-index까지를 복사??
		}


		public int IndexOf(T item)		// 찾고자 하는 대상의 인덱스가 몇인지 구하는 함수
		{
			return Array.IndexOf(items, item, 0, size);		// 0부터 size까지 items에 item이 있는지 인덱스 추출
		}

		public T? Find(Predicate<T> match)
		{
			if (match == null)
				throw new ArgumentNullException("match");       // match가 null이면 안돼! 예외처리

			for (int i = 0; i < size; i++)
			{
				if (match(items[i]))
					return items[i];
			}

			return default(T);
		}

		public int FindIndex(Predicate<T> match)
		{
			for(int i = 0; i <size; i++)
			{
				if (match(items[i]))
					return i;
			}
			return - 1;
		}
		

		public void Grow()		// 크기 키우기
		{	
			int newCapacity = items.Length * 2;
			T[] newItems = new T[newCapacity];			// 2배 더 큰 배열을 만들었음.
			Array.Copy(items, 0, newItems, 0, size);    // 새 배열에 원래 배열 데이터들 복사
			items = newItems;							// items의 주소를 바꿈. newitems로.
														// 그리고 newitems는 사라짐.
														// 원래 배열도 가비지콜렉터에 의해 사라지게 될 거야
		}
	}
}
