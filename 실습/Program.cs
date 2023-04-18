using System.Drawing;

namespace 실습
{
	// 선형리스트 구현 - msdn C# List 참고
	// 인덱서[], Add, Remove, Find, FindIndex, Count 등
	internal class Program
	{
		// 리스트는 배열이다. 다만 배열과 다른 점은 동적할당이 가능하다는 것이다.
		// int[] Array = new Array 배열은 이렇게 만드니까 리스트도 이런식으로 만들면 된다.
		// 리스트는 템플릿으로 만들어야 한다. 다른 자료형도 리스트로 만들 수 있도록!
		
		public class List<T>
		{
			public int allSize;					// 총 리스트 사이즈
			int count;						// 현재 들어온 데이터 개수
			public T[] array;				// T 자료형 배열
		
			public List()
			{
				allSize = 5;				// 처음엔 5이라고 가정한다.
				count = 0;					// 처음엔 0
				array = new T[allSize];		// 배열 인스턴스한다.
			}

			// Add 함수 만들기. 데이터를 추가하면 (Add하면) 차례대로 배열에 데이터가 집어넣어진다.
			public void Add(T item)
			{
				if(count == allSize)
				{
					AddSize();
				}
				array[count] = item;
				count++;
			}

			// RemoveAt 함수 만들기. (지정한 index 의 값을 삭제)
			public void RemoveAt(int index)
			{
				if (index < 0 || index >= allSize)				// 범위를 벗어나면,
				{
					throw new IndexOutOfRangeException();       // 예외처리 한다.
				}

				//array[index] = default;

				Array.Copy(array, index + 1, array, index, allSize - index - 1);    // 배열을 복사한다.

				count--;			// 데이터 하나를 지웠으니 count도 하나 감소한다.
			}
				

			// 크기 늘려주는 함수 만들기
			public void AddSize()
			{
				// 배열 5칸이 꽉차면 크기를 늘려준다. 두 배로 늘린다. (전체 allSize에서 두 배 늘리기)
				// 1. allSize * 2 크기의 새로운 배열을 만들기
				T[] array2 = new T[allSize * 2];
				// 2. 새로운 array2배열에 기존 배열 데이터 값을 복사해서 넣는다.
				Array.Copy(array, 0, array2, 0, allSize);
				// 3. size가 2배로 바뀐다.
				allSize += 2;
				// 4. 원래 배열은 자연스레 없어질 것이니 신경쓰지 않는다. 그리고 이제 새로 들어오는 데이터들은 array2에 저장된다.(array를 가리키고 있던 것을 array2로 가리키기)
				array = array2;			//****************** 교수님 ,, 이거 제가 멋대로 하긴 했는데 조금 헷갈려요..
										//****************** 원래 있던 array 배열을 가리키던 주소값이 array2의 주소값으로 바뀌게 되는 게 이 식이 맞나요? (근데 결과는 잘 돌아가는 것 같아요ㅜㅜ)
			}

			// 넣은 값의 인덱스를 추출하는 함수 만들기
			public int IndexOf(int data)	
			{
				return Array.IndexOf(array, data);		// array에서 data가 있는 인덱스를 추출한다.
			}


			public int FindIndex(Predicate<T> match)
			{
				for (int i = 0; i < allSize; i++)
				{
					if (match(array[i]))
						return i;
				}
				return -1;
			}

			public T? Find(Predicate<T> match)						// 조금 어려워서 수업 때 했던 거를 거의 그대로 참고 했습니다 ㅜㅜ
			{
				if (match == null)
					throw new ArgumentNullException("match");       // match가 null이면 안 되므로 예외처리를 합니다.

				for (int i = 0; i < allSize; i++)					// 모든 배열을 반복합니다.
				{
					if (match(array[i]))							// 만약 i번째의 배열이 조건식과 맞다면
						return array[i];							// 그 배열의 데이터를 반환합니다.
				}

				return default(T);									// 위의 값이 나오지 않았다면 기본값을 반환합니다.
			}
		}

		
		
		static void Main(string[] args)
		{
			List<int> list = new List<int>(); // { 1,2,3,4,0,0,0,}
			list.Add(1);
			list.Add(2);
			list.Add(3);
			list.Add(4);
			list.RemoveAt(2);

			int[] arr = new int[5] { 1, 2, 3, 4,5};
		}
	}
}