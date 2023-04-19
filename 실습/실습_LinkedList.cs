using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace 실습
{
	internal class 실습_LinkedList
	{
		// LinkedList 구현하기
		// LinkedListNode 클래스 먼저 만들고 LinkedList 클래스 만들기
		// , Remove(T value), Remove(node),
		public class LinkedListNode<T>
		{
			// 노드에는 데이터, prev 주소, next 주소, 리스트
			internal LinkedList<T> list;
			internal LinkedListNode<T> prev;		// LinkedListNode 클래스의 prev
			internal LinkedListNode<T> next;		// LinkedListNode 클래스의 next
			internal T data;						// T 자료형의 data

			public LinkedListNode(T data)		// 생성자 만들기
			{
				this.list = null;
				this.prev = null;
				this.next = null;
				this.data = data;
			}

			public LinkedListNode(LinkedList<T> list, T data)		// 생성자 만들기
			{
				this.list = list;
				this.prev = null;
				this.next = null;
				this.data = data;
			}
		}

		public class LinkedList<T>
		{
			// 리스트 본체에는 head, tail, 노드, (리스트 가 있어야 하나?)
			LinkedListNode<T> head;
			LinkedListNode<T> tail;
			LinkedListNode<T> node;
			uint count = 0;					// count는 -가 오면 안 돼서 uint로 했습니다.

			public LinkedList()				// 생성자 만들기
			{
				this.head = null;
				this.tail = null;
				this.node = null;
			}


			public LinkedListNode<T> AddFirst(T _data)		// { 1,2,3 } 1 앞에 0을 넣어줌
			{
				// 1. 새로운 노드를 생성한다.
				LinkedListNode<T> newNode = new LinkedListNode<T>(this, _data);			// 리스트와 데이터 값을 생성하는 노드 객체를 만든다.

				// * 만약 newNode가 첫 노드가 아닌 경우에 2, 3번 실행
				if(count != 0)		// 만약 newNode가 첫 노드가 아니라면,
				{
					head.prev = newNode;	// 2. head.prev가 newNode를 가리키게 한다.
					newNode.next = head;    // 3. newNode.next가 head를 가리키게 한다. (여기까지 서로를 가리키게 함)
					head = newNode;         // 4. head가 _data를 가리키도록 한다.
				}
				else    // 만약 newNode가 첫 번째 노드라면,
				{
					head = newNode;
				}
				count++;
				return newNode; 
			}


			public LinkedListNode<T> AddLast(T _data)		// { 1,2,3 }  4
			{
				// 1. 새로운 노드 생성
				LinkedListNode<T> newNode = new LinkedListNode<T>(this, _data);		  // 리스트와 데이터 값을 생성하는 노드 객체를 만든다.

				// 2. tail노드가 새로운 노드를 가리키게 한다. (노드가 있을 경우)
				if (count == 0)				// 노드가 하나도 없을 시
				{
					head = newNode;         // head가 newNode를 가리키게 한다.
					tail = newNode;
				}
				else     			    	// 노드가 하나라도 있을 떄
				{
					newNode.prev = tail;	// newNode의 prev가 tail이었던 노드를 가리킨다.
					tail.next = newNode;    // tail의 next는 newNode를 가리킨다.
					tail = newNode;			// 새로운 tail은 newNode를 가리킨다.
				}						

				// 3. count++
				count++;					// 개수를 증가시킨다.
				return newNode;
			}

			public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T _data)
			{                                                                       
				
				// 0. 예외처리*************** 예외처리 부분은 좀 어려워서 수업 때 했던 내용과 같습니다ㅜㅜ
				if (node.list != this)       // 예외1 : node가 LinkedList에 포함된 노드가 아닌 경우.
					throw new InvalidOperationException();      // 유효하지 않은 명령

				if (node == null)           // 예외2 : 노드가 null 인 경우. 없는 노드를 지울 수는 없징
					throw new ArgumentNullException(nameof(node));


				// 1. 새로운 노드를 만든다.
				LinkedListNode<T> newNode = new LinkedListNode<T>(this, _data);

				// 2. 구조변경
				node.prev = newNode;            // 2. node.prev가 newNode를 가리키게 한다. 
				newNode.prev = node.prev;       // newNode의 prev가 node.prev를 가리키게 한다.
				newNode.next = node;            // newNode.next가 node를 가리키게 한다.
				node.prev.next = newNode;		// node.prev.next가 newNode를 가리키게 한다.


				count++;					// 개수를 증가시킨다.
				return newNode;
			}

			public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T _data)      // { 1,2, 4}   2뒤에 3을 넣고 싶다.
			{
				// 0. 예외처리*************** 예외처리 부분은 좀 어려워서 수업 때 했던 내용과 같습니다ㅜㅜ
				if (node.list != this)       // 예외1 : node가 LinkedList에 포함된 노드가 아닌 경우.
					throw new InvalidOperationException();      // 유효하지 않은 명령

				if (node == null)           // 예외2 : 노드가 null 인 경우. 없는 노드를 지울 수는 없징
					throw new ArgumentNullException(nameof(node));

				// 1. newNode를 생성한다.
				LinkedListNode<T> newNode = new LinkedListNode<T>(this, _data);

				// 2. 구조를 변경해준다.
				if (node.next == null)
				{
					node.next = newNode;
					newNode.prev = node;
					tail = newNode;
				}
				else
				{
					newNode.next = node.next;
					node.next.prev = newNode;
					node.next = newNode;
					newNode.prev = node;
				}

				count++;
				return newNode;
			}

			public LinkedListNode<T> Find(T _data)			// Find가 어려워서 수업 때 했던 부분을 많이 참고 했습니다ㅜㅜ
			{
				LinkedListNode<T> target = head;
				EqualityComparer<T> comparer = EqualityComparer<T>.Default; // 똑같은지 비교한다.

				while (target != null)      // 노드가 없을 때까지 찾는다
				{
					if (comparer.Equals(_data, target.data))
						return target;
					else
						target = target.next;   // 다음으로 가서 찾기
				}
				return null;
			}

			// 노드로 remove하는 함수
			public void Remove(LinkedListNode<T> node)
			{
				if (node.list != this)       // 예외1 : node가 LinkedList에 포함된 노드가 아닌 경우.
					throw new InvalidOperationException();      // 유효하지 않은 명령

				if (node == null)           // 예외2 : 노드가 null 인 경우. 없는 노드를 지울 수는 없징
					throw new ArgumentNullException(nameof(node));

				// 0. 지웠을 때 head나 tail이 변경되는 경우 적용
				if (head == node)
					head = node.next;
				if (tail == node)
					tail = node.prev;

				// 1. 연결구조 바꾸기
				if (node.prev != null)
					node.prev.next = node.next;

				if (node.next != null)
					node.next.prev = node.prev;

				// 2. count--
				count--;
			}

		}

		static void Main(string[] args)
		{
		}
	}
}
