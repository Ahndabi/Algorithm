// using System.Collections.Generic;
// 위 시스템은 자료구조들을 담고 있음.

using DataStructrue;

namespace _06.Heap
{
	internal class Program
	{
		/******************************************************
		 * 힙 (Heap)
		 * 
		 * 부모 노드가 항상 자식노드보다 우선순위가 높은 속성을 만족하는 트리기반의 자료구조
		 * 많은 자료 중 우선순위가 가장 높은 요소를 빠르게 가져오기 위해 사용
		 * 힙영역의 힙과 완전 무관. 전혀 상관 없음.
		 ******************************************************/
		// 둘 이상의 저장 방식?
		// Heap을 이용한 어뎁터임.
		// 트리기반 : 1. 하나의 노드(부모)가 여러 자식들을 가질 수 있다.
		//			  2. 자식이 다시 부모를 가리킬 수 없다.(순환구조 불가능) 
		// 부모가 두개 자식을 가지는 것 : 이진트리
		// 힙은 이진트리의 자료구조임.
		// 순서가 일직선인 선형자료가 아니라, 비선형자료구조이다.
		// 우선순위 큐는 게임에서 많이 사용됨.
		static void PriorityQueue()
		{                   // int : 우선순위를 판가름 할 키
							// string : 실제 데이터 타입
			PriorityQueue<string, int> pq = new PriorityQueue<string, int>();
			// 가장 먼저 나오는 애는 우선순위가 가장 높은 애임.

			pq.Enqueue("감자", 3);
			pq.Enqueue("양파", 5);
			pq.Enqueue("당근", 1);
			pq.Enqueue("토마토", 2);
			pq.Enqueue("마늘", 4);

			// string으로 우선순위를 만들면 숫자랑 다르게 

			while (pq.Count > 0)
			{
				Console.WriteLine(pq.Dequeue());	// 우선순위가 높은 순서대로 데이터 출력
				// output: 당근 토마토 감자 마늘 양파

				// 근데 이건 오름차순이고, 만약 내림차순으로 하고 싶으면
				// PriorityQueue<string, int> pq 
				// = new PriorityQueue<string, int>(Comparer<int>.Create((a, b) => b - a));
				// 이렇게 작성
			}
		}

		// 시간복잡도
		// 탐색(가장우선순위높은)	 추가	     삭제
		// 0(1)						0(logN)		0(logN)


		// C#에서는 노드를 피한다고 했지. 그래서 힙도 마찬가지인데
		// 무조건 두 개씩 자식을 갖고 있는다고 했으니까 노드기반으로 만들지 않고
		// 노드들마다 순서(번호)를 매겨서 배열처럼 만든다. 이렇게 각각의 노드들을 순서대로 배열로 만듦

		// 만약 우선순위가 높은 새로운 노드가 추가되면, 배열의 앞부분에 끼워넣는 게 아니라,
		// 우선 마지막 배열 순서에 넣은 다음에 추가된 노드의 부모들과 우선순위를 비교해서
		// 우선순위가 더 높다면 부모와 자리를 바꾸는 식으로 앞으로 옮겨감.

		// 만약 있는 노드를 삭제하려면?
		// 일단 부모가 없는 자식이 있으면 안 돼.
		// 가장 뒤에 있던 자료를 그 빵꾸난 부모자리로 일단 옮김.(여기서 힙 구조가 깨지겠지?)
		// 그럼 빵구를 채웠던 그 노드가 밑의 자식들과 비교해서 뒤로 뒤로 옮기는 식으로 함.

		static void Main(string[] args)
		{
			// PriorityQueue();
			PriorityQueue<string> queue = new PriorityQueue<string>();
			queue.Enqueue("다비", 4);
			queue.Enqueue("딸기", 6);
		}


	}
}