namespace _05.Queue
{
	internal class Program
	{
		/******************************************************
		 * 큐 (Queue)
		 * 
		 * 선입선출(FIFO), 후입후출(LILO) 방식의 자료구조
		 * 입력된 순서대로 처리해야 하는 상황에 이용
		 ******************************************************/

		static void Test()
		{
			Queue<int> queue = new Queue<int>();
											// Enqueue : 데이터 넣는 거 
			for (int i = 0; i < 10; i++) queue.EnQueue(i);      // 0123456789

								// Peek : 최전방. 가장 앞에 어떤 데이터 있는 지 확인 
			Console.WriteLine(queue.Peek());		// 0
		
			while(queue.Count > 0)
			{						 // Dequeue : 꺼내는 거 
				Console.WriteLine(queue.Dequeue());				// 0123456789
			}
		}

		static void Main(string[] args)
		{
		}
	}
}