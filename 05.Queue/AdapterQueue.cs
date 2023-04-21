using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05.Queue
{
	/******************************************************
	 * 어댑터 패턴 (Adapter)
	 * 
	 * 한 클래스의 인터페이스를 사용하고자 하는 다른 인터페이스로 변환
	 ******************************************************/

	// Queue는 리스트를 활용하면 Add로 추가하는 건 효율적인데, ReMoveAt하면 매우 비효율적.
	// 왜냐하면 맨 앞에[0]를 지웠을 때, 그 뒤에 애들을 한 칸씩 앞당겨야 해서 부담임;;
	// 그래서 링크드리스트를 사용해!! 앞당길 필요 없잖아

	// 근데 C#의 특성상 링크드리스트를 잘 안쓰자나,,, 그래서 C#은 Queue를 링크드리스트로 구현을 안 해.
	// 그래서 Queue같은 경우에는 직접 구현해서 써. (C++은 링크드리스트 활용해서 만듦. C#만 ㄴㄴ)

	internal class AdapterQueue<T>
	{
		private LinkedList<T> container;

		public AdapterQueue()
		{
			container = new LinkedList<T>();
		}

		public void Push(T item)
		{
			container.AddLast(item);
		}

		public T Pop()
		{
			T value = container.First<T>();
			container.RemoveFirst();
			return value;
		}
	}
}
