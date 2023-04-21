using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 실습
{
	// 큐는 스택과 완전 반대의 자료구조이다. 
	// 큐는 FIFO의 자료구조라서 list 배열형태로 만들기 힘들다. 
	// 그렇다고 링크드리스트를 활용해서 만들면 C#의 특성상 가비지컬렉터가 비효율적으로 운영되기 때문에 링크드리스트를 사용하지 않는 것이 좋다.
	// 그래서 큐는 직접 구현해서 만든다.
	// 큐는 환형배열로 구현하는 것처럼 만들고, head 와 tail 화살표를 만들어 데이터가 하나 유입되면 head가 오른쪽으로 한 칸 옮겨지고 데이터가 하나 삭제되면 tail은 오른쪽으로 한 칸 옮겨진다.

	public class 실습_Queue<T>
	{
		// 큐는 배열 head, tail, index, count
		// head가 있는 위치에 데이터를 넣고, tail의 다음 위치의 데이터를 삭제하도록 함.
		int head;
		int tail;
		T[] array;
		int count;
		int fullSize;

		public 실습_Queue()	
		{
			head = 0;				//  head, tail, count 모두 0으로 설정한다.
			tail = -1;
			count = 0;
			fullSize= 4;
			array = new T[4];		// 처음엔 4칸으로 만든다.
		}

		public void Enqueue(T value)	// 데이터를 넣는 함수이다.
		{
			if (count == fullSize)		// 만약 배열이 꽉 찼다면?
				Grow();					// 늘려준다.

			if (head + 1 == tail || (tail == 0 && head == fullSize - 1))         // 만약 한 칸 이동해야 할 head의 위치가 tail의 위치와 같다면?
			{						
				Grow();					// 늘려준다.
			}

			array[head] = value;        // 배열 헤드 위치에 값을 넣는다.

			if (head == fullSize)       // head가 가장 끝에 있다면?
				head = 0;               // head를 처음으로 가게 한다.
			else
				head++;					// headf를 1 증가시킨다.
			
			count++;                    // 갯수도 1 증가시킨다.
		}

		public T Peek()					// 제일 끝에 있는 값을 반환한다. 최전방 함수이다.
		{
			return array[head - 1];		// head 위치에 데이터를 넣어야 하므로 head는 빈칸이다. 그래서 바로 전 칸의 데이터가 가장 마지막 데이터이다.
		}

		public T Dequeue()		 // 가장 앞에 있는 데이터를 제거하고 반환하는 함수이다.
		{
			tail++;				 // tail을 하나 증가시킨다.
			T item = array[tail];		// 반환할 값인 array[tail]을 item에 저장한다.
			array[tail] = default(T);   // array의 tail 부분을 기본값으로 설정한다.(지운다.)
			count--;					// count는 하나 감소시킨다.
			return item;				// item을 반환한다.
		}

		private void Grow()     // 배열이 꽉 찼을 경우 배열을 늘려주는 함수이다.
		{
			T[] newArray = new T[count * 2];    // 2배 더 늘린 newArray를 만든다.
			int newfullSize = count * 2;

										     						//		 h  t		      t     h
												// 배열을 복사해야징	{ 1, 2, 0, 4 }  - > { 3, 4, 1, 0, 0, 0, 0 ,0}
										     						//	  t	   	   h          t        h
												// 배열을 복사해야징	{ 1, 2, 3, 0 }  - > { 1, 2, 3, 0, 0, 0, 0 ,0}		(구현할 때 보면서 만들려구 해놓은 예시입니다.)

			// tail 뒤에 +1을 해주는 이유는 tail이 -1로 시작하기 때문이다.
			if (head > tail)
			{
				Array.Copy(array, tail + 1, newArray, 0, fullSize);
				tail = -1;                  // tail은 다시 -1이 되고
			}
			else
			{
				Array.Copy(array, tail + 1, newArray, 0, fullSize - 1);
				Array.Copy(array, 0, newArray, fullSize - tail, newfullSize - (fullSize - tail + 1));
				tail = -1;					    // tail은 다시 -1이 되고
				head = fullSize - tail + 1;		// head는 fullSize - tail + 1이 된다.
			}
			array = newArray;			// 배열은 새 배열로 바꿔준다.
			fullSize = newfullSize;		// fullsize는 newfullSize가 된다.
		}
	}
}
