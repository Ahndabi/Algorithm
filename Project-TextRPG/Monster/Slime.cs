using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TextRPG
{
	internal class Slime : Monster
	{
		int moveCount;

		public override void MoveAction()
		{
			if(moveCount++ % 3 != 0)		// 3턴 마다 움직이도록
				return;

			Random random = new Random();
			switch (random.Next(0, 4))		// 랜덤으로 4방향 중 아무방향으로 움직인다.
			{
				case 0:
					Move(Direction.Up);
					break;
				case 1:
					Move(Direction.Down);
					break;
				case 2:
					Move(Direction.Left);
					break;
				case 3:
					Move(Direction.Right);
					break;
			}
		}
	}
}
