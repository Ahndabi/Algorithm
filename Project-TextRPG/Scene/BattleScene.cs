using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TextRPG
{
	internal class BattleScene : Scene
	{

		private Monster monster;
		public BattleScene(Game game) : base(game) 
		{
		}
		public override void Render()
		{
			StringBuilder sb = new StringBuilder();
			// 문자열 일일이 콘솔출력으로 하기 힘드니까 이렇게 다 넣어놓기
			sb.AppendLine("엄청난 몬스터를 만났다.");
			sb.AppendLine("1. 공격하기");
			sb.AppendLine("2. 도망가기");
			sb.Append("행동을 선택하세요 :");

			Console.Write(sb.ToString());
		}

		public override void Update()
		{
			string input = Console.ReadLine();
		}
		
		public void BattleStart(Monster monster)
		{
			this.monster = monster;
			Data.monsters.Remove(monster);

			Console.Clear();
			Console.WriteLine("전투 시작!!!");
			Thread.Sleep(1000);	// 1초 동안 전투시작! 이 문구를 보여줌
		}
	}
}
