using Project_TextRPG;
using Project_TextRPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TextRPG
{
	public static class Data	// 맵, 플레이어 등,, 같은 것들은 첨부터 끝까지 있는 게 좋으니, static으로?
	{
		public static Player player;
		public static bool[,] map;
		public static List<Monster> monsters;

		public static void Init()
		{
			player = new Player();
			monsters = new List<Monster>();
		}

		public static void LoadLevel1()
		{
			map = new bool[,]
			{
				{ false, false, false, false, false, false, false, false, false, false, false, false, false, false },
				{ false,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false },
				{ false,  true,  true,  true,  true, false,  true,  true,  true,  true, false, false,  true, false },
				{ false,  true,  true,  true,  true, false,  true,  true,  true,  true, false,  true,  true, false },
				{ false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true, false },
				{ false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false },
				{ false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false },
				{ false,  true,  true,  true, false, false, false, false,  true,  true,  true,  true,  true, false },
				{ false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false },
				{ false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false },
				{ false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false },
				{ false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false },
				{ false,  true, false,  true,  true,  true,  false,  true,  true,  true,  true, false,  true, false },
				{ false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false },
				{ false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false },
				{ false, false, false, false, false, false, false, false, false, false, false, false, false, false },
			};

			player.pos = new Position(2, 2);

			Monster slime1 = new Slime();
			slime1.pos = new Position(4, 5);
			monsters.Add(slime1);

			Monster dragon = new Dragon();
			dragon.pos = new Position(10, 7);
			monsters.Add(dragon);

			Monster bug = new Bug();
			bug.pos = new Position(5, 12);
			monsters.Add(bug);

			Monster spider = new Spider();
			spider.pos = new Position(11, 2);	
			monsters.Add(spider);
		}

		public static Monster MonsterInPos(Position pos)    // 그 위치에 있는 몬스터를 가져오기
		{
			// x위치도 똑같고 y위치도 똑같은 몬스터
			Monster monster = monsters.Find(target => target.pos.x == pos.x && target.pos.y == pos.y);
			return monster;
		}
	}
}
