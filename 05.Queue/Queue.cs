using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05.Queue
{
	// Queue는 c#에서 링크드리스트를 거의 쓰지 않기 때문에 직접 구현해야함.
	// 그래서 Queue가 스택보다 기술면접이 더 많이 나옴.

	// 노드기반으로 구현하면 안 되니까 배열로 써야해. 
	// 근데 인덱스 안 쓴 이유가 Queue에서 맨 앞에 애를 꺼내면 뒤에 애들 한 칸씩 땡기는 거 때문이었자나
	// 근데 Queue에서 [0] 맨 앞에를 제거하면 뒤에 애들 앞으로 안 땡김. 가장 앞을 가리키는 인덱스만 ++ 해줌.
	// 앞 데이터를 제거하고 화살표가 한 칸 뒤로감. 하나 추가하면 빠진 인덱스에 추가됨. 뒤화살표도 뒤로감(?)
	// 그래서 이런 배열을 환형배열이라고 함. (또는 원형배열)

	// 가장 뒤의 인덱스에 위치에 있다면 가장 앞으로 옴.
	// 전단이랑 후단이랑 겹친다면? 비어있는지 채워있는건지 구분이 안감.
	// 그래서, 만약 8개짜리 큐를 사용한다치면 공간을 9개를 만들어서 후단이 9번째 있을 때 꽉찼다고 표현.
	// 후단이 전단보다 한칸 전에 있다? 이건 꽉 차 있는 거야.

	public class Queue<T>
	{
		private const int DefaultCapacity = 4;		// 초기 크기
		private T[] array;
		private int head;
		private int tail;


		public Queue()
		{
			array = new T[DefaultCapacity + 1];     // 크기보다 한 칸 더 크게 만듦(전과 후 화살표가 겹치지않기 위해)
			head = 0;
			tail = 0;
		}

		public int Count
		{
			get
			{
				if (head <= tail)
					return tail - head;
				else
					return tail - head + array.Length;
			}
		}

		public void EnQueue(T item)		// 데이터 집어넣는 함수
		{
			if (IsFull())       //  만약 꽉 차 있다면 배열의 크기를 늘려줘야징
			{
				Grow();
			}

			// 다만 tail이 맨 끝에 있을 땐 맨 앞에 가줘야하지~
			array[tail] = item;
			MoveNext(ref tail);		// tail의 원본이 바뀌어야 하니까 ref 참조형식으로 한 거임
		}

		public T Dequeue()		// 꺼내는 함수
		{
			if (IsEmpty())
				throw new InvalidOperationException();

			T result = array[head];
			MoveNext(ref head);
			return result;
		}

		public T Peek()
		{
			if (IsEmpty())
				throw new InvalidOperationException();

			return array[head];
		}

		private void MoveNext(ref int index)	// tail의 원본이 바뀌어야 하니까 ref 참조형식으로.
		{
			index = (index == array.Length - 1) ? 0 : index + 1;
			// 인덱스가 끝에 있다면 0으로 가고 아니면 +1 해라.
		}


		private bool IsEmpty()
		{
			return head == tail;
		}

		private bool IsFull()
		{
			if (head > tail)
				return head == tail + 1;
			else        // 예외사항) head가 맨 앞에 있고 tail이 제일 뒤에 있을 때
				return head == 0 && tail == array.Length - 1;
		}				// Lengh 길이는 총 5고 마지막이라면 인덱스는 [4]일 테니까 -1 한 거임
	
		private void Grow()
		{
			int newCapacity = array.Length * 2;
			T[] newArray = new T[newCapacity];
			Array.Copy(array, newArray, Count);

			if (head < tail)    // 이 경우라면 그대로 복사해도 큐의 규칙이 깨지지 않아
			{
				Array.Copy(array, newArray, Count);
			}
			else  // 하지만 tail이 더 앞에 있는 경우에 크기를 늘려버리면 뒤에가 빈공간으로 남아버리지,,,
			{  // 이렇게 이때는 규칙이 깨지기 때문에. head로부터 끝까지 복사하고 그걸 처음부터 시작하도록 복사.
				Array.Copy(array, head, newArray, 0, array.Length - head);
				// array배열에서 head부터 끝까지 복사함. 그리고 newArray배열에 0부터 복사한 거 붙일 거임.
				// 그리고 이 총 길이는 array.Length - head 임.

				Array.Copy(array, 0, newArray, array.Length - head, tail);
				head = 0;
				tail = Count;
			}
			array = newArray;
		}
	}
}


