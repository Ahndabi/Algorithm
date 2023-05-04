using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 여기는 자주 사용할 구조체 같은거?
namespace Project_TextRPG
{
	public struct Position
	{
		public int x;
		public int y;

		public Position(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}

	public enum Direction { Left, Right, Up, Down }		// 방향키
}
