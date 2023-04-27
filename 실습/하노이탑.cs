using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 실습
{
	internal class 하노이탑		// 하다가 포기했습니다..
	{
		static Stack<int>[] stick = new Stack<int>[3];         // 스택 형태의 stick(기둥)을 만든다. 크기는 3칸. 0, 1, 2 기둥이 있다.

		static void Move2(int count, int start, int end)
		{
			int other = 1;

			if (count == 0)
				return;

			if (count == 1)
			{
				int circle = stick[start].Pop();    // 첫번째 기둥에서 마지막 남은 원반을 뺀다.
				stick[end].Push(circle);        // 마지막 기둥에(2번째) 그 원반을 넣는다.
				Console.WriteLine($"{circle} {stick}");
				Move2(count - count, start, end);

			}

			Move2(count - 1, start, other);
			Move2(1, start, end);
			Move2(count - 1, other, end);
			// Move2(count - 2, start, other);

		}

		static void Main1()
		{

			for (int i = 0; i < 3; i++)
			{
				stick[i] = new Stack<int>();    // 각 스택 0, 1, 2 기둥을 만든다.
			}

			for (int i = 0; i < 4; i++) // 원반을 스택에다가 넣음
			{
				stick[0].Push(i);
			}

			Move2(3, 0, 2);
		}
	}
}
