using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 실습
{
	// 내가 이해한 다익스트라 정리
	// 시작 정점을 정하고, 그 정점에서 갈 수 있는 모든 노드까지의 최단 경로를 구하는 자료구조이다.
	// 모든 노드에서 출발하여 모든 경로를 도는 FloydWarshall와는 달리, 시작 노드 하나를 구하고 거기에서 가장 빠른 경로를 구한다.
	// 전에 구했던 경로보다 더 빠른 경로가 나오면 그 경로로 갱신하면서 반복한다.
	// A -> B -> C가 있을 때, A -> C 경로가 없으니 INF로 설정한 뒤에 B를 거쳐가는 A->B + B->C를 A->C의 거리로 갱신한다.

	internal class Dijkstra
	{
		// 다익스트라 구현하기(교수님이 구현해주신 식 그대로 따라치면서 주석 달았습니다..!!ㅜㅜ)

		const int INF = 99999;	// 경로가 나오지 않는 경우를 INF로 설정한다.

		// 그래프의 graph, 시작노드인 start, 시작노드와 다른노드의 거리인 distance, 거쳐간 path를 매개변수로 한다.
		public static void ShortestPath(in int[,] graph, in int start, out int[] distance, out int[] path)
		{
			int size = graph.GetLength(0);	    // 사이즈는 2차원 배열 graph의 y축인 0차수로 설정한다.
			bool[] visited = new bool[size];    // 한 번 방문한 곳을 또 방문할 필요는 없으니, visited를 설정한다.

			distance = new int[size];			// ?에서 ?까지 가는 데에 걸리는 길이(거리)
			path = new int[size];				// 거쳐간 경로를 size 크기의 배열로 만든다.

			for(int i =0; i < size; i++)		// 사이즈 전부를 반복하여 돈다.
			{
				distance[i] = graph[start, i];  // start노드부터 i노드까지의 거리를 distance에 저장한다.
				path[i] = graph[start, i] < INF ? start : graph[start, i];
				// start노드부터 i까지의 경로가 존재한다면 path에 start노드를 저장하고, INF면 graph[start, i]를 저장한다.
			}

			for(int i = 0; i < size; i++)
			{
				// 1. 방문하지 않은 정점 중 가장 가까운 정점부터 탐색한다.
				int next = -1;		
				int minCost = INF;	// 최단거리는 아직 갱신되지 않았으므로 INF로 저장한다.
				for(int j = 0; j < size; j++)
				{
					if (!visited[j] && distance[j] < minCost)    // 방문하지 않았으면서 제일 작은 거리보다 더 작다면
					{
						next = j;	// 다음 노드는 j로 하고
						minCost = distance[j];	// minCost는 j까지의 거리로 갱신한다.
					}
				}
				if (next < 0)	// 만약 next가 0보다 작으면(예외처리)
					break;		// 반복문을 탈출한다.

				// 2. 직접 연결된 거리보다 거쳐서 더 짧아진다면 갱신(next를 거쳐서 더 짧아지는 거리가 있다면 갱신)
				for (int j = 0; j < size; j++)
				{
					// start, next, j 이렇게 노드 3개가 있는 것처럼 함.
					// distance[j] : 목적지까지 직접 연결된 거리 (start부터 j까지 거리)
					// distance[next] : 탐색중인 정점까지 거리 (start부터 next까지 거리)
					// graph[next, j] : 탐색중인 정점부터 목적지의 거리 (next랑 j의 거리)
					if (distance[j] > distance[next] + graph[next, j])
					{
						distance[j] = distance[next] + graph[next, j];
						path[j] = next;
					}
				}
				visited[next] = true;	// next는 방문한 노드로 체크한다.
			}
		
		}
	}
}
