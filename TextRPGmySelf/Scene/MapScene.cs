using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGmySelf
{
	public class MapScene : Scene
	{
		public static Level1 level1;
		Scene battleScene;
		public static Monster returnmonster;

		public MapScene(Game game) : base(game)
		{
			level1 = new Level1(game);
		}

		public override void MapPrinting()
		{
			level1.MapPrinting();
		}

		public override void Update()
		{
			level1.Update();

			// error : player가 null이래
			if (this.pos.x == level1.player.pos.x && this.pos.y == level1.player.pos.y)        // 만약, 플레이어랑 몬스터랑 위치가 같다면
			{
				Game.curScene = battleScene;							// 배틀씬으로 넘어갑니다
				returnmonster = ReturnMonster();                        // 몬스터를 반환하는 함수
			}
		}

		public Monster ReturnMonster()
		{
			// 만약 몬스터의 위치와 플레이어의 위치가 같다면?
			// 몬스터는 monsters 배열에 여러 몬스터가 있을 테니까 몬스터 하나하나를 반복기로 돌리며 플레이어와 위치를 비교
			foreach (Monster monster in Monster.monsters)
			{
				if (monster.pos.x == player.pos.x && monster.pos.y == player.pos.y)
					return monster;
			}
			return default;
		}
	}
}
