using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TextRPG
{
	public class Player
	{
		public char icon = '♥'; // 플레이어 아이콘
		public Position pos;

		public void Move(Direction dir)		// 플레이어의 이동기능 만들어주기
		{
			Position prevPos = pos;
			// 플레이어 이동
			switch (dir)
			{
				case Direction.Up:
					pos.y--;
					break;
				case Direction.Down:
					pos.y++;
					break;
				case Direction.Left:
					pos.x--;
					break;
				case Direction.Right:
					pos.x++;
					break;
			}
		}
	}
}
