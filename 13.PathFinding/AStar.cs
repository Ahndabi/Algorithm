using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13.PathFinding
{
	internal class AStar
	{
		/******************************************************
		 * A* 알고리즘
		 * 
		 * 다익스트라 알고리즘을 확장하여 만든 최단경로 탐색알고리즘
		 * 경로 탐색의 우선순위를 두고 유망한 해부터 우선적으로 탐색 (가장 가까운 정점부터 탐색하는 것이 아님)
		 * 출발지에서 목적지까지 가는 최단 경로를 찾아내기 위해 고안된 알고리즘
		 ******************************************************/
		// 게임에서는 다익스트라보다 A*가 더 빠름(경우의 수를 줄였기 때문에)
		// 다익스트라는 가중치 없이 전방향으로 전체적으로 탐색하는데 A*는 아님!!
		// 더 유망한 경로를 선정해서 ㄱㄱ (점수를 매겨서 유망한 것을 선택)
		// A*는,
		// 1. f값이 가장 작은 정점부터 탐색을 해서
		// 2. 탐색한 정점의 f값을 구해줌

		// f, g, h		// 점수를 매기는 데 필요한 것들
		// f = g + h	// 총 점수 (f가 가장 낮은 값부터 탐색) (경로에 따라 달라짐)
		// g = 지금까지 온 거리 (걸린 거리. 실제로 왔던 거리)
		// h = 예상거리 (앞으로 갈 예상거리) (휴리스틱)		// 얘가 제일 중요
		// h(휴리스틱)을 어케 설정하냐에 따라 효율성이 달라짐 (대각선으로 이동할수록 효율성이 좋아짐 - 유클리드 거리)
		// 대각선의 효율성을 이용함. 1 : 1 : 루트2 자나. 루트2가 대략 1.414....인데 소수점으로 계산하면 힘드니까
		// 한 칸을 1이 아니라 10으로 잡아줌. 그래서 한 칸 이동은 10으로 계산, 대각선은 14로 계산해줌
		// f, g, h 모두 작을수록 좋은 거임~!


		// 다익스트라와 A* 알고리즘의 차이점?
		// A*알고리즘은 완전한 최단 경로를 찾지 않고 최단 경로의 근사값을 찾아내는 것을 목표로 함.
		// 가까운 노드부터 순차적으로 모두 방문하는 것이 아닌, 휴리스틱 함수를 통해 추정하여 매긴 점수를 토대로 탐색함.
		// 즉, 정확한 정답을 포기한 대신, 탐색 속도를 다익스트라보다 더 높임~!

		// A*도 다익스트라처럼 거쳐온 노드들을 path로 나타내면 좋은데, 얘는 역순으로 따라가보면 경로가 나온대,,?
		// 만약 중간에 장애물이 있으면? - 일단 장애물이 없다고 가정하고(장애물 뚫음) 예상거리 정함.

		const int CostStraight = 10;
		const int CostDiagonal = 14;

		static Point[] Direction =
		{
            new Point(  0, +1 ),			// 상
			new Point(  0, -1 ),			// 하
			new Point( -1,  0 ),			// 좌
			new Point( +1,  0 ),			// 우
			//new Point( -1, +1 ),		    // 좌상
			//new Point( -1, -1 ),		    // 좌하
			//new Point( +1, +1 ),		    // 우상
			//new Point( +1, -1 )				// 우하
		};


		public static bool PathFinding(bool[,] tileMap, Point start, Point end, out List<Point> path)
		{   // 경로가 있다없다를 나타내주기 위한 bool 자료형 사용. 경로가 있으면 true

			// 초기화 작업해주기
			int ySize = tileMap.GetLength(0);
			int xSize = tileMap.GetLength(1);

			bool[,] visited = new bool[ySize, xSize];   // 그 좌표가 탐색이 되었는지 안되었는지(방문여부)
			ASNode[,] nodes = new ASNode[ySize, xSize];	// 위치에 대한 정점들
			PriorityQueue<ASNode, int> nextPointPQ = new PriorityQueue<ASNode, int>();
			// f로 우선순위대로 컨트롤 해줘야하니, 우선순위 큐 활용. 내가 노드만 집어넣으면 우선순위가 먼저 꺼내지는 정점은 f가 낮은 순서대로인 거겠지

			// 0. 시작정점을 생성하여 추가
			ASNode startNode = new ASNode(start, null, 0, Heuristic(start, end));
			nodes[startNode.point.y, startNode.point.x] = startNode;
			nextPointPQ.Enqueue(startNode, startNode.f);

			while (nextPointPQ.Count > 0)
			{
				// 1. 다음으로 탐색할 정점 꺼내기
				ASNode nextNode = nextPointPQ.Dequeue();

				// 2. 방문한 정점은 방문표시
				visited[nextNode.point.y, nextNode.point.x] = true;

				// 3. 탐색할 정점이 도착지인 경우. 도착했다고 판단해서 경로 반환
				if(nextNode.point.x == end.x && nextNode.point.y == end.y)
				{
					Point? pathPoint = end;
					path = new List<Point>();

					while(pathPoint != null) // 부모정점이 없을 때까지(지금 정점이 start일 때까지. 시작지점까지 갈 때까지)
					{
						Point point = pathPoint.GetValueOrDefault();	// pathPoint 자체가 null일 수 있어서 GetValueOrDefault()붙임
						path.Add(point);
						pathPoint = nodes[point.y, point.x].parent;
					}
					path.Reverse();     // 경로는 왔던 길을 뒤따라 가니까, path를 뒤집어야 경로가 나옴
					return true;
				}

				// 4. Astar 탐색을 진행
				for(int i = 0; i < Direction.Length; i++)
				{
					int x = nextNode.point.x + Direction[i].x;
					int y = nextNode.point.y + Direction[i].y;

					// 4-1. 탐색하면 안 되는 경우 제외
					// 맵을 벗어났을 경우
					if (x < 0 || x >= xSize || y < 0 || y >= ySize)
						continue;
					// 탐색할 수 없는 정점일 경우
					else if (tileMap[y, x] == false)    // false는 갈 수 없는 정점.
						continue;
					// 이미 방문한 정점일 경우
					else if (visited[x, y])
						continue;

					// 4-2. 탐색
					int g = nextNode.g + 10;	// g 점수 넣기
					int h = Heuristic(new Point(x, y), end);	// h 점수 넣기
					ASNode newNode = new ASNode(new Point(x, y), nextNode.point, g, h);	// 새 노드 만들어서 정점 정보 넣기

					// 4-3. 정점의 갱신이 필요한 경우 새로운 정점으로 할당
					if (nodes[y,x] == null || nodes[y,x].f > newNode.f)	
						// 점수 계산이 되지 않은 정점이거나, 가중치가 더 높은 정점인 경우(내가 주려는 점수가 더 낮은경우)
					{
						nodes[y, x] = newNode;
						nextPointPQ.Enqueue(newNode, newNode.f);
					}
				}
			}
			// 여기까지 온 경우, 갈 수 있는 길이 없는 경우임. 못찾음(여기까지 온 경우, 갈 수 있는 길이 없는 경우임. 못찾음)
			path = null;	// 경로가 없고
			return false;	// 찾지 못했다.
		}

		// 휴리스틱 : 최상의 경로를 추정하는 순위값, 휴리스틱에 의해 경로탑색 효율이 결정됨
		static int Heuristic(Point start, Point end)   // 휴리스틱을 구해주는 함수
		{
			int xSize = Math.Abs(start.x - end.x);  // 가로로 가야하는 횟수
			int ySize = Math.Abs(start.y - end.y);  // 세로로 가야하는 횟수

			// 맨해튼 거리 : 가로 세로를 통해 이동하는 거리
			// return CostStraight * (xSize + ySize);

			// 유클리드 거리 : 대각선을 통해 이동하는 거리
			return CostStraight * (int)Math.Sqrt(xSize * xSize + ySize * ySize);
		}



		class ASNode   // 정점 만들어주기
		{
			public Point point;    // 현재 정점 위치
			public Point? parent;  // 이 정점을 탐색한 정점. 내가 어떤 정점에 의해 탐색 당했는지(나의 부모에 대한 정보) 
			//     parent는 null일수도 있어서 Point?로 함
			public int f;		   // f(x) = g(x) + h(x) : 총 거리
			public int g;		   // 현재까지의 거리, 즉 지금까지 온 경로 가중치
			public int h;	       // 휴리스틱 : 앞으로 예상되는 거리, 목표까지 추정 경로 가중치

			public ASNode(Point point, Point? parent, int g, int h)	// f는 g랑 h만 있으면 나오니까 매개변수에 포함X
			{
				this.point = point;
				this.parent = parent;
				this.g = g;
				this.h = h;
				this.f = g + h;
			}
		}
	}

	public struct Point		// 좌표 만들어주기 (위치 정보)
	{ // 구조체는 빈 데이터를 가질 수 없대
		public int x;
		public int y;

		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}
}
// 질문 1. 실제로 이동할 땐 상하좌우로만 가능한데 대각선의 가중치를 고려하는 이유는?
//		  ㄴ 안그러면 경우의 수가 너무 많아져서. 맨해튼거리에서 유클리드 거리는 단 하나만 있음. 그래서 경우의 수를 줄여줌
