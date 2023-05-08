using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGmySelf
{
	public class Game
	{
		public static Scene curScene;         // 현재 씬
		MainScene mainScene;
		MapScene mapScene;
		bool isRunning;
		// 게임루프에는 초기화 - 랜더링 - 업데이트(입력도 포함) - 릴리즈 가 있음.
		public Game()
		{
			isRunning = true;
			mainScene = new MainScene(this);	// 여기서 this를 하는 이유는 Game에 대한 객체를 여러 개 만들지 않고 지금 이 Game만을 가리켜서 병행이 가능하도록
			mapScene = new MapScene(this);		// 또 public Game() 안에 생성해주는 이유는, this는 Game 클래스가 만들어지고 생성되기 때문에
												// 위에다가(멤버변수 두는 위치에다가) 넣으면 안됨. 거기는 this가 생기기 전임
		}

		public void Run()		// 게임루프
		{
			Init();
			while(isRunning)
			{
				Render();
				Update();
			}
			Release();
		}

		void Init()
		{
			curScene = mainScene;   // 첫 화면은 메인씬
		}

		public void GameStart()
		{
			Console.Clear();
			curScene = MapScene.level1;    // 게임시작하면 씬이 mapScene으로 변경
			curScene.MapPrinting();
		}

		void Render()	// 맵 프린팅
		{
			Console.Clear();
			curScene.MapPrinting();
		}

		void Update()	// 입력, 갱신
		{
			curScene.Update();
		}
		
		public void Release()
		{
			this.isRunning = false;  // 게임끝	
			return;	
		}
	}
}
