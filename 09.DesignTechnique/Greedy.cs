using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09.DesignTechnique
{
	internal class Greedy
	{
		/******************************************************
		 * 탐욕 알고리즘 (Greedy Algorithm)
		 * 
		 * 문제를 해결하는데 사용되는 근시안(짧은시야)적 방법
		 * 문제를 직면한 당시에 당장 최적인 답을 선택하는 과정을 반복
		 * 단, 반드시 최적의 해를 구해준다는 보장이 없음(다만 굉장히 빠름)
		 ******************************************************/
		// 뒷 생각 안하고 일단 지금 최적의 선택을 하는 것
		// 빨리빨리 처리해서 당장의 효율을 보는 것
		// 빠르고 직관적이지만 최적의 해를 구해준다는 보장은 없다.

		// 예시 - 자판기 거스름돈
		void CoinMachine(int money)
		{
			int[] coinType = { 500, 100, 50, 10, 5, 1 };

			foreach (int coin in coinType)
			{
				while (money <= coin)
				{
					Console.WriteLine("{0} 코인 배출", coin);
					money -= coin;
				}
			}
		}
		// 500원부터 일단 다 뽑아 - 더 이상 뽑을 수 없어 - 그럼 100원 다 뽑아 - 더 이상 뽑을 수 없어 -
		// 그럼 50원 다 뽑아 .....이런 식으로 최적의 거스름돈을 먼저 고려함


	}
}
