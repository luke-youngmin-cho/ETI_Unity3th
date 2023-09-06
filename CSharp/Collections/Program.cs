using System.Collections;

namespace Collections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();

            MyDynamicArray<int> da1 = new MyDynamicArray<int>();
            da1.Add(2);
            da1.Add(3);

            foreach (var item in da1)
            {
                Console.WriteLine(item);
            }

            // using 구문 : IDisposable 객체의 Dispose() 호출을 보장하는 구문
            using (IEnumerator<int> e = da1.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    Console.WriteLine(e.Current);
                }
                e.Reset();
            }


            if (da1.Remove(4))
                da1.RemoveAt(0);

            if (da1.FindIndex((item) =>
            {
                return item < 4;
            }) < 0)
                Console.WriteLine("I couldnt found something smaller than 4 !!");

            // 람다식 : 컴파일러가 판단할 수 있는 내용들을 전부 생략하고 => 기호로 람다식이란것만 명시해주면됨
            if (da1.FindIndex((item) => item < 4) < 0)
                Console.WriteLine("I couldnt found something smaller than 4 !!");


            Queue<int> queue = new Queue<int>();
            queue.Enqueue(3);
            queue.Enqueue(5);
            queue.Enqueue(2);
            Console.WriteLine(queue.Dequeue());
            if (queue.Peek() > 0)
            {
                Console.WriteLine("First is bigger than 0");
            }

            Stack<int> stack = new Stack<int>();
            stack.Push(3);
            stack.Push(5);
            stack.Push(2);
            Console.WriteLine(stack.Pop());
            if (stack.Peek() > 0)
            {
                Console.WriteLine("Last is bigger than 0");
            }

            Dictionary<string, float> points = new Dictionary<string, float>();
            points.Add("철수", 90.0f);
            Console.WriteLine(points["철수"]);
        }

        static bool IsSmallerThan4(int item)
        {
            return item < 4;
        }

        //(int item) => item < 4;

    }
}