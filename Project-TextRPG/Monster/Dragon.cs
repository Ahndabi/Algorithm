using Project_TextRPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Project_TextRPG
{
	public class Dragon : Monster	// 플레이어를 추적하여 드래곤 만들기
	{
		int moveCount;
		public override void MoveAction()
		{
			// 매 반복마다 무브카운트가 1씩 추가되고 무브카운트를 2로 나눈 나머지가 0일때만 움직이게 하는 조건문입니다.
			if (moveCount++ % 2 != 0)
				return;

			List<Point> path;
			bool result = Astar.PathFinding(Data.map, new Point(pos.x, pos.y),
				new Point(Data.player.pos.x, Data.player.pos.y), out path);

			if (!result)	// 플레이어에게 향하는 길이 없을 수도 있으니 안움직이도록
				return;

			if (path[1].x == pos.x)
			{
				if (path[1].y == pos.y - 1)
					Move(Direction.Up);
				else
					Move(Direction.Down);
			}
			else
			{
				if (path[1].x == pos.x - 1)
					Move(Direction.Left);
				else
					Move(Direction.Right);
			}

		}
	}
}
