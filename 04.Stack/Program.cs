namespace _04.Stack
{
	internal class Program
	{
		// 스택, 큐 : 이 자료구조는 효율(시간복잡도)보다는 사용용도(특징, 역할) 때문에 쓰이는 경우가 많음

		// 스택
		// 선입후출(FILO), 후입선출(LIFO)
		// 박스형
		// 처음 넣은 게 마지막에 나오는 특징 때문에 사용하는 경우가 많음
		// ex. 뒤로가기(ctrl+z), 스킬트리 등 ...

		// 큐(Queue)
		// 선입선출(FIFO)
		// 파이프라인(중간에서 뽑을 수 없고 입구와 출구가 반대)
		// 먼저 온 데이터가 먼저 처리되어야 하는 경우
		// ex. 대기열, 진행순서


		/******************************************************
		 * 스택 (Stack)
		 * 
		 * 선입후출(FILO), 후입선출(LIFO) 방식의 자료구조
		 * 가장 최신 입력된 순서대로 처리해야 하는 상황에 이용
		 ******************************************************/


		void Test()
		{
			Stack<int> stack = new Stack<int>();
											// Push : 데이터 넣는 거 
			for (int i = 0; i < 10; i++) stack.Push(i);     // 0123456789

							     // Peek : 최상단 확인(꺼내진 않음)
			Console.WriteLine(stack.Peek());		// 9

			while(stack.Count > 0)
			{						 // Pop : 꺼내는 거
				Console.WriteLine(stack.Pop());		// 987654321
			}
		}


		static void Main(string[] args)
		{
		}
	}
}