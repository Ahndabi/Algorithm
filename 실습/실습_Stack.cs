using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace 실습
{
	// Stack은 LIFO, FILO의 자료구조이다.
	// 스택과 큐는 자료구조의 효율성때문에 사용하기 보다는, 그 특징과 용도로 인해 사용하는 경우가 대부분이다.
	// Stack의 특성상 []배열과 비슷해서 List를 활용하여 구현하는 것이 효율적이다. (이런 걸 어뎁터라 한다.)
	// Push, Peek, Pop
	public class 실습_Stack<T>
	{
		// 스택은 리스트를 활용해 만들 거라 list만 생성해준다.
		List<T> list;

		public 실습_Stack()		// { 1,2,3,4} 
		{
			list = new List<T>();
		}

		public void Push(T value)		// 데이터를 넣는 함수를 만든다.
		{
			list.Add(value);			// list.Add 함수를 활용한다.
										// 만약 데이터가 꽉 찼을 경우, 굳이 또 구현할 필요 없이 이미 Add 함수에 구현이 되어있다.
		}

		public T Peek()			// 제일 끝에 있는 데이터를 알려주는 최전방 함수를 만든다.
		{
			return list[list.Count - 1];	// 리스트의 개수에서 하나를 뺀 인덱스를 반환한다. (하나를 빼는 이유는 갯수와 인덱스는 1 차이가 나기 때문이다.)
		}

		public T Pop()		// 맨 위의 데이터를 제거하고 반환하는 함수이다. 
		{
			// { 1,2,3, 4}  count 3
			T item = list[list.Count - 1];		// item에 맨 위의 데이터를 보관한다.
			list.RemoveAt(list.Count - 1);      // list.RemoveAt을 이용하여 제거한다.
			return item;
		}
	}
}
