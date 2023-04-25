using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 실습
{
	internal class BinarySearchTree<T> where T : IComparable<T>		// T는 비교할 수 있어야 함
	{
		class Node
		{
			internal T item;			// 데이터 값
			internal Node parent;		// 부모 노드
			internal Node leftChld;		// 왼쪽 자식 노드
			internal Node rightChild;   // 오른쪽 자식 노드
			
			public Node()   // 생성자	// T item, Node parent, Node leftChld, Node rightChild
			{
				this.item = item;
				this.parent = parent;
				this.leftChld = leftChld;
				this.rightChild = rightChild;
			}
			public bool IsParentNode { get { return leftChld != null || rightChild != null; } }	// 내가 부모노드인지
			public bool IsLeftChld { get { return parent != null && parent.leftChld == this; } } // 내가 왼쪽 자식인지
			public bool IsRightChild { get { return parent != null && parent.rightChild == this; } } // 내가 오른쪽 자식인지
			public bool HasNoParent { get { return parent == null; } }	// 부모 노드가 없는 경우
			public bool HasBothChild { get { return rightChild != null && leftChld != null; } } // 자식이 둘 다 있는 경우
			public bool HasNoChild { get { return leftChld == null && rightChild == null; } } // 자식X
			public bool HasLeftChild { get { return leftChld != null && rightChild == null; } } // 오자식X
			public bool HasRightChild { get { return leftChld == null && rightChild != null; } } // 왼자식X
		}

		Node root = null;		// 최상위 노드

		public void Add(T item)		// 노드를 추가하는 함수
		{
			Node newNode = new Node();      // 새로운 노드를 만든다.
			Node current = new Node();      // 현재의 노드를 만든다.
			current = root;		// 현재의 노드에 root를 넣는다.

			// 먼저, root와 newNode를 비교한다.
			if (root == null)		// 만약에 root가 비어있으면,
				root = newNode;		// root가 newNode가 된다.

			while(current != null)
			{
				// newNode보다 비교할 노드의 데이터가 더 작으면,
				if (newNode.item.CompareTo(current.item) < 0)
				{
					if (current.rightChild != null)  // 오른쪽 자식이 있는 경우에,
					{
						current = current.rightChild;   // 비교할 노드는 오른쪽 자식이 되어 이것을 반복한다.
					}
					else if (current.rightChild == null)  // 오른쪽 자식이 없으면,
					{
						newNode = current.rightChild;    // newNode는 그 자식이 되고
						newNode.parent = current;        // newNode의 부모는 current가 된다.
					}
				}
				// newNode보다 비교할 노드의 데이터가 더 크다면,
				else if (newNode.item.CompareTo(current.item) > 0)
				{
					if (current.leftChld != null)   // 왼쪽 자식이 있는 경우에,
					{
						current = current.leftChld; // 비교할 노드는 current의 왼쪽자식이 된다.
					}
					else if (current.leftChld == null)  // 왼쪽 자식이 없다면,
					{
						newNode = current.leftChld;     // newNode는 그 왼쪽 자식이 된다.
						newNode.parent = current;       // newNode의 부모는 current가 된다.
					}
				}
				else // 데이터 값이 같을 경우
					return;	// 여기서 끝
			}
		}

		public bool TryGetValue(T item, out T outValue)
		{
			if (root == null)	// 노드가 없다면,
			{
				outValue = default(T);	// outValue는 기본값으로 저장한다.
				return false;			// false 반환
			}

			Node findNode = FindNode(item);		// findNode를 만들고 함수를 통해 노드를 찾는다.
			if (findNode == null)		// findNode가 없다면,
			{
				outValue = default(T);	// 마찬가지로 outValue는 기본값으로 저장한다.
				return false;			// false 반환
			}
			else  // findNode를 찾았다면
			{
				outValue = findNode.item;	// outValue에 값을 저장한다.	
				return true;				// true 반환
			}
		}

		Node FindNode(T item)
		{
			Node current = root;
			// 찾으면 true, 없으면 false
			if(root == null)			// 노드가 하나도 없다면,
				return null;			// null을 반환한다.

			while(current != null)		// 비교할 노드가 있다면,
			{
				// 찾고자 하는 값이 현재 노드의 값보다 작은 경우
				if (item.CompareTo(current.item) < 0)
					current = current.leftChld;

				// 찾고자 하는 값이 현재 노드의 값보다 큰 경우
				else if (item.CompareTo(current.item) > 0)
					current = current.rightChild;

				// 값이 같은 경우(찾은 경우)
				else
					return current;		// 그 노드 자체를 반환한다.
			}
			return null;	// 값이 없으면 null을 반환한다.
		}

		public bool Remove(T item)		// 지우는 함수이다.
		{
			Node findNode = FindNode(item);		// 지울 노드를 찾는다.
			if(findNode == null)		// 찾으려는 노드가 없으면
				return false;			// false를 반환한다.
			else  // 노드가 있다면
			{						
				EraseNode(findNode);	// 지우는 함수를 이용해서 찾고
				return true;			// true를 반환한다.
			}	
		}

		void EraseNode(Node node)
		{
			if(node.HasNoChild)     // 자식 노드가 없는 노드일 경우
			{
				if (node.IsLeftChld)	// 노드의 왼쪽 자식일 영우
					node.parent.leftChld = null;
				else if (node.IsRightChild)		// 노드의 오른쪽 자식일 경우
					node.parent.rightChild = null;
				else // 부모 노드라면,
					root = null;
			}

			// 자식 노드가 1개인 노드일 경우
			else if (node.HasLeftChild || node.HasRightChild)
			{
				Node parent = node.parent;
				Node child = node.HasLeftChild ? node.leftChld : node.rightChild;

				if (node.IsLeftChld)	// 만약 노드의 왼쪽자식이라면,
				{
					parent.leftChld = child;	
					child.parent = parent;
				}
				else if (node.IsRightChild)		// 노드의 오른쪽 자식이라면,
				{
					parent.rightChild = child;
					child.parent = parent;
				}
				else  // 최정상 노드라면
				{
					root = child;
					child.parent = null;
				}
			}
			// 자식 노드가 2개라면,
			// 왼쪽 자식 중 가장 큰값과 값을 바꾸고, 그 노드를 지운다.
			else 
			{
				Node replaceNode = node.leftChld;		// 대체할 노드는 왼쪽 자식이 된다.
				while (replaceNode.rightChild != null)	// 오른쪽 자식이 빌 때까지 반복한다.
				{
					replaceNode = replaceNode.rightChild;	// 대체할 노드는 그 노드의 오른쪽 자식이 된다.
				}
				node.item = replaceNode.item;	// 노드의 값은 대체 값이 된다.
				EraseNode(replaceNode);
			}
		}
	}
}
