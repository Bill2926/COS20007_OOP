List<int> A = new List<int>();
A.Add(2);
A.Add(69);
A.Add(35);
A.Add(100);

for  (int i = 0; i < A.Count; i++)
{
    Console.WriteLine(A[i]);
}

Stack<int> B = new Stack<int>();
B.Push(2);
B.Push(3);
B.Push(4);
B.Push(5);
Console.WriteLine(B.Pop());

Queue<int> C = new Queue<int>();
C.Enqueue(2);
C.Enqueue(9);
C.Enqueue(11);
C.Enqueue(200);
Console.WriteLine(C.Dequeue());

Console.ReadKey();