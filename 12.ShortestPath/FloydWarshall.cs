using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12.ShortestPath
{
	internal class FloydWarshall
	{
		/******************************************************
		 * 플로이드-워셜 알고리즘 (Floyd-Warshall Algorithm)
		 * 
		 * 모든 노드에서 출발하여 다른 노드로 가는 각각의 최단 경로를 구함
		 * 모든 노드를 거쳐가며 최단 거리가 갱신되는 조합이 있을 경우 갱신
		 ******************************************************/
		// 이 그래프에서 각각의 정점에서(모든 정점에서) 다른 정점까지 가는 최단경로를 구함
		// 그냥 전체 싹 다 돌림! 전부 체크해주면서 가장 짧은 거리를 계속 갱신(속도가 좀 느림)
		// 상대적으로 중요도가 낮음

		// 다익스트라처럼 짧은 것부터 안 해도 돼. 어차피 다 할 거니까		
		// costTable, pathTable = 비용, 거리(가중치) 테이블, 각 정점에 대한 방문자 저장용 테이블 
		public static void ShortestPath(int[,] graph, out int[,] costTable, out int[,] pathTable)
		{
			// 시작과 도착을 둘 다 선정을 하고 거기서 다 도는 방식
			int size = graph.GetLength(0);
			costTable = new int[size, size];
			pathTable = new int[size, size];

			// 전체 2차원 배열들을 모두 돌앗
			for (int y = 0; y < size; y++)
			{
				for (int x = 0; x < size; x++)
				{
					costTable[y, x] = graph[y, x];
					pathTable[y, x] = -1;
				}
			}

			for (int middle = 0; middle < size; middle++)
			{
				for (int start = 0; start < size; start++)	// 시작부터
				{
					for (int end = 0; end < size; end++)	// 끝까지
					{
						if (costTable[start, end] > costTable[start, middle] + costTable[middle, end])	// 더 크다면
						{
							costTable[start, end] = costTable[start, middle] + costTable[middle, end];	// 그거로 교체
							pathTable[start, end] = middle;		// 거쳐가는 부분은 middle
						}
					}
				}
			}
		}
	}
}
