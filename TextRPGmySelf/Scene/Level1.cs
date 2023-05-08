using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGmySelf
{
	public class Level1 : Scene
	{
		public Player player;
		Slime slime;
		List<Monster> monsters = new List<Monster>();

		public Level1(Game game) : base(game)
		{
			slime = new Slime(8, 8);
			player = new Player(2, 2);
		}

		public override void MapPrinting()
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
				{ false,  true, true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false },
				{ false, false, false, false, false, false, false, false, false, false, false, false, false, false },
			};

			StringBuilder sb = new StringBuilder();

			for(int y = 0; y < map.GetLength(0); y++)
			{
				for(int x = 0; x < map.GetLength(1); x++)
				{
					if (map[y, x] == false)			// 벽
						sb.Append('▩');

					else if (map[y, x] == true)		// 길
						sb.Append('　');
				}
				sb.AppendLine();
			}
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(sb.ToString());


			Console.ForegroundColor = ConsoleColor.Red;
			Console.SetCursorPosition(player.pos.x * 2, player.pos.y);
			Console.WriteLine(player.icon);					// 플레이어도 그려주기
			Console.CursorVisible = false;

			
			monsters.Add(slime);
			foreach (Monster monster in monsters)           // 몬스터 그려주기
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.SetCursorPosition(monster.pos.x * 2, monster.pos.y);
				Console.Write(monster.icon);
			}
		}

		public override void Update()
		{
			player.InputKey();
			slime.MoveAction();
		}
	}
}
