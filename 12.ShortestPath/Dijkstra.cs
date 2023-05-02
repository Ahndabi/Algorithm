using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12.ShortestPath
{
	internal class Dijkstra
	{
		/******************************************************
		 * 다익스트라 알고리즘 (Dijkstra Algorithm)
		 * 
		 * 특정한 노드에서 출발하여 다른 노드로 가는 각각의 최단 경로를 구함
		 * 방문하지 않은 노드 중에서 최단 거리가 가장 짧은 노드를 선택 후,
		 * 해당 노드를 거쳐 다른 노드로 가는 비용 계산
		 ******************************************************/
		// A B C 노드들이 있는데, A->C 거리보다 A->B + B->C가 더 짧다면? A-> = A->B + B->C 로 가정한다는 뜻.(이걸 반복해서 최단 거리로 만듦)
		// 1. 탐색을 안 한 가장 가까운 정점을 방문
		// 2. 그 정점과 이어진 정점을 방문하면서 최단거리 계산. 이 두개를 반복

		const int INF = 99999;

		public static void ShortestPath(in int[,] graph, in int start, out int[] distance, out int[] path)
		{
			int size = graph.GetLength(0);
			bool[] visited = new bool[size];	// 이미 가본 곳을 또 가면 안 되니깐 visited 만들어주기

			distance = new int[size];	// ?에서 ?까지 가는 데에 거리의 길이
			path = new int[size];		// ?를 가기 위해 거치는 노드
			for (int i = 0; i < size; i++)
			{
				distance[i] = graph[start, i];
				path[i] = graph[start, i] < INF ? start : graph[start, i];
			}
			for(int i = 0; i < size; i++)
			{
				// 1. 방문하지 않은 정점 중 가장 가까운 정점부터 탐색
				int next = -1;
				int minCost = INF;
				for(int j = 0; j < size; j++)
				{
					if (!visited[j] && distance[j] < minCost)   // 방문하지 않았으면서 제일 작은 거리보다 더 작다면
					{
						next = j;
						minCost = distance[j];
					}
				}
				if (next < 0)
					break;

				// 2. 직접 연결된 거리보다 거쳐서 더 짧아진다면 갱신(next를 거쳐서 더 짧아지는 거리가 있다면 갱신)
				for(int j = 0; j < size; j++)
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
				visited[next] = true;
			}
		}
	}
}
