using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Project_TextRPG
{
	public class Bug : Monster
	{
		// 애벌래인데.. 계~속 같은 곳만 빙글빙글 돌 거임
		public override void MoveAction()
		{

			// 함수가 한 번 실행 될 때마다 하나씩 실행돼야해
			// 밑의 Move들을 전부 다 한 곳에 넣어놓고 하나씩 꺼내게? (먼저 넣은 것부터 꺼내는 거니까 Queue)
			queue.Enqueue(Direction.Up);
			queue.Enqueue(Direction.Right);
			queue.Enqueue(Direction.Right);
			queue.Enqueue(Direction.Down);
			queue.Enqueue(Direction.Down);
			queue.Enqueue(Direction.Left);
			queue.Enqueue(Direction.Left);
			queue.Enqueue(Direction.Up);

			Move(queue.Dequeue());
		}
		Queue<Direction> queue = new Queue<Direction>();

	}
}
