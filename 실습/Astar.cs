using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 실습
{
	internal class Astar
	{
		// A* 알고리즘 
		// 다익스트라 알고리즘과는 다르게, 최적의 해를 찾는 것이 아닌 최단 경로의 근사값을 찾는다
		// 최단 경로를 찾는 방법은 f, g, h에 점수를 부여하여 f의 값이 가장 적은 정점부터 탐색하는 것이다.
		// 그래서 경로를 찾는 속도가 다익스트라에 비해 빠르다.

		// 교수님이 구현해주신 코드 따라 치면서 주석 달았습니다 ㅜㅜ
		const int CostStraight = 10;    // 상하좌우 가중치
		const int CostDiagonal = 14;    // 대각선 가중치

		static Point[] Direction =
		{
			new Point(0, +1),	// 상
			new Point(0, -1),	// 하
			new Point(+1, 0),	// 우
			new Point(-1, 0),	// 좌
		};

		// 경로가 있다 없다를 나타내주기 위한 bool 자료형 사용, 경로가 있으면 true
		public static bool PathFinding(bool[,] tileMap, Point start, Point end, out List<Point> path)
		{
			// 초기화 작업
			int ySize = tileMap.GetLength(0);
			int xSize = tileMap.GetLength(1);

			bool[,] visited = new bool[ySize, xSize];   // 그 좌표가 탐색 되었는지 안되었는지 여부(방문여부)
			ASNode[,] nodes = new ASNode[ySize, xSize]; // 위치에 대한 정점들
			PriorityQueue<ASNode, int> nextPointPQ = new PriorityQueue<ASNode, int>();
			// f에 의한 우선순위대로 탐색을 해야하니, 우선순위 큐를 활용한다. 노드만 집어 넣으면 우선순위에 맞게 꺼낸다.

			// 0. 시작정점을 생성하여 추가
			ASNode startNode = new ASNode(start, null, 0, Heuristic(start, end));
			nodes[startNode.point.y, startNode.point.x] = startNode;    // 시작노드 위치
			nextPointPQ.Enqueue(startNode, startNode.f);	// 다음노드는 f값으로 우선순위큐를 이용해 꺼낸다.

			while(nextPointPQ.Count > 0)	// 탐색할 다음노드가 0이 될 때까지 반복
			{
				// 1. 다음으로 탐색할 정점 꺼내기
				ASNode nextNode = nextPointPQ.Dequeue();

				// 2. 방문한 정점은 방문표시
				visited[nextNode.point.y, nextNode.point.x] = true;

				// 3. 탐색할 정점이 도착지인 경우. 도착했다고 판단하여 경로를 반환
				if(nextNode.point.x == end.x && nextNode.point.y == end.y)
				{
					Point? pathPoint = end;
					path = new List<Point>();

					// 도착지점에 도달했으면 이제 경로를 찾아야한다.(역순임)
					while(pathPoint != null)    // 부모정점이 없을 때까지 반복 (지금 정점이 start일 때까지 반복)
					{
						Point point = pathPoint.GetValueOrDefault();  // pathPoint 자체가 null일 수 있어서 GetValueOrDefault()붙임
						path.Add(point);    // path에 point를 추가한다.(경로)
						pathPoint = nodes[point.y, point.x].parent;
					}
					path.Reverse(); // 경로는 왔던 길을 뒤따라 가니까 path를 뒤집어야 한다. 역순이다.
					return true;
				}

				// 4. Astar 탐색을 진행
				for(int i =0; i< Direction.Length; i++)		// 탐색할 곳이 지금 정점으로부터 상,하,좌,우 총 4곳
				{
					int x = nextNode.point.x + Direction[i].x;
					int y = nextNode.point.y + Direction[i].y;

					// 4-1. 탐색하면 안 되는 경우 제외
					// 맵을 벗어났을 경우
					if (x < 0 || x >= xSize || y < 0 || y >= ySize)
						continue;
					// 탐색할 수 없는 정점일 경우
					else if (tileMap[y, x] == false)    // false는 갈 수 없는 정점
						continue;
					// 이미 방문한 정점일 경우
					else if (visited[x, y])
						continue;

					// 4-2. 탐색
					int g = nextNode.g + 10;    // g 점수 넣기
					int h = Heuristic(new Point(x, y), end);    // h 점수 넣기
					ASNode newNode = new ASNode(new Point(x, y), nextNode.point, g, h);

					// 4-3. 정점의 갱신이 필요한 경우 새로운 정점으로 할당
					if (nodes[y, x] == null || nodes[y, x].f > newNode.f)
					// 점수 계산이 되지 않은 정점이거나, 가중치가 더 높은 정점인 경우(내가 주려는 점수가 더 낮은경우)
					{
						nodes[y, x] = newNode;
						nextPointPQ.Enqueue(newNode, newNode.f);
					}
				}
			}
			// 여기까지 온 경우, 갈 수 있는 길이 없는 경우임. 못찾음(여기까지 온 경우, 갈 수 있는 길이 없는 경우임. 못찾음)
			path = null;    // 경로가 없고
			return false;   // 찾지 못했다.
		}

		static int Heuristic(Point start, Point end)
		{
			int xSize = Math.Abs(start.x - end.x);      // 가로로 가야하는 횟수
			int ySize = Math.Abs(start.y - end.y);  // 세로로 가야하는 횟수

			// 맨해튼 거리 : 가로 세로를 통해 이동하는 거리
			// return CostStraight * (xSize + ySize);

			// 유클리드 거리 : 대각선을 통해 이동하는 거리
			return CostStraight * (int)Math.Sqrt(xSize * xSize + ySize * ySize);
		}

		class ASNode	// 정점 만들어주기
		{
			public Point point;     // 현재 정점 위치
			public Point? parent;   // 이 정점을 탐색한 정점. 내가 어떤 정점에 의해 탐색 당했는지(나의 부모정점)
									// 부모 정점은 없을 수도 있기 때문에(null일수도) Point? 로 했다.
			public int f;           // f(x) = g(x) + h(x) : 총 거리
			public int g;           // 현재까지의 거리, 즉 지금까지 온 경로
			public int h;			// 휴리스틱 : 앞으로 예상되는 거리, 목표까지의 추정 경로 가중치

			public ASNode(Point point, Point? parent, int g, int h)		// f는 g+h 하면 나오기에 매개변수로X
			{
				this.point = point;
				this.parent = parent;
				this.g = g;
				this.h = h;
				this.f = g + h;
			}
		}
	}

	public struct Point     // 좌표 구조체 만들기(위치 정보)
	{
		public int x;   // x좌표
		public int y;   // y좌표

		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}
}
