using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13.PathFinding
{
	internal class Tilemap
	{
		/******************************************************
		 * 타일맵 (Tilemap)
		 * 
		 * 2차원 평면의 그래프를 정점이 아닌 위치를 통해 표현하는 그래프
		 * 위치의 이동가능 유무만을 가지는 타일맵과,
		 * 타일의 종류를 표현한 이차원 타일맵이 있음
		 * 인접한 이동가능한 위치간 간선이 있으며 가중치는 동일함
		 ******************************************************/
		// 인접해 있는 한 칸씩은 모두 갈 수 있다 라고 구현. (한 칸 띄어서 갈 수 없음. 좌표가 2 이상 차이나면 못감)
		// 그래서 bool을 이용해서 이동 가능 유무를 표현해서 구현 가능(다른 방식으로도 가능)
		// 연속적으로만 이동가능.


		// <타일맵 그래프>
		// 예시 - 위치의 이동가능 표현한 이차원 타일맵
		bool[,] tileMap1 = new bool[7, 7]
		{
			{ false, false, false, false, false, false, false },
			{ false,  true,  true, false, false, false, false },
			{ false, false,  true, false, false,  true, false },
			{ false, false,  true,  true,  true,  true, false },
			{ false, false,  true, false, false, false, false },
			{ false, false,  true,  true,  true,  true, false },
			{ false, false, false, false, false, false, false },
		};
		/*
		 * ■ ■ ■ ■ ■ ■ ■
		 * ■   ■     ■ ■ ■
		 * ■   ■     ■	 ■
		 * ■   ■			 ■
		 * ■   ■     ■ ■ ■
		 * ■				 ■
		 * ■ ■ ■ ■ ■ ■ ■
		 */



		// 예시 - 타일의 종류를 표현한 이차원 타일맵
		enum TileType
		{
			None = ' ',
			Wall = '■',
			Path = '*',
			Start = 'S',
			Goal = 'E',
		}

		char[,] tileMap2 = new char[9, 9]
		{
			{ '■', '■', '■', '■', '■', '■', '■', '■', '■' },
			{ '■', 'S', '■', '■', ' ', ' ', '■', '■', '■' },
			{ '■', '*', '■', '■', ' ', '■', '■', ' ', '■' },
			{ '■', '*', '■', '■', ' ', '■', '■', ' ', '■' },
			{ '■', '*', '■', '*', '*', '*', '*', '*', '■' },
			{ '■', '*', '■', '*', '■', '■', '■', '*', '■' },
			{ '■', '*', '■', '*', '■', '■', '■', '*', '■' },
			{ '■', '*', '*', '*', '■', '■', '■', 'E', '■' },
			{ '■', '■', '■', '■', '■', '■', '■', '■', '■' },
		};
		/*
		 * ■ ■ ■ ■ ■ ■ ■ ■ ■
		 * ■ S  ■ ■		 ■ ■ ■
		 * ■ *  ■ ■	  ■ ■    ■
		 * ■ *  ■ ■    ■ ■    ■
		 * ■ *  ■ * * * * * * *  ■
		 * ■ *  ■ *  ■ ■ ■ *  ■
		 * ■ *  ■ *  ■ ■ ■ *  ■
		 * ■ * * * *  ■ ■ ■ E  ■ 
		 * ■ ■ ■ ■ ■ ■ ■ ■ ■
		 */

	}
}
