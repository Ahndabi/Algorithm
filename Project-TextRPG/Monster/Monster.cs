using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TextRPG
{
    public abstract class Monster       // 몬스터도 추상화로 상위개념 만들어줌.
    {
        public char icon = '▼'; // 몬스터 아이콘
        public Position pos;        // 몬스터도 위치가 있으니 포지션 만듦

        public abstract void MoveAction();      // 몬스터가 움직이는 액션. 추상화로
        
        // 몬스터 이동
        public void Move(Direction dir)
        {
            Position prevPos = pos;
            switch (dir)
            {
                case Direction.Up:
                    pos.y--;
                    break;
                case Direction.Down:
                    pos.y++;
                    break;
                case Direction.Left:
                    pos.x--;
                    break;
                case Direction.Right:
                    pos.x++;
                    break;
            }
        }

	}
}
