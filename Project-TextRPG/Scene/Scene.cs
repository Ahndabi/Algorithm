using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TextRPG		// 네임스페이스에서 .Scene을 지웠음
{
	public abstract class Scene		// 씬은 추상클래스로.
	{
		// 씬을 여러 개로 나눔

		protected Game game;
		public Scene(Game game)
		{
			this.game = game;	// 내가 지금 어떤 게임에 속해있는지.
		}

		public abstract void Render();		// Game 클래스에 있는 Render와 다른 거임
		public abstract void Update();		// Game 클래스에 있는 Update와 다른 거임
	}
}
