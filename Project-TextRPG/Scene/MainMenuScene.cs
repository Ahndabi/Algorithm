using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TextRPG
{
	internal class MainMenuScene : Scene
	{
		public MainMenuScene(Game game) : base(game)
		{ 
		}

		public override void Render()
		{
			StringBuilder sb = new StringBuilder();
			// 문자열 일일이 콘솔출력으로 하기 힘드니까 이렇게 다 넣어놓기
			sb.AppendLine("1. 게임시작");
			sb.AppendLine("2. 게임종료").AppendLine();
			sb.Append("메뉴를 선택하세요 :");
			Console.Write(sb.ToString());
		}

		public override void Update()
		{
			string input = Console.ReadLine();

			int command;
			if(!int.TryParse(input, out command))	// 숫자가 문자로 변환이 불가능하다면?
			{
				// 잘못 입력한대로 되면 안 되니까.
				// 입력을 잘못했는데 그대로 진행되면 안 되니까
				Console.WriteLine("잘못 입력 하셨습니다.");
				Thread.Sleep(1000);		// 1초 동안 프로그램 잠깐 멈춰! (Thread : 프로그램을 돌리는 단위)
				// 1초 동안 "잘못입력하셨습니다." 라는 문구가 나오고 다시 돌아감
				return;
			}
			switch(command)
			{
				case 1: // 게임시작
					game.GameStart();
					Console.WriteLine("게임시작");
					Thread.Sleep(1000);
					break;
				case 2:	// 게임종료
					game.GameOver();
					Console.WriteLine("게임종료");
					Thread.Sleep(1000);
					break;
				default:
					Console.WriteLine("잘못 입력 하셨습니다.");
					Thread.Sleep(1000);
					break;
			}
		}
	}
}
