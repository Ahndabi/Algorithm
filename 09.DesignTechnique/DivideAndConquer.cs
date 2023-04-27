using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09.DesignTechnique
{
	internal class DivideAndConquer
	{
		/******************************************************
		 * 분할정복 (Divide and Conquer)	(or 분할정복병합 이라고도 함)
		 * 
		 * 큰 문제를 작은 문제로 나눠서 푸는 하향식 접근 방식
		 * 분할을 통해서 해결하기 쉬운 작은 문제로 나눈 후 정복한 후 병합하는 과정
		 ******************************************************/
		// 자주 쓰는 기법 중 하나임!
		// 문제를 제대로 분할할 수 있을 때 사용.
		// 큰 문제를 작은 여러 문제로 나눠서(분할) 풀이(정복) - 풀이한 것을 다 합침(병합) - 전체가 해결됨!
		// 간단한 명령어로도 전체 만들기 가능

		// 예시 1 - 폴더 삭제
		struct Directory
		{
			public List<Directory> childDir;	// 하나의 폴더 안에 여러 폴더들이 있음
		}
		void RemoveDir(Directory directory)		// 하나의 폴더를 지우려면 폴더 안에 있는 폴더들도 다 지워야함.
		{
			foreach (Directory dir in directory.childDir)
			{
				RemoveDir(dir);		// 그 안에 폴더들을 모두 지우고 그 다음 메인 폴더를 지워야 함
			}

			Console.WriteLine("폴더 내 파일 모두 삭제");
		}

		// 예시 2 - 제곱 계산
		public int Pow(int x, int n)
		{
			// x^n = (x * x)^(n / 2)

			if (n == 0)
				return 1;
			else if (n % 2 == 0)
				return Pow(x * x, n / 2);
			else
				return x * Pow(x * x, (n - 1) / 2);
		}
	}
}
