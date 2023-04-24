using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace 실습
{
	// 힙(Heap) 에 대해 이해한 대로 정리하기*********
	// 힙(Heap)이란 이진트리 형식의 우선순위 자료구조이다. 힙영역의 힙이랑은 별개의 개념이다.
	// 우선순위가 높은 순서의 자료부터 불러올 수 있다.
	// 자료구조 힙은 부모노드와 자식노드들로 이루어진 배열이다.
	// 노드라고 해서 링크드리스트를 쓰면 가비지컬렉터로 인해 과부하가 걸릴 수 있으므로 리스트의 배열형태를 사용한다.
	// 이진트리는 하나의 부모가 최대 두 자식을 가지고 있을 수 있다.
	// 부모노드는 반드시 자식의 우선순위보다 높아야 한다.
	// 배열의 순서는 부모-왼쪽자식-오른쪽자식 이다.
	// 힙은 순환이 되지 않는 비선형자료구조이다.

	internal class 실습_Heap
	{
		public class HeapTest<T>
		{
			// 우선순위 큐 구현하기
			// 리스트 배열로 어뎁터 만들기
			struct Node     // 노드들의 구조체
			{
				public T value;    // 노드 값
				public int priority;	// 우선순위 값
			}

			int count;      // 노드 개수
			public int index;		// 인덱스

			List<Node> heapNode;     // 리스트 배열로 어뎁터해서 구현

			public HeapTest()	// 초기화 해준다.
			{
				heapNode = new List<Node>();	
				count = 0;
				index = 0;
			}

			public void EnQueue(T value, int priority)		// 데이터를 넣는 함수이다.
			{
				Node newNode = new Node()		// 새로운 노드를 만든다.
				{
					value = value, priority = priority
				};

				// 1. 배열의 가장 마지막에 넣는다.
				heapNode.Add(newNode);

				int newNodeIndex = heapNode.Count - 0;  // 새로운 노드의 인덱스는 count - 0 이다.

				int parentIndex = (newNodeIndex - 1) / 2;   // 부모노드의 인덱스이다.
															// 2. 부모와 비교하여 data 우선순위가 더 높으면 부모와 자리를 바꾼다.
				while(newNodeIndex > 0)		// newNodeIndex가 최상위 노드가 될 때까지 반복한다.
				{
					int parentNodeIndex = ParentNodeIndex(newNodeIndex);    // 부모 노드의 인덱스이다.
					Node parentNode = heapNode[parentIndex];

					/*
					if (newNode.priority > parentNode.priority)     // 만약 자식의 우선순위가 부모보다 크다면
					{
						heapNode[parentIndex] = newNode;
						heapNode[newNodeIndex] = parentNode;
						parentNode = newNode;
					}*/

					if (newNode.priority < parentNode.priority) // 기본이 오름차순이니까, 숫자가 더 작으면 바뀌도록
					{
						heapNode[newNodeIndex] = parentNode;
						heapNode[parentIndex] = newNode;   // 부모 위치에 새로운 노드
						newNodeIndex = parentIndex;
					}
					else
						break;
				}
			}

			public T Dequeue()     // 노드를 삭제하는 함수이다.
			{
				// 노드를 삭제하고 나서 마지막에 있는 노드를 빈자리에 넣는다.
				// 빈자리를 채운 노드는 자식노드와 계속 비교해가며 순서를 바꾼다.

				Node firstNode = heapNode[0];		// 가장 첫번째 노드이다.

				Node lastNode = heapNode[heapNode.Count - 1];   // 가장 마지막 노드이다.
				heapNode[0] = lastNode;			// 마지막 노드를 가장 앞으로 가져온다.
				heapNode.RemoveAt(heapNode.Count - 1);		// 가장 뒤에 있는 노드를 지운다.

				// 자식 노드들과 반복하여 비교한다.
				while (index < heapNode.Count)
				{
					int leftChildIndex = LeftChildIndex(index);		// 왼쪽 자식의 인덱스이다.
					int rightChildIndex = RightChildIndex(index);	// 오른쪽 자식의 인덱스이다.

					// 자식이 둘 다 있는 경우
					if(rightChildIndex < heapNode.Count)
					{
						// 왼쪽 자식과 오른쪽 자식을 비교하여 우선순위 높은 자식을 선정한다.
						int lessChildIndex = heapNode[leftChildIndex].priority < heapNode[rightChildIndex].priority
							? leftChildIndex : rightChildIndex;

						// 더 우선순위가 높은 자식과 부모 노드를 비교하여
						// 부모가 우선순위가 더 낮은 경우 바꾼다.
						if (heapNode[lessChildIndex].priority < heapNode[index].priority)
						{
							heapNode[index] = heapNode[lessChildIndex];
							heapNode[lessChildIndex] = lastNode;
							index = lessChildIndex;
						}
						else
							break;
					}

					// 자식이 하나만 있는 경우 즉, 왼쪽 자식만 있는 경우이다.
					else if (leftChildIndex < heapNode.Count)
					{
						if (heapNode[leftChildIndex].priority < heapNode[index].priority)
						{
							heapNode[index] = heapNode[leftChildIndex];
							heapNode[leftChildIndex] = lastNode;
							index = leftChildIndex;
						}
					}
				}
				return firstNode.value;
			}

			public T Peek()		// 가장 앞에 있는 노드의 값을 구하는 함수이다.
			{
				return heapNode[0].value;
			}



			public int ParentNodeIndex(int childIndex)    // 부모 인덱스 구하는 함수이다.
			{
				return (childIndex - 1) / 2;			
			}

			public int LeftChildIndex(int parentIndex)    // 왼쪽 자식 인덱스 구하는 함수이다.
			{
				return parentIndex * 2 + 1;
			}

			public int RightChildIndex(int parentIndex)   // 오른쪽 자식 인덱스 구하는 함수이다.
			{
				return parentIndex * 2 + 2;
			}
		}

	}
}
