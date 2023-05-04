using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TextRPG
{
	// 상하좌우로 움직이는 맵씬. 소코반에서 많이 따왔대

	internal class MapScene : Scene
	{
		public MapScene(Game game) : base(game) 
		{
		}

		public override void Render()
		{
			PrintMap();
		}

		public override void Update()
		{
			// 플레이어 움직여주기
			// 입력하는 데로 바로바로 움직여야 하니까 리드라인보다는 리드키
			ConsoleKeyInfo input = Console.ReadKey();
			switch(input.Key)
			{
				case ConsoleKey.UpArrow:    // 플레이어가 위로 이동
					Data.player.Move(Direction.Up);
					break;
				case ConsoleKey.DownArrow:
					Data.player.Move(Direction.Down);
					break;
				case ConsoleKey.LeftArrow:
					Data.player.Move(Direction.Left);
					break;
				case ConsoleKey.RightArrow:
					Data.player.Move(Direction.Right);
					break;
			}

			// 플레이어 몬스터 접근
			Monster monsterInpos = Data.MonsterInPos(Data.player.pos);
			if (monsterInpos != null)
			{
				// 전투시작
				game.BattleStart(monsterInpos);
				return;
			}

			// 몬스터의 이동 갱신
			foreach (Monster monster in Data.monsters)		// 데이터에 있는 몬스터 가져옴
			{
				monster.MoveAction();
				if(monster.pos.x == Data.player.pos.x &&	// 몬스터랑 플레이어랑 위치가 같으면?
					monster.pos.y == Data.player.pos.y )
				{
					// 전투시작
					game.BattleStart(monster);
					return;
				}
			}
		}

		void PrintMap()		// 맵 데이터를 기반으로 맵을 그려줌
		{
			Console.ForegroundColor = ConsoleColor.White;
			StringBuilder sb = new StringBuilder();		// 연속적으로 찍을거라 스트링빌더 사용
			for(int y = 0; y < Data.map.GetLength(0); y++)
			{
				for(int x = 0; x < Data.map.GetLength(1); x++)
				{
					if (Data.map[y, x])		// 움직일 수 있는 true 지역이라면,
						sb.Append('　');
					else   // false 지역이라면,
						sb.Append('▦');		// 벽 : X
				}
				sb.AppendLine();
			}
			Console.WriteLine(sb.ToString());

			Console.ForegroundColor = ConsoleColor.Green;
			
			foreach(Monster monster in Data.monsters)
			{
				Console.SetCursorPosition(monster.pos.x*2, monster.pos.y);	// 윈도우 10은 x에 *2
				Console.Write(monster.icon);
			}

			// 맵이 다 그려진 후에 플레이어 그려주기
			Console.ForegroundColor = ConsoleColor.Red;
			Console.SetCursorPosition(Data.player.pos.x*2, Data.player.pos.y); // 여기두 *2
			Console.Write(Data.player.icon);
		}
	}
}
