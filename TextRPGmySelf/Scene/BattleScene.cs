using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGmySelf
{
	public class BattleScene : Scene	// 몬스터랑 플레이어가 마주치면 배틀
	{
		public BattleScene(Game game) : base(game)
		{
		}

	

		public override void MapPrinting()
		{
			Console.WriteLine(player.icon +"                   ");
			Console.WriteLine(Monster.returnmonster.icon);
			
		}

		public override void Update()
		{
			throw new NotImplementedException();
		}

	}
}
