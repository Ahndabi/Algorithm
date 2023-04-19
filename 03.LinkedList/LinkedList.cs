using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Datastructure				// 네임스페이스 링크드리스트 이름이 겹쳐서 헷갈리니 DataStructure로 바꿈
{
	// 노드를 먼저 만들어야함.
	public class LinkedListNode<T>		// 노드 클래스
	{
		internal LinkedList<T> list;
		internal LinkedListNode<T> prev;
		internal LinkedListNode<T> next;
		private T item;                         // 데이터

		public LinkedListNode(T value)			// 이렇게 생성자도 만들어줌. 편하게
		{
			this.list = null;
			this.prev = null;
			this.next = null;
			this.item = value;
		}

		public LinkedListNode(LinkedList<T> list, T value)
		{
			this.list = list;
			this.prev = null;
			this.next = null;
			this.item = value;
		}

		public LinkedListNode(LinkedList<T> list, LinkedListNode<T> prev, LinkedListNode<T> next, T value)
		{
			this.list = list;
			this.prev = prev;
			this.next = next;
			this.item = value;
		}

		
		// 프로퍼티 이용해서 읽기 전용으로 만듦
		public LinkedList<T> List {  get { return list; } }
		public LinkedListNode<T> Prev {  get { return prev; } }
		public LinkedListNode<T> Next { get { return next; } }
		public T Value {  get { return item; } set { item = value; } }
	}


	public class LinkedList<T>		// 리스트 본체
	{
		private LinkedListNode<T> head;
		private LinkedListNode<T> tail;
		private int count;

		public LinkedList() 		// 생성자
		{
			this.head = null;
			this.tail = null;
			this.count = 0;
		}

		public LinkedListNode<T> First { get { return head; } }
		public LinkedListNode<T> Last { get { return tail; } }
		public int Count { get { return count; } }
	
		public LinkedListNode<T> AddFirst(T value)		// 앞에 데이터 붙이는 함수
		{
			// 1. 새로운 노드 만들기					// 지금 링크드리스트(this)와 벨유
			LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);

			// 2. 연결구조 바꾸기
			// 2-1. head 노드가 있을 때
			if (head != null)
			{
				newNode.next = head;
				head.prev = newNode;
			}
			// 2-2. Head 노드가 없을 때
			else
			{
				head = newNode;
				tail = newNode;
			}

			// 3. 갯수 늘리기
			count++;
			return newNode;
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

			if(node.next != null)
				node.next.prev = node.prev;

			// 2. count--
			count--;
		}

		// 내가 지정한 노드 앞에 새로운 노드 붙이기
		public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
		{
			if (node.list != this)       // 예외1 : node가 LinkedList에 포함된 노드가 아닌 경우.
				throw new InvalidOperationException();      // 유효하지 않은 명령

			if (node == null)           // 예외2 : 노드가 null 인 경우. 없는 노드를 지울 수는 없징
				throw new ArgumentNullException(nameof(node));

			// 1. 새로운 노드 만든다.
			LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);

			// 2. 연결구조 바꾸기
			newNode.next = node;
			newNode.prev = node.prev;
			node.prev.next = newNode;
			if(node.prev != null)
				node.prev = newNode;

			// 3. 개수증가
			count++;

			return newNode;
		}


		// 데이터로 remove 함수
		public bool Remove(T value)
		{
			LinkedListNode<T> findNode = Find(value);	// = 찾기
			if(findNode != null)
			{
				Remove(findNode);
				return true;
			}
			else
			{
				return false;
			}
		}

		public LinkedListNode<T> Find(T value)
		{
			LinkedListNode<T> target = head;
			EqualityComparer<T> comparer = EqualityComparer<T>.Default;	// 똑같은지 비교함

			while (target!=null)		// 노드가 없을 때까지 찾는다
			{
				if (comparer.Equals(value, target.Value))
					return target;
				else
					target = target.next;	// 다음으로 가서 찾기
			}
			return null;
		}
	}
}
