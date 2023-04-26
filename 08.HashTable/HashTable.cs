using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 구현방식도 중요
// 깃허브가 정석이래
// 잘못된 것 같음. 그냥 깃허브 코드 봐
namespace DataStructure
{
	public class Dictionary<TKey, TValue> where TKey : IEquatable<TKey>
	{
		// 맨 처음에 크게 만들어주기~ (일단 1000정두)
		const int DefaultCapacity = 1000;
		
		struct Entry
		{
			public enum State { None, Using, Delected }	
			public State state;
			public int hashCode;
			public TKey Key;
			public TValue Value;
		}

		Entry[] table;

		public Dictionary()
		{
			table = new Entry[DefaultCapacity];
		}

		public TValue this[TKey key]	// Key값으로 탐색을 해서 TValue를 가져오는 함수
		{
			get
			{
				// 1. key를 index로 해싱
				int index = Math.Abs(key.GetHashCode() % table.Length);

				// 2. key가 일치하는 데이터가 나올 때까지 다음으로 이동
				while (table[index].state == Entry.State.Using) 
				{
					// 3. 동일한 키값을 찾았을 때 반환하기
					if (key.Equals(table[index].Key))   // 그 위치가 사용중이지 않다면?
					{
						return table[index].Value;
					}
					if (table[index].state != Entry.State.Using)	// 사용 중인 데이터를 못찾았다면?
					{
						break;		// 그 데이터를 저장한 적이 없는 거야~ 데이터를 못찾음!
					}
					else  // 그 위치가 사용중이라면?
					{
						// 그 다음 위치로 이동
						index = index < table.Length ? index + 1 : 0;   // 테이블의 크기를 넘어서면 안되기에.
					}
				}
				throw new InvalidOperationException();
			}
			set  // 탐색해서 그 값을 바꾸는 것
			{
				// 1. key를 index로 해싱
				int index = Math.Abs(key.GetHashCode() % table.Length);

				// 2. key가 일치하는 데이터가 나올 때까지 다음으로 이동
				while (table[index].state == Entry.State.Using)
				{
					// 3. 동일한 킥밧을 찾았을 때 덮어쓰기
					if (key.Equals(table[index].Key))   // 그 위치가 사용중이지 않다면?
					{
						table[index].Value = value;		// Value 바꿔주기
						return;
					}
					if (table[index].state != Entry.State.Using)    // 사용 중인 데이터를 못찾았다면?
					{
						break;      // 그 데이터를 저장한 적이 없는 거야~ 데이터를 못찾음!
					}
					else  // 그 위치가 사용중이라면?
					{
						// 그 다음 위치로 이동
						index = index < table.Length ? index + 1 : 0;   // 테이블의 크기를 넘어서면 안되기에.
					}
				}
				throw new InvalidOperationException();

			}
		}

		public void Add(TKey key, TValue value)		// 데이터 추가하는 함수
		{
			// 1. key를 index로 해싱
			// 테이블 크기는 정해져 있으니 그 안에서 저장해야해.
			int index = Math.Abs(key.GetHashCode() % table.Length); // GetHashCode를 지원해줌.(숫자로 변환해줌)
																	// Math.Abs 는 절댓값. GetHashCode 가 마이너스가 나와버리면 안 되니까!
																	// 이제 그 인덱스 값에 데이터를 넣기 전에 충돌이 일어날 수도 있으니 충돌 처리 해줘야함.
			// 2. 사용중이 아닌 index까지 다음으로 이동
			while (table[index].state == Entry.State.Using)		// 만약 사용중이라면 다른 곳에 넣어야지
			{
				// 3-1. 동일한 키값을 찾았을 때 오류 (C# Dictionary는 중복 키를 허용X
				if (key.Equals(table[index].Key))	// 그 위치가 사용중이지 않다면?
				{
					throw new InvalidOperationException();	
				}
				else  // 그 위치가 사용중이라면?
				{
					// 3-2. 다음 index로 이동. 
					index = index < table.Length ? index + 1 : 0;	// 테이블의 크기를 넘어서면 안되기에.
				}
			}
			// 4. 사용중이 아닌 index를 발견한 경우 그 위치에 저장.
			table[index].hashCode = key.GetHashCode();
			table[index].Key = key;
			table[index].Value = value;
			table[index].state = Entry.State.Using;
		}

		public bool Remove(TKey key)	// 지우는 함수
		{
			// 1. key를 index로 해싱
			int index = Math.Abs(key.GetHashCode() % table.Length);

			// 2. key값과 동일한 데이터를 찾을 때까지 index 증가
			while (table[index].state == Entry.State.Using)
			{
				if (key.Equals(table[index].Key))   // 그 위치가 사용중이지 않다면?
				{
					table[index].state = Entry.State.Delected;  // 지웠다! 라고 해줘야함.
					return true;
				}
				if (table[index].state == Entry.State.None)
				{
					break;
				}
				index = index < table.Length ? index + 1 : 0;   // 테이블의 크기를 넘어서면 안되기에.
			}
			return false;
		}
	}
}
