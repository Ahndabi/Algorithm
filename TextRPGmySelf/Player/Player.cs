using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGmySelf
{
	public class Player
	{
		public Direction dir;		// 방향키
		public Position pos;        // x, y
		public char icon = '♥';

		public Player(int x, int y)
		{
			pos.x = x;
			pos.y = y;
		}

		public void Move(Direction dir)		// 플레이어 움직이는 함수
		{
			Position prePos = pos;
			switch(dir)
			{
				case Direction.Left:
					pos.x--;
					break;
				case Direction.Right:
					pos.x++;
					break;
				case Direction.Up:
					pos.y--;
					break;
				case Direction.Down:
					pos.y++;
					break;
			}
			
			if (!Scene.map[pos.y, pos.x])		// 만약 벽이라면 가지 못하도록!
			{
				pos = prePos;
			}
		}

		public void InputKey()		// 키 입력받는 함수
		{
			ConsoleKeyInfo info = Console.ReadKey();	// 방향키 하나를 입력받음

			switch (info.Key)
			{
				case ConsoleKey.LeftArrow:
					Move(Direction.Left);
					break;
				case ConsoleKey.RightArrow:
					Move(Direction.Right);
					break;
				case ConsoleKey.UpArrow:
					Move(Direction.Up);
					break;
				case ConsoleKey.DownArrow:
					Move(Direction.Down);
					break;
				default:
					return;
			}
		}
	}
}
