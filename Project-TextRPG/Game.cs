using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Project_TextRPG
{
	public class Game
	{
		bool running = true;    // 게임이 돌아가는 중이냐?

		Scene curScene;         // 현재 진행되고 있는 씬
		MainMenuScene mainMenuScene;	
		MapScene mapScene;
		InventoryScene inventoryScene;
		BattleScene battleScene;

		public void Run()	// 게임 동작시키기
		{
			// 초기화
			Init();

			// 게임 루프 (textRPG게임 순서는 보통 랜더링-입력-갱신임)
			while(running)
			{
				// 랜더링(표현)
				Render();
				// 갱신 + 입력
				Update();
			}

			// 마무리
			Release();
		}

		void Init()
		{
			Console.CursorVisible = false;		// 커서 안 보이게
			Data.Init();

			mainMenuScene = new MainMenuScene(this);
			mapScene = new MapScene(this);
			inventoryScene = new InventoryScene(this);
			battleScene = new BattleScene(this);

			curScene = mainMenuScene;   // 제일 먼저 시작해야할 씬. 메인메뉴지
		}

		public void GameStart()	// 
		{
			Data.LoadLevel1();
			curScene = mapScene;		// 게임시작하면 맵씬으로 넘어가기
		}

		public void GameOver()
		{
			running = false;    // 게임루프 종료

			StringBuilder sb = new StringBuilder();

			// 여기 밑에 텍스트들은 게임오버 간지나게 하려고 그냥 만든거
			sb.AppendLine();
			sb.AppendLine("  ***    *   *   * *****       ***  *   * ***** ****  ");
			sb.AppendLine(" *      * *  ** ** *          *   * *   * *     *   * ");
			sb.AppendLine(" * *** ***** * * * *****      *   * *   * ***** ****  ");
			sb.AppendLine(" *   * *   * *   * *          *   *  * *  *     *  *  ");
			sb.AppendLine("  ***  *   * *   * *****       ***    *   ***** *   * ");
			sb.AppendLine();
			sb.AppendLine();
			Console.WriteLine(sb.ToString());
		
		}

		public void BattleStart(Monster monster)
		{
			curScene = battleScene;		// 배틀씬으로 바뀜
		}

		void Render()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			curScene.Render();		// 여기 Render랑 Game클래스의 이 함수이름 Render랑은 다름. 여기 Render는 씬에 있는 렌더임
		}

		void Update()
		{
			curScene.Update();
		}
		void Release()
		{
		}
	}
}
