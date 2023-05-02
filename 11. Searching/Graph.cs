using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11._Searching
{
	internal class Graph
	{
		/******************************************************
		 * 그래프 (Graph)
		 * 
		 * 정점의 모음과 이 정점을 잇는 간선의 모음의 결합
		 * 한 노드에서 출발하여 다시 자기 자신의 노드로 돌아오는 순환구조를 가짐
		 * 간선의 방향성에 따라 단방향 그래프, 양방향 그래프가 있음 (양방향 : 서울에서 강릉 갔는데, 거기서 강릉에서 서울로도 ㅆㄱㄴ)
		 * 간선의 가중치에 따라   연결 그래프, 가중치 그래프가 있음
		 * 경로탐색, 길찾기 등에 사용됨.(Map쪽에 많이 쓰임)
		 ******************************************************/

		// 지점 지점 지점들..에 해당하는 정점(vertex?)들이 있고, 이 정점들을 연결하는 간선(edge)들이 있음
		// 정점들과 간선들의 모음집 = 그래프
		// 어떤 정점은 간선이 안 붙어있을 수도 있고, 여러 간선들이 붙어있을 수도 있음
		// 근데 트리도 정점도 간선들의 모음집임
		// 그럼 그래프랑 트리의 차이점은?
		// 그래프는 순환구조가 있음. (순환구조가 딱 하나라도 있으면 그래프. 하나라도 아예 없으면 트리임)
		// 트리는 다시 자기로 돌아올 수 없음. 순환구조X
		// 갔던 길 말고 다른 길로 돌아와야 순환구조라고 할 수 있음. 단순히 하나의 간선에서 왔다갔다가 순환이 아님!!

		// 그래프는 두 가지로 표현이 가능.



		// <인접행렬 그래프>
		// 그래프 내의 각 정점의 인접 관계를 나타내는 행렬(리스트)
		// 2차원 배열을 [출발정점, 도착정점]으로 표현
		// 장점 : 인접여부 접근이 빠름 0(1)
		// 단점 : 메모리 사용량이 많음 0(n^2) (모든 연결을 다 true false,,이렇게 하나하나 표현해줘야 하니까)
		//		  정점의 개수가 많아지면 인접리스트를 쓰는 경우가 많음/
		// [시작정점, 끝정점]	ex) [1,3] 1정점 -> 3정점
		bool[,] matrixGraph1 = new bool[5, 5]	// 이거 무슨 교수님이 보여주신 그래프 하나 보고 한 거임
		{  // [0,0] [0,1] [0,2] [0,3] [0,4]
			{false, true, true, true, true},
			{true, false, true, false, true},
			{true, true, false, false, true},
			{true, false, false, false, true},
			{true, true, false, true, false}
			// 양방향인 경우 대각선으로 나눴을 때 대칭임.
		};

		// 가중치를 표현할 수 있는 걸 만들어
		const int INF = int.MaxValue;   // 최대값. 단절표현
		int[,] matrixGraph2 = new int[5, 5]
		{   // -1이면 연결X 을 뜻함. 거리가 -1인 경우는 없자나(음수는 단절)
			// 혹은 INF를 단절로 표현. 이 거리는 무한으로 걸린다!
			{0, 132, 16, INF, INF },
			{0, 132, 16, INF, INF },
			{0, 132, 16, INF, INF },
			{0, 132, 16, INF, INF },
			{0, 132, 16, INF, INF }
		};

		public void Test()
		{
			if (matrixGraph1[1, 3]) { }
				// true면 갈 수 있음
		}




		// <인접리스트 그래프>
		// 인접한 간선만을 리스트에 추가하여 관리 o(N)
		// 단점 : 인접여부를 확인하기 위해 리스트 탐색이 필요(그래서 속도가 좀 느릴 수 있음) 0(N)
		//		  (1번이랑 연결되어 있는 거를 확인할 때 거기에 연결되어 있는 리스트 다 돌아야 하니까)
		// 갯수가 언제든지 늘어날 수도 있기 때문에 Node기반은 좀,,, 그래서 List씀
		// 정점마다 연결되어 있는 인접정점이 다 다르지. 그래서 하나의 정점이 여러 노드를 가질 수 있도록 함
		List<List<int>> listGraph1;  // 연결 그래프
		List<List<(int, int)>> listGraph2;	// 가중치 그래프
		public void CreateGraph()
		{
			listGraph1 = new List<List<int>>();
			for(int i = 0; i < 5; i++)
			{
				listGraph1.Add(new List<int>());
			}
			listGraph1[0].Add(1);
			listGraph1[1].Add(0);
			listGraph1[1].Add(3);
		}	
	}
}
