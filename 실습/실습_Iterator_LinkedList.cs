using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 실습
{
	internal class 실습_Iterator_LinkedList
	{
		// Iterator 부분만 빼고 깃허브에서 가져왔습니다.
		public class LinkedListNode<T>
		{
			internal LinkedList<T> list;
			internal LinkedListNode<T> prev;
			internal LinkedListNode<T> next;
			private T item;

			public LinkedListNode(T value)
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

			public LinkedList<T> List { get { return list; } }
			public LinkedListNode<T> Prev { get { return prev; } }
			public LinkedListNode<T> Next { get { return next; } }

			public T Item { get { return item; } set { item = value; } }
		}

		public class LinkedList<T> : IEnumerable<T>
		{
			private LinkedListNode<T> head;
			private LinkedListNode<T> tail;
			private int count;

			public LinkedList()
			{
				this.head = null;
				this.tail = null;
				count = 0;
			}

			public LinkedListNode<T> First { get { return head; } }
			public LinkedListNode<T> Last { get { return tail; } }
			public int Count { get { return count; } }

			public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
			{
				ValidateNode(node);
				LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);
				InsertNodeBefore(node, newNode);
				if (node == head)
					head = newNode;
				return newNode;
			}

			public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
			{
				ValidateNode(node);
				LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);
				InsertNodeAfter(node, newNode);
				if (node == tail)
					tail = newNode;
				return newNode;
			}

			public LinkedListNode<T> AddFirst(T value)
			{
				LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);
				if (head != null)
				{
					InsertNodeBefore(head, newNode);
					head = newNode;
				}
				else
				{
					InsertNodeToEmptyList(newNode);
				}
				return newNode;
			}

			public LinkedListNode<T> AddLast(T value)
			{
				LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);
				if (tail != null)
				{
					InsertNodeAfter(tail, newNode);
					tail = newNode;
				}
				else
				{
					InsertNodeToEmptyList(newNode);
				}
				return newNode;
			}

			public void Clear()
			{
				head = null;
				tail = null;
				count = 0;
			}

			public bool Contains(T value)
			{
				return Find(value) != null;
			}

			public LinkedListNode<T> Find(T value)
			{
				LinkedListNode<T>? node = head;
				EqualityComparer<T> c = EqualityComparer<T>.Default;
				if (value != null)
				{
					while (node != null)
					{
						if (c.Equals(node.Item, value))
							return node;
						else
							node = node.next;
					}
				}
				else
				{
					while (node != null)
					{
						if (node.Item == null)
							return node;
						else
							node = node.next;
					}
				}
				return null;
			}

			public bool Remove(T value)
			{
				LinkedListNode<T>? node = Find(value);
				if (node != null)
				{
					Remove(node);
					return true;
				}
				else
				{
					return false;
				}
			}

			public void Remove(LinkedListNode<T> node)
			{
				ValidateNode(node);
				if (head == node)
					head = node.next;
				if (tail == node)
					tail = node.prev;
				RemoveNode(node);
			}

			public void RemoveFirst()
			{
				if (head == null)
					throw new InvalidOperationException();

				LinkedListNode<T> headNode = head;
				Remove(headNode);
			}

			public void RemoveLast()
			{
				if (tail == null)
					throw new InvalidOperationException();

				LinkedListNode<T> tailNode = tail;
				Remove(tailNode);
			}

			private void ValidateNode(LinkedListNode<T> node)
			{
				if (node == null)
					throw new ArgumentNullException();
				if (node.list != this)
					throw new InvalidOperationException();
			}

			private void InsertNodeBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
			{
				newNode.next = node;
				newNode.prev = node.prev;
				if (newNode.prev != null)
					newNode.prev.next = newNode;

				node.prev = newNode;

				count++;
			}

			private void InsertNodeAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
			{
				newNode.prev = node;
				newNode.next = node.next;
				if (newNode.next != null)
					newNode.next.prev = newNode;

				node.next = newNode;

				count++;
			}

			private void InsertNodeToEmptyList(LinkedListNode<T> newNode)
			{
				if (count != 0)
					throw new InvalidOperationException();

				head = newNode;
				tail = newNode;
				count++;
			}

			private void RemoveNode(LinkedListNode<T> node)
			{
				if (node.list != this)
					throw new InvalidOperationException();

				if (node.prev != null)
					node.prev.next = node.next;
				if (node.next != null)
					node.next.prev = node.prev;

				count--;
			}

			public IEnumerator<T> GetEnumerator()
			{
				return new Enumerator(this);
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return new Enumerator(this);
			}

			public struct Enumerator : IEnumerator<T>
			{
				// 링크드리스트의 반복자는 노드의 값과 리스트, 노드가 있어야합니다.
				LinkedListNode<T> node;
				LinkedList<T> list;
				T current;

				public Enumerator(LinkedList<T> list)
				{
					this.list = list;
					this.node = list.head;			// 현재 노드는 head 를 가리킵니다.
					this.current = default(T);		// 현재 값은 기본값으로 설정합니다.
				}

				public T Current { get { return current; } }

				object IEnumerator.Current { get { return current; } }

				public void Dispose()
				{
				}

				public bool MoveNext()
				{
					while(node != null)		// 현재의 노드가 null이면 반복을 끝냅니다.
					{
						current = node.Item;    // 현재 값은 현재 노드의 아이템입니다.
						node = node.Next;       // 다음 노드를 가리킵니다.
						return true;
					}
					return false;
				}

				public void Reset()			// 반복자가 처음으로 되돌아가는 함수입니다.
				{
					node = list.head;       // 노드는 헤드를 가리킵니다.
					current = default(T);	// 현재 값은 기본값으로 설정합니다.
				}
			}
		}

	}
}
