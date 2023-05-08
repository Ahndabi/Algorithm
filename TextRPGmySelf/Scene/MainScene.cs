using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGmySelf
{
	public class MainScene : Scene      // 게임 시작 첫 화면.
	{
		public MainScene(Game game) : base(game)
		{
		}

		public override void MapPrinting()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("게임을 시작하시겠습니까?");
			sb.AppendLine("1. 게임시작");
			sb.AppendLine("2. 게임종료");
			sb.AppendLine();
			sb.Append("입력하세요 :");
			Console.Write(sb.ToString());

		}

		public override void Update()
		{
			string input = Console.ReadLine();

			if (input != "1" && input != "2")       // 예외처리. 입력받은 키가 1이나 2가 아니라면
			{
				Console.WriteLine();
				Console.WriteLine("잘못 입력하셨습니다.");
				Thread.Sleep(1000);
			}
			else if (input == "1")  // 게임시작
			{
				Thread.Sleep(500);
				game.GameStart();
			}
			else if (input == "2")  // 게임종료
			{
				Console.WriteLine("-------------게임종료-------------");
				game.Release();
			}
		}
	}
}
