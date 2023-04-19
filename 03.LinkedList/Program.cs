namespace _03.LinkedList
{
	// 연속적인 배열의 단점 (배열, List)
	// 중간 인덱스를 하나 지웠을 때, 그걸 지우고 그대로 냅둘 수 없음. (즉 빈공간을 남길 수 없음)
	// 삽입과 삭제에서 그렇게 효율적이진 않다.. (접근은 좋은데!)
	// 그래서 LinkedList가 이런 삽입과 삭제의 단점을 보완하여 나왔음.
	internal class Program
	{

		/******************************************************
		 * 연결리스트 (Linked List)
		 * 
		 * 데이터를 포함하는 노드들을 연결식으로 만든 자료구조
		 * 노드는 데이터와 이전/다음 노드 객체를 참조하고 있음(각자 이전,다음 데이터들의 위치를 알고있음)
		 * 노드가 메모리에 연속적으로 배치되지 않고 이전/다음노드의 위치를 확인
		 * 따로따로 두자!
		 ******************************************************/

		// <링크드리스트 사용>
		void LinkedList()
		{
			LinkedList<string> linkedList = new LinkedList<string>();

			// 링크드리스트 요소 삽입
			linkedList.AddFirst("0번 앞데이터");		// 앞에 추가하는 것도 가능 (AddFirst)
			linkedList.AddFirst("1번 앞데이터");
			linkedList.AddLast("0번 뒤데이터");			// 끝(뒤)에 추가 (AddLast)
			linkedList.AddLast("1번 뒤데이터");

			// 링크드리스트 요소 삭제  O(n)  (그냥 삭제는 O(n) 이고, 특정 노드를 통한 삭제는 O(n)이다.
			linkedList.Remove("1번 앞데이터"); 

			// 링크드리스트 요소 탐색
			LinkedListNode<string> findNode = linkedList.Find("0번 뒤데이터");

			// 링크드리스트 노드를 통한 노드 참조
			// 노드(class)는 이전 주소, 데이터, 다음주소 가 있는 애임) 
			// 링크드리스트는 노드들을 연결한 거임. (노드기반)
			// 하나의 노드에 하나의 데이터만 갖고 있음.
			LinkedListNode<string> prevNode = findNode.Previous;
			LinkedListNode<string> nextNode = findNode.Next;
			
			// 링크드리스트 노드를 통한 노드 삽입
			linkedList.AddBefore(findNode, "찾은노드 앞데이터");
			linkedList.AddAfter(findNode, "찾은노드 뒤데이터");

			// 링크드리스트 노드를 통한 삭제 
			linkedList.Remove(findNode);
		
			// 링크드리스트는 인덱스를 통한 접근이 불가능. 인덱스가 없음
		}


		// <LinkedList의 시간복잡도>
		// 접근		탐색	삽입	삭제
		// O(n)		O(n)	O(1)	O(1)			// 깃허브에 잘못되었고 이게 정답임.

		// 노드를 알고 있는 상태에서 삭제를 하니까 0(1)가 되는거야. 특정 노드를 삭제해줘! 가 되니까.
		// 갖고 있는 모든 노드의 정보를 알고 있거든.

		// 접근에 있어서는 링크드리스트는 비효율적임.
		// 링크드리스트는 가비지콜렉터에 무리가 많이 감... 하나씩 삭제하는 게 자유롭다보니. 그래서 C#에서 많이 안 씀


		// 1. 단방향 링크드리스트 : 이전으로 되돌아가지 못함. 데이터 용량을 줄일 수 있음.
		// 2. 양방향 링크드리스트 : 서로의 주소를 알고 있음. 데이터 용량이 많아짐.
		// 3. 원형(환형) 링크드리스트 : 양방향인데, 마지막 노드는 첫번째 노드를 가리킴. 첫번째 노드도 마지막 노드를 가리킴

		static void Main()
		{
			LinkedList<int> list = new LinkedList<int>();
			list.AddLast(1);
			list.AddLast(2);
			list.AddLast(3);
			list.AddLast(4);
		}
	}
}