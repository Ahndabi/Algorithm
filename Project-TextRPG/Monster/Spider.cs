using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TextRPG
{
	internal class Spider : Monster
	{
		Position position = new Position();
		// 거미는 하나의 x에 고정하고 y만 움직이도록. 위~~~~~아래~~~~~~로 계속 왔다갔다 하도록 만들겠삼
		// x는 11로 고정
		public override void MoveAction()
		{
			position.x = 11;	// x는 11로 고정
			
		}
	}
}
