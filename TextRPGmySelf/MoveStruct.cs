using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGmySelf
{
	// 방향키
	public enum Direction { Left, Right, Up, Down }

	// x, y좌표
	public struct Position
	{
		public int x, y;
		public Position(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}
}
