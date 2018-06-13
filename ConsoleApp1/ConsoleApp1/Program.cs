using System;
using System.Collections.Generic;

namespace Olimpiada
{
    class Program
    {
        static void Delete(int n, int k, int[,] M)
        {
            for (int i = 0; i < n; i++)
            {
                M[k, i] = 0;
                M[i, k] = 0;
            }
        }

        static void copy(List<int> list1, List<int> list2)  //копіювання цепочки(списка), при умові що вона має на данний момент макс довжину
        {
            list1.Clear();
            for (int i = 0; i < list2.Count; i++)
                list1.Add(list2[i]);
        }


        static void Find(int from, int N, int[,] C, List<int> current, List<int> maximum)
        {

            for (int j = 0; j < N; j++)
                if (C[from, j] == 1)
                {
                    int[,] C1 = (int[,])C.Clone();
                    current.Add(j);
                    if (current.Count > maximum.Count)
                    {
                        copy(maximum, current);
                    }
                    for (int i = 0; i < N; i++)
                        if (i != j && C1[from, j] == 1)
                            Delete(N, i, C1);
                    Delete(N, from, C1);
                    Find(j, N, C1, current, maximum);
                }
            current.RemoveAt(current.Count - 1);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите размерность матрицы:");
            int n = Convert.ToInt32(Console.ReadLine());
            int[,] matrix = new int[n, n];
            Console.WriteLine("Как? \n1 - генерация \n2 - ввод");
            int choise = Convert.ToInt32(Console.ReadLine());
            if (choise == 1)
            {
                Random rnd = new Random();
                for (int b = 0; b < n; b++)
                {
                    for (int v = 0; v < n; v++)
                    {
                        matrix[b, v] = rnd.Next(2);
                        matrix[b, b] = 0;
                        Console.Write(matrix[b, v].ToString() + "\t");
                    }
                    Console.WriteLine("\n");
                }
            }
            else
            {
                Console.WriteLine("Введите матрицу:");
                for (int i = 0; i < n; i++)
                {
                    string[] s = Console.ReadLine().Split(' ');
                    for (int j = 0; j < n; j++)
                    {
                        matrix[i, j] = Convert.ToInt32(s[j]);
                        matrix[i, i] = 0;
                    }
                }

            }
            List<int> curr = new List<int> { };
            List<int> max = new List<int> { };

            for (int i = 0; i < n; i++)
            {
                curr.Add(i);
                Find(i, n, matrix, curr, max);
            }

            Console.WriteLine("Имеем цепочку:");
            for (int i = 0; i < max.Count; i++)
                Console.Write((max[i] + 1) + " ");
            Console.WriteLine("Кол-во удаленных колец: " + (n - max.Count));

        }

    }
}
