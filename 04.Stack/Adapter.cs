using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.Stack
{
	// 리스트에 있는 기능을 가지고 Stack을 만들면 훨씬 쉬움. 이런 경우를 Adapter라고 해.

	/******************************************************
	 * 어댑터 패턴 (Adapter)
	 * 
	 * 한 클래스의 인터페이스를 사용하고자 하는 다른 인터페이스로 변환
	 ******************************************************/


	internal class Adapter<T>
	{
		private List<T> container;

		public Adapter()
		{
			container = new List<T>();
		}

		public void Push(T item)
		{
			container.Add(item);
		}

		public T Pop()
		{
			T item = container[container.Count - 1];
			container.RemoveAt(container.Count - 1);
			return item;
		}

		public T Peek()
		{
			return container[container.Count - 1];
		}
	}
}
