using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructrue		// 네임스페이스 바꿨음
{

	// 만약에 어려우면, TPriority 자리에 int를 넣어주어도 ㄱㅊ아. 어차피 우선순위 매기는 곳이니.
	internal class PriorityQueue<TElement>
	{	
		struct Node
		{
			public TElement element;
			public int priority;
		}

		private List<Node> nodes;       // 리스트를 이용한 어뎁터로 구현.
		//private IComparer<TPriority> comparer;		// 우선순위끼리 비교할 수 있는 comparer

		public PriorityQueue()
		{
			this.nodes = new List<Node>();
		}
		
		public int Count {  get { return nodes.Count; } }
		public void Enqueue(TElement element, int priority)
		{
			// 만약 우선순위가 높은 새로운 노드가 추가되면, 배열의 앞부분에 끼워넣는 게 아니라,
			// 우선 마지막 배열 순서에 넣은 다음에 추가된 노드의 부모들과 우선순위를 비교해서
			// 우선순위가 더 높다면 부모와 자리를 바꾸는 식으로 앞으로 옮겨감.

			Node newNode = new Node() { element = element, priority = priority };

			// 1. 가장 뒤에 데이터 추가
			nodes.Add(newNode);
			int newNodeIndex = nodes.Count - 1;
			
			// 2. 새로운 노드를 힙상태가 유지되도록 승격 작업 반복
			while(newNodeIndex > 0)		// 0 이 최상위 노드기 때문에 >= 라고 하면 무한루프됨
			{
				// 2-1. 부모 노드 확인
				int parentIndex = (newNodeIndex - 1) / 2;
				Node parentNode = nodes[parentIndex];

				// 2-2. 자식 노드가 부모 노드보다 우선순위가 높으면 교체
				if (newNode.priority < parentNode.priority) // 기본이 오름차순이니까, 숫자가 더 작으면 바뀌도록
				{
					nodes[newNodeIndex] = parentNode;
					nodes[parentIndex] = newNode;	// 부모 위치에 새로운 노드
					newNodeIndex = parentIndex;	
				}
				else
					break;
			}
		}

		public  TElement Dequeue()
		{
			// 만약 있는 노드를 삭제하려면?
			// 일단 부모가 없는 자식이 있으면 안 돼.
			// 가장 뒤에 있던 자료를 그 빵꾸난 부모자리로 일단 옮김.(여기서 힙 구조가 깨지겠지?)
			// 그럼 빵구를 채웠던 그 노드가 밑의 자식들과 비교해서 뒤로 뒤로 옮기는 식으로 함.

			Node rootNode = nodes[0];
			
			// 1. 가장 마지막 노드를 최상단으로 위치
			Node lastNode = nodes[nodes.Count - 1];
			nodes[0] = lastNode;
			nodes.RemoveAt(nodes.Count - 1);    // 가장 뒤에 있는 애를 지우기

			int index = 0;
			// 2. 자식 노드들과 비교하여 더 작은 자식과 교체 반복
			while (index < nodes.Count)
			{
				int leftChildIndex = GetLeftChildIndex(index);
				int rightChildIndex = GetRightChildIndex(index);

				// 2-1. 자식이 둘 다 있는 경우
				if(rightChildIndex < nodes.Count)
				{
					// 2-1-1. 왼쪽 자식과 오른쪽 자식을 비교하여 우선순위가 높은 자식을 선정
					int lessChildIndex = nodes[leftChildIndex].priority < nodes[rightChildIndex].priority
						? leftChildIndex : rightChildIndex;

					// 2-1-2. 더 우선순위가 높은 자식과 부모 노드를 비교하여
					// 부모가 우선순위가 더 낮은 경우 바꾸기
					if (nodes[lessChildIndex].priority < nodes[index].priority)
					{
						nodes[index] = nodes[lessChildIndex];
						nodes[lessChildIndex] = lastNode;
						index = lessChildIndex;
					}
					else
					{
						break;
					}
				}

				// 2-2. 자식이 하나만 있는 경우	== 왼쪽 자식만 있는 경우
				else if(leftChildIndex < nodes.Count)
				{
					if (nodes[leftChildIndex].priority < nodes[index].priority)
					{
						nodes[index] = nodes[leftChildIndex];
						nodes[leftChildIndex] = lastNode;
						index = leftChildIndex;		
					}
				}
				// 2-3. 자식이 없는 경우
				else
				{

				}
			}

			return rootNode.element;
		}
		public TElement Peek()
		{
			return nodes[0].element;	// 가장 앞에 있는 애.
		}


		private int GetParentIndex(int childIndex)			// 부모 인덱스 구하기
		{
			return (childIndex - 1) / 2;
		}

		private int GetLeftChildIndex(int parentIndex)		// 자식 중 왼쪽 자식
		{
			return parentIndex * 2 + 1;
		}


		private int GetRightChildIndex(int parentIndex)		// 자식 중 오른쪽 자식
		{
			return parentIndex * 2 + 2;
		}


	}
}
