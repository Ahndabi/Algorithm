using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace _11._Searching
{
	public class Search
	{
		// <순차 탐색>
		// 처음부터 끝까지 하나하나 전부 확인하면서 동일한 값을 찾을 때까지 함. 없으면 -1 반환
		// 값이 같은지 아닌지 확인
		// 자료가 정렬이 안 되어 있어도 탐색 가능
											// in은 입력전용 파라메터임(출력전용 out도 있음) 안붙여도 결과 같음 
		public static int SequentialSearch<T>(in IList<T> list, in T item) where T : IEquatable<T>
		{
			for(int i = 0; i < list.Count; i++) 
			{
				if (item.Equals(list[i]))
					return i;	// 찾은 경우
			}
			return -1;	// 못 찾은 경우
		}
		

		// <이진 탐색>
		// 탐색이 반절 반절씩,, 줄어듦
		// 단, 데이터들이 전부 정렬이 되어 있을 때만 보장이 가능한 탐색임.
		// 같은지 아닌지에(같은 것만 찾는것) 초점이 맞춰있으니 Icomparable보다는
		// 찾는 영역이 계속 줄어들 거야.
		// 처음엔 제일 작은 값을 low, 제일 큰 값을 high라고 두고, 그 중간이 중간지점임
		// 중간 영역을 찾았는데 내가 찾고자 하던 값보다 커? 그럼 처음부터 중간까지 탐색,, 이렇게 범위를 줄여나감
		// 이렇게 범위가 줄여나갈 땐 그 중간이 low혹은 high로 대체됨. 그렇게 계속,,,
		public static int BinarySearch<T>(in IList<T> list, in T item) where T : IComparable<T>
		{
			int low = 0;
			int high = list.Count - 1;
			while(low <= high)
			{	// 중간 위치를 찾음
				int middle = (low + high) >> 1;     // 나누기 2 하는 것보다 0.5 곱하는 게 훨빠름. 컴퓨터가 나누기에 약함
													// 근데 >> 1 이렇게 해서 비트연산하는 게 제~일 빠름
				int compare = list[middle].CompareTo(item);	// 내가 찾고자하는 item과 비교함

				if(compare < 0)			// 비교를 했는데 더 작은 값이 나왔다? 그럼 중간값을 low로 설정
					low = middle + 1;	// middle까지는 이미 비교했잖아! 그럼 굳이 다시 비교할 필요 없으니 middle + 1을 low로 설정
				else if(compare  > 0)	// 더 큰 값이 나왔다? 그럼 중간값을 high로 설정
					high = middle - 1;	// 마찬가지로 middle은 비교 이미 했으니까 -1을 high로 설정
				else    // 찾은 경우
					return middle;
			}
			return -1;		// 못 찾음! 없음!
		}
		// LinkedList는 중간위치를 찾기가 어려워,,, 그리고 low나 high가 중간까지 가려면 어차피 n/2 노드를 탐색해야 하니까
		// 그래서 LinkedList는 바이너리서치가 없음.
		// 인덱스 개념이 있는 경우에만 BinarySearch 사용 가능


		// 일반 트리구조의 자료들을 탐색하는 것도 배워보자! 다른 클래스 ㄱㄱ


		// 그래프 안에 내가 찾고자 하는 정점이 있느냐 를 알아볼 수 있어야겠지 (이진탐색트리 제외. 얘는 애초에 탐색하려고 만듬,,)
		// 그래프 서치 알고리즘 : DFS BFS	(차이점 설명 이런 거 기술면접)



		// <깊이 우선 탐색> DFS (Depth first search)
		// 그래프의 분기를 만났을 때 최대한 깊이 내려간 뒤, 더이상 깊이 갈 곳이 없을 경우 다음 분기를 탐색 
		// 일단 한 번 쭉~ 한 길로 찔러보고 더 못내려가면 그 옆으로 가서 다시 쭉~ 끝까지 내려가고,, 반복(찌르기)
		// 백트래킹 (분할정복)
		// 함수호출 스택을 이용해서 하는 구현
		// 내가 그 위치에 갈 수 있는지 여부(visited), 어떤 경로로 갈 수 있는지(path). 2가지가 필요
		public static void DFS(bool[,] graph, int start, out bool[] visited, out int[] parents)
		{
			visited = new bool[graph.GetLength(0)];
			parents = new int[graph.GetLength(0)];
			for (int i = 0; i < graph.GetLength(0); i++)
			{
				visited[i] = false;
				parents[i] = -1;
			}
		}
		private static void SearchNode(bool[,] graph, int start, bool[] visited, int[] parents)
		{
			visited[start] = true;
			for (int i = 0; i < graph.GetLength(0); i++)
			{
				if (graph[start, i] &&  // start부터 i까지 갈 수 있는 연결되어 있는 정점이며,
					!visited[i])        // 방문한 적 없는 정점
				{
					parents[i] = start;
					SearchNode(graph, i, visited, parents);
				}	// 같고 있는 정점노드만큼 호출. 
			}
		}



		// <너비 우선 탐색> BFS (Breadth first search)
		// 그래프의 분기를 만났을 때 모든 분기를 하나씩 저장하고, 저장한 분기를 한번씩 거치면서 탐색
		// 균등하게 한 칸씩 다 가봄.
		// 옆에 옆에 옆에 가보고 옆에 더이상 없으면 그다음 한 칸 밑으로 내려가서 옆에 옆에,,반복
		// Queue로 구현
		public static void BFS(bool[,] graph, int start, out bool[] visited, out int[] parents)
		{
			visited = new bool[graph.GetLength(0)];
			parents = new int[graph.GetLength(0)];
			for (int i = 0; i < graph.GetLength(0); i++)
			{
				visited[i] = false;
				parents[i] = -1;
			}

			Queue<int> bfsQueue = new Queue<int>();
			bfsQueue.Enqueue(start);	// 맨 처음 탐색될 정점을 queue에 담아두고
			while(bfsQueue.Count > 0)	// queue가 빌 때까지 반복
			{
				int next = bfsQueue.Dequeue();
				visited[next] = true;   // 이미 뺀 정점은 방문했다는 표시 해줌

				for (int i = 0; i < graph.GetLength(0); i++)
				{
					if (graph[start, i] &&  // start부터 i까지 갈 수 있는 연결되어 있는 정점이며,
						!visited[i])        // 방문한 적 없는 정점
					{
						parents[i] = start;
						bfsQueue.Enqueue(i);
					}   
				}
			}
		}
	}
}
