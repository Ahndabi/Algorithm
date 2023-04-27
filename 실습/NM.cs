using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NM
{
	internal class NM	// 완성을 못했습니다.......
	{                           // 4     2
		static void GetArray(int N, int M)  // (1 <= M <= N <= 7)
		{
			for (int i = 1; i <= N; i++)
			{
				for (int j = 1; j <= N; j++)        // 줄바꿈
				{
					for (int k = 1; k < 2; k++)
					{
						Console.Write(i);
						Console.Write(" ");
						Console.Write(j);
					}
					Console.WriteLine();
				}
			}
		}

		static void Main()
		{
			GetArray(4, 2);
		}
	}
}
