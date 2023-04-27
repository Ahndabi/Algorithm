namespace _09.DesignTechnique
{
	// 이제 알고리즘 ! 
	internal class Program
	{
		/******************************************************
		 * 알고리즘 설계기법 (Algorithm Design Technique)
		 * 
		 * 어떤 문제를 해결하는 과정에서 해당 문제의 답을 효과적으로 찾아가기 위한 전략과 접근 방식
		 * 문제를 풀 때 어떤 알고리즘 설계 기법을 쓰는지에 따라 효율성이 막대하게 차이
		 * 문제의 성질과 조건에 따라 알맞은 알고리즘 설계기법을 선택하여 사용
		 ******************************************************/

		// 나는 탐색이 중요한 작업을 할 건데 탐색에서 좀 비효율적인 자료구조를 사용했다? 
		// 자료구조 자체는 접근 탐색 삽입 삭제 중에서 내가 고려하여 정하면 돼.(직관적으로 파악 가능)
		// 알고리즘은 과연 어떨 때 그런 자료구조들을 써줘야 하는지 !
		// 어떤 기준들로 알고리즘을 선택해야 하냐? - 정답은 없음.

		// 알고리즘 설계기법들을 도입을 하여 문제를 해결하는공


		// 하노이탑 - 분할 정복으로 구현
		public static void Move(int count, int start, int end)
		{
			if(count == 1)
			{
				int node = stick[start].Pop();
				stick[end].Push(node);
				Console.WriteLine($"{start} 스틱에서 {end} 스틱으로 {node} 이동");
				return;		// 하나 남았을 땐 그냥 이동
			}
			int other = 3 - start - end;       // 나머지 임시거처 할 수 있는 장소
			Move(count - 1, start, other);
			Move(count - 1, other, end);
			Move(1, start, end);
		}

		public static Stack<int>[] stick;	// 스틱은 3개

		static void Main(string[] args)
		{
			// stick 3개 만들기
			stick = new Stack<int>[3];
			for(int i = 0; i < stick.Length; i++)
			{
				stick[i] = new Stack<int>();
				// 배열만든 것과 스택 만드는 것은 별개임.
				// 각각의 배열 0 1 2 칸에 new Stack을 만들어줘야함.
				// 0 에도 new Stack , 1에도 new Stack, 2에도 new Stack을 만든거임
			}
			for (int i = 5; i > 0; i--)
			{
				stick[0].Push(i);
			} 
			Move(6, 0, 2);
		}
	}
}