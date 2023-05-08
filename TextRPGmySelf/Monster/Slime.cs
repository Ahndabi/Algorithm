using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGmySelf
{
	public class Slime : Monster
	{
		public Slime(int x, int y) : base(x, y)
		{
			icon = '▼';
			pos.x = x; pos.y = y;
			monsters.Add(this);
		}

		public override void MoveAction()	// 랜덤으로 움직이기
		{
			if (moveCount++ % 2 == 0)		// 2턴마다 움직이기
				return;

			Position prePos = pos;

			Random ran = new Random();
			int input = ran.Next(4);
			switch(input)
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

			if (!MapScene.map[pos.y, pos.x])	// 벽 통과하지 못하도록
				pos = prePos;
		}
	}
}
