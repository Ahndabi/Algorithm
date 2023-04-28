namespace _10.Sorting
{
	internal class Program
	{
		/******************************************************
		 * 선형 정렬
		 * 
		 * 1개의 요소를 재위치시키기 위해 전체를 확인하는 정렬
		 * n개의 요소를 재위치시키기 위해 n개를 확인하는 정렬
		 * 시간복잡도 : O(N^2)
		 * 잘 안 쓰임
		 ******************************************************/

		// <선택정렬>
		// 데이터 중 가장 작은 값부터 하나씩 선택하여 정렬
		// 가장 작은 값을 골라서 맨 앞으로 와. 원래 맨 앞에 있던 값은 제일 작은 애랑 교환. 이걸 반복.
		public static void SelectionSort(IList<int> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				int minIndex = i;
				for (int j = i + 1; j < list.Count; j++)
				{
					if (list[j] < list[minIndex])
						minIndex = j;
				}
				Swap(list, i, minIndex);
			}
		}

		private static void Swap(IList<int> list, int left, int right)
		{
			int temp = list[left];
			list[left] = list[right];
			list[right] = temp;
		}


		// <삽입정렬>
		// 데이터를 하나씩 꺼내어 정렬된 자료 중 적합한 위치에 삽입하여 정렬
		// 2 8 6 이라는 정렬이 있으면, 처음부터 하나하나씩 꺼내보면서 위치를 조정. 2는 일단 그대로 두고,
		// 8 은 2보다 크니까 일단 제자리. 6은 2보단 크고 8보단 작으니까 2랑 8 사이에 넣기. 결국 2 6 8
		// 3 1 2 라면, 3은 일단 그대로. 1은 3 앞으로 감. 그러면 132. 그다음 2는 3보다 작으니까 3앞으로감
		public static void InsertionSort(IList<int> list)
		{
			for (int i = 1; i < list.Count; i++)
			{
				int key = list[i];
				int j;
				for (j = i - 1; j >= 0 && key < list[j]; j--)
				{
					list[j + 1] = list[j];
				}
				list[j + 1] = key;
			}
		}


		// <버블정렬>
		// 서로 인접한 데이터를 비교하여 정렬
		// 최적화 여부 적용 가능??
		public static void BubbleSort(IList<int> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				for (int j = 1; j < list.Count; j++)
				{
					if (list[j - 1] > list[j])
						Swap(list, j - 1, j);
				}
			}
		}





		/******************************************************
		 * 분할정복 정렬
		 * 
		 * 1개의 요소를 재위치시키기 위해 전체의 1/2를 확인하는 정렬
		 * n개의 요소를 재위치시키기 위해 n/2개를 확인하는 정렬
		 * 시간복잡도 : O(NlogN)
		 ******************************************************/

		// <힙정렬>
		// 힙을 이용하여 우선순위가 가장 높은 요소부터 가져와 정렬
		// 우선순위의 Heap의 그 heap 맞음
		// 단점 : 불안정 정렬
		// 실제로는 퀵정렬보다 느릴 수 있는데 평균적으로는 빠르다. 정답은 없음  
		public static void HeapSort(IList<int> list)
		{
			PriorityQueue<int, int> pq = new PriorityQueue<int, int>();

			for (int i = 0; i < list.Count; i++)
			{
				pq.Enqueue(list[i], list[i]);
			}

			for (int i = 0; i < list.Count; i++)
			{
				list[i] = pq.Dequeue();
			}
		}


		// <합병정렬>
		// 데이터를 2분할하여 정렬 후 합병
		// 큰 덩어리에서 반절을 추출함. 그 반절에서 또 반절... 해서 하나의 데이터가 남았을 때 하나 하나씩 비교함
		// 반 남은 것끼리 정렬하고 다른 반 남은 거랑 합침
		// 배열을 복사하여 그 배열을 저장할 새로운 배열을 만들어야 하기 때문에 메모리가 많이 쓰임
		// 다른 정렬과는 다르게 메모리 측면에서는 부담(단점) - 오버헤드
		// 메모리를 할당하기 위한 대기시간 등등(이런 걸 오버헤드라고 하는데 이거 때매 느릴 수 있다는 거야~!)
		// 안정정렬
		public static void MergeSort(IList<int> list, int left, int right)
		{
			if (left == right) return;

			int mid = (left + right) / 2;
			MergeSort(list, left, mid);
			MergeSort(list, mid + 1, right);
			Merge(list, left, mid, right);
		}

		public static void Merge(IList<int> list, int left, int mid, int right)
		{
			List<int> sortedList = new List<int>();
			int leftIndex = left;
			int rightIndex = mid + 1;

			// 분할 정렬된 List를 병합
			while (leftIndex <= mid && rightIndex <= right)
			{
				if (list[leftIndex] < list[rightIndex])
					sortedList.Add(list[leftIndex++]);
				else
					sortedList.Add(list[rightIndex++]);
			}

			if (leftIndex > mid)    // 왼쪽 List가 먼저 소진 됐을 경우
			{
				for (int i = rightIndex; i <= right; i++)
					sortedList.Add(list[i]);
			}
			else  // 오른쪽 List가 먼저 소진 됐을 경우
			{
				for (int i = leftIndex; i <= mid; i++)
					sortedList.Add(list[i]);
			}

			// 정렬된 sortedList를 list로 재복사
			for (int i = left; i <= right; i++)
			{
				list[i] = sortedList[i - left];
			}
		}


		// <퀵정렬>
		// 하나의 피벗을 기준으로 작은값과 큰값을 2분할하여 정렬
		// 987654321 이라는 정렬할 때 시간복잡도가 0{n^2)이됨.. 이런 게 최악인 경우임
		// 단점: 불안정 정렬
		// 실제로 해보면 퀵정렬이 힙정렬보다 빠를 수 있음.
		public static void QuickSort(IList<int> list, int start, int end)
		{
			if (start >= end) return;

			int pivotIndex = start;
			int leftIndex = pivotIndex + 1;
			int rightIndex = end;

			while (leftIndex <= rightIndex) // 엇갈릴때까지 반복
			{
				// pivot보다 큰 값을 만날때까지
				while (list[leftIndex] <= list[pivotIndex] && leftIndex < end)
					leftIndex++;
				while (list[rightIndex] >= list[pivotIndex] && rightIndex > start)
					rightIndex--;

				if (leftIndex < rightIndex)     // 엇갈리지 않았다면
					Swap(list, leftIndex, rightIndex);
				else    // 엇갈렸다면
					Swap(list, pivotIndex, rightIndex);
			}

			QuickSort(list, start, rightIndex - 1);
			QuickSort(list, rightIndex + 1, end);
		}



		static void Main(string[] args)
		{
		}
	}
}