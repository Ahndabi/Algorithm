using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

// 이것도 뭔가 이상,, 깃허브 봐(그냥 내가 이상하게 한듯)

namespace DataStructure		// 네임스페이스 구현
{
	// 이진탐색트리 구현
	internal class BinarySearchTree<T> where T : IComparable<T>	// T item 데이터가 비교가 가능해야 함.
	{
		Node root;		// 최상위 노드

		public BinarySearchTree()
		{
			this.root = null;
		}

		public void Add(T item)
		{
			Node newNode = new Node(item, null, null, null);

			if(root == null)
			{
				root = newNode;
				return;
			}

			Node current = root;
			while(current != null)
			{
				// 비교해서 더 작은 경우 왼쪽으로 감
				if(item.CompareTo(current.item) < 0)
				{
					// 비교 노드가 왼쪽 자식이 있는 경우
					if(current.left != null)
					{
						// current는 왼쪽이랑 또 비교해야 함(이걸 반복)
						// 왼쪽 자식과 또 비교하기 위해 current 왼쪽 자식으로 설정
						current = current.left;	
					}
					// 비교 노드가 왼쪽 자식이 없는 경우
					else
					{
						// 그자리가 지금 추가가 될 자리
						current.left = newNode;
						newNode.parent = current;
						return;
					}
				}
				// 더 큰 경우에는 오른쪽으로 감
				else if (item.CompareTo(current.item) > 0)
				{
					// 비교 노드가 오른쪽 자식이 있는 경우
					if (current.right != null)
					{
						//오른쪽 자식과 또 비교하기 위해 current 오른쪽 자식으로 설정
						current = current.right;
					}
					// 비교 노드가 오른쪽 자식이 없는 경우
					else
					{
						// 그 자리가 지금 추가가 될 자리
						current.right = newNode;
						newNode.parent = current;
						return;
					}

				}
				// 동일한 데이터가 들어온 경우
				else
				{
					return;
				}

			}
		}

		// out T outValue : 출력전용 파라메타. 여기 넣은 변수도 출력대상임
		// 함수 종료 전까지 outValu에 꼭 넣어라?
		public bool TryGetValue(T item, out T outValue)		
		{
			Node findNode = FindNode(item);
			if (findNode == null)
			{
				outValue = default(T);
				return false;
			}
			else
			{
				outValue = findNode.item;
				return true;
			}
		}

		public bool Remove(T item)
		{
			Node findNode = FindNode(item);
			if(findNode == null)
			{
				return false;
			}
			else
			{
				EraseNode(findNode);
				return true;
			}
		}

		private Node FindNode(T item)
		{
			if (root == null)
				return null;

			Node current = root;
			while (current != null)
			{
				// 현재 노드의 값이 찾고자 하는 값보다 작은 경우
				if (item.CompareTo(current.item) < 0)
				{
					// 왼쪽 자식부터 다시 찾기 시작
					current = current.left;
				}

				// 현재 노드의 값이 찾고자 하는 값보다 큰 경우
				else if (item.CompareTo(current.item) > 0)
				{
					// 오른쪽 자식부터 다시 찾기 시작
					current = current.right;
				}

				// 현재 노드의 값이 찾고자 하는 값이랑 같은 경우
				else
				{
					// 찾음
					return current;		// 그 노드 자체를 반환
				}
			}
			return null;
		}

		void EraseNode(Node node)
		{
			// 자식 노드가 없는 노드일 경우
			if(node.HasNoChild)
			{
				if (node.IsLeftChild)
					node.parent.left = null;
				else if (node.IsRightChild)
					node.parent.right = null;
				else  // if(node.IsRootNode) 부모노드라면
					root = null;
			}
			// 자식 노드가 1개인 노드일 경우
			else if(node.HasLeftChild || node.HasRightChild)
			{
				Node parent = node.parent;
				Node child = node.HasLeftChild ? node.left : node.right;
				
				if(node.IsLeftChild)
				{
					parent.left = child;
					child.parent = parent;
				}
				else if(node.IsRightChild)
				{
					parent.right = child;
					child.parent = parent;
				}
				else  // if(node.IsRootNode) 내가 최정상 노드라면
				{
					root = child;
					child.parent = null;
				}
			}
			// 3. 자식 노드가 2개인 노드일 경우
			// 왼쪽 자식 중 가장 큰값과 데이터 교환한 후, 그 노드를 지워주는 방식으로 대체
			else   // if(node.HasBothChild)
			{
				Node replaceNode = node.left;
				while(replaceNode.right != null)
				{
					replaceNode = replaceNode.right;
				}
				node.item = replaceNode.item;
				EraseNode(replaceNode);
			}
		}

		/*
		public void Print(Node node)     // 중위순회
		{
			if(node.HasLeftChild)  Print(node.left);
			Console.WriteLine(node.item);
			if (node.HasRightChild) Print(node.right);
		}*/	// 왜 오류나징..

		class Node
		{
			internal T item;	
			internal Node parent;
			internal Node left;
			internal Node right;

			public Node(T item, Node parent, Node left, Node right)
			{
				this.item = item;
				this.parent = parent;
				this.left = left;
				this.right = right;
			}

			public bool IsRootNode { get { return parent == null; } }	// 내가 부모노드
			public bool IsLeftChild { get { return parent != null && parent.left == this; } }	// 내가 왼쪽자식
			public bool IsRightChild { get { return parent != null && parent.right == this; } }	 // 내가 오른쪽자식

			public bool HasNoChild { get { return left == null && right == null; } } // 자식X
			public bool HasLeftChild { get { return left != null && right == null; } } // 오자식X

			public bool HasRightChild { get { return left == null && right != null; } } // 왼자식X
			public bool HasBothChild { get { return left != null && right != null; } } // 자식O
		}

	}
}
