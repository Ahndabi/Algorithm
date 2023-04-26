using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace 실습
{
	public class Dictionary<TKey, TValue> where TKey : IEquatable<TKey>
	{
		const int DefaultCapacity = 100;		// 전체 해시 테이블의 크기

		struct Entry
		{
			public enum State { None, Using, Delected }	// 인덱스가 비어있는지, 사용중인지, 삭제됐는지를 나타내는 열거형
			public State state;
			public int hashCode;
			public TValue value;	// 데이터
			public TKey key;		// 키 값
		}

		Entry[] table = new Entry[100];	// 해시 테이블 배열을 만들어준다.
		

		public Dictionary()
		{
			table = new Entry[DefaultCapacity];     // DefaultCapacity 크기의 테이블을 만든다.
		}

		public TValue this[TKey key]    // key값으로 탐색을 해서 TValue를 가져오는 함수
		{
			get
			{
				// 1. key를 index로 해싱
				int index = Math.Abs(key.GetHashCode() % table.Length);
				// 지원해주는 HashCode를 사용해서 해시코드를 추출하고 테이블의 길이를 나누어 나온 절대값의 키값은 index이다.

				// 2. key가 일치하는 데이터가 나올 때까지 다음으로 이동
				while (table[index].state == Entry.State.Using)     // 그 위치가 사용 중일때까지 반복한다.
				{
					// 3. 동일한 키값을 찾았을 때 반환하기
					if (key.Equals(table[index].key))       // 같은 키를 발견했다면?
					{
						return table[index].value;          // 그 인덱스의 값을 반환한다.
					}
					if (table[index].state != Entry.State.Using)    // 사용중인 데이터를 못찾았다면?
					{
						break;  // 그 데이터를 저장한 적이 없는 것이기 때문에 반복문 탈출
					}
					else  // 그 위치에 다른 키값이 사용중이라면?
					{
						index = index < table.Length ? index++ : 0;     // 테이블의 크기보다 작으면 index++
					}
				}
				throw new InvalidOperationException();
			}
			set  // 탐색해서 그 값을 바꾸는 것
			{
				// 1. key를 index로 해싱
				int index = Math.Abs(key.GetHashCode() % table.Length);

				// 2. key가 일치하는 데이터가 나올 때까지 다음으로 이동
				while (table[index].state == Entry.State.Using)     // 그 위치가 사용 중일때까지 반복한다.
				{
					// 3. 동일한 키값을 찾았을 때 반환하기
					if (key.Equals(table[index].key))       // 같은 키를 발견했다면?
					{
						table[index].value = value;     // Value를 바꿔준다.
					}
					if (table[index].state != Entry.State.Using)    // 사용중인 데이터를 못찾았다면?
					{
						break;  // 데이터를 못찾았기 때문에 반복문을 탈출한다.
					}
					else  // 그 위치에 다른 키값이 사용중이라면?
					{
						index = index < table.Length ? index++ : 0;     // 테이블의 크기보다 작으면 index++
					}
				}
				throw new InvalidOperationException();
			}
		}

		public void Add(TKey key, TValue value)		// 데이터 추가하는 함수
		{
			// 1. key를 index로 해싱
			int index = Math.Abs(key.GetHashCode() % table.Length);

			// 2. 사용중이 아닌 index까지 다음으로 이동 (비어있지 않은 인덱스를 찾아야 한다.)
			while (table[index].state != Entry.State.None)
				// 만약 그곳이 Using상태거나 Delected 상태라면, 반복문을 끝낸다.
			{
				// 3-1. 동일한 키값을 찾았을 때 오류 (C# Dictionary는 중복 키를 허용X)
				if (key.Equals(table[index].key))
				{
					throw new InvalidOperationException();
				}
				else if (table[index].state == Entry.State.Using)  // 그 위치가 사용중이라면?
				{
					// 3-2. 다음 index로 이동. 
					index = index < table.Length ? index + 1 : 0;   // 테이블의 크기를 넘어서면 안되기에.
				}
				else // 그 인덱스가 Delected 된 상태였다면?
					break;	// 반복문을 탈출하여 Delected 된 곳에 데이터를 저장한다.
			}
			// 4. 사용중이 아니거나 Delected된 index를 발견한 경우에 그 위치에 저장한다.
			table[index].state = Entry.State.Using;		// 그 위치를 사용중이라고 바꾸고,
			table[index].value = value;					// value 값을 넣고,
			table[index].key = key;						// key 값을 넣고,
			table[index].hashCode = key.GetHashCode();  // 해시코드도 저장하면 된다.
		}

		public bool Remove(TKey key)
		{
			// 1. key를 index로 해싱
			int index = Math.Abs(key.GetHashCode() % table.Length);

			// 2. key값과 동일한 데이터를 찾을 때까지 index 증가
			while (table[index].state == Entry.State.Using)
			{
				if (key.Equals(table[index].key))   // 그 위치를 찾았다면?
				{
					table[index].state = Entry.State.Delected;  // Delected된 state로 바꿔준다.
					return true;		// true 반환한다.
				}
				if (table[index].state == Entry.State.None)		// 사용중인 인덱스를 찾지 못했다면?
				{	
					break;		// 반복문을 탈출하고 false 반환한다.
				}
				index = index < table.Length ? index + 1 : 0;   // 테이블의 크기를 넘어서면 안되기에.
			}
			return false;
		}
	}
}
