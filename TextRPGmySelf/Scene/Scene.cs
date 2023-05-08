using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGmySelf
{
	public abstract class Scene
	{
		protected Game game;
		protected Player player;
		protected Position pos;
		public static bool[,] map;

		public Scene(Game game)
		{
			this.game = game;
		}

		public abstract void MapPrinting();
		public abstract void Update();
	}
}
