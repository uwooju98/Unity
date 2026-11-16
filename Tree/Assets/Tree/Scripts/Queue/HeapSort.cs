using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public static class HeapSort
{
    public static void Sort<T>(T[] array)
    {
        Sort(array, null);
    }

    public static void Sort<T>(T[] array, IComparer<T> comparer)
    {
        // TODO 1: null 검사
        if (array == null)
        {
            throw new ArgumentNullException("없어요");
        }

        if (comparer == null)
        {
            comparer = Comparer<T>.Default;
        }

        var lastParentIndex = array.Length / 2 - 1;

        for (int i = lastParentIndex; i >= 0; i--)
        {
            SiftDown(array, i, array.Length, comparer);
        }

        int n = array.Length;

        for (int end = n - 1; end > 0; end--)
        {
            T temp = array[0];
            array[0] = array[end];
            array[end] = temp;
            SiftDown(array, 0, end, comparer);
        }


        //// TODO 2: 모든 원소를 우선순위 큐에 넣는다

        //var queue = new PriorityQueue<T, T>(comparer);

        ////for (int i = 0; i < array.Length; i++)
        ////{
        ////    queue.Enqueue(array[i], array[i]);
        ////}

        //foreach (T item in array)
        //{
        //    queue.Enqueue(item, item);
        //}



        //// TODO 3: 하나씩 꺼내 배열 앞부터 채운다

        //for (int i = 0; i < array.Length; i++)
        //{
        //    array[i] = queue.Dequeue();
        //}
        //이건 정렬은 아니고 힙으로 만드는거임
    }

    private static void SiftDown<T>(T[] array, int index, int size, IComparer<T> comparer)
    {
        while (true)
        {
            int leftChildIndex = 2 * index + 1;
            int rightChildIndex = 2 * index + 2;
            int largestIndex = index;

            if (leftChildIndex < size &&
                comparer.Compare(
                    array[leftChildIndex],
                    array[largestIndex]) > 0)
            {
                largestIndex = leftChildIndex;
            }

            if (rightChildIndex < size &&
                comparer.Compare(
                    array[rightChildIndex],
                    array[largestIndex]) > 0)
            {
                largestIndex = rightChildIndex;
            }

            if (index == largestIndex)
            {
                break;
            }

            //Swap(index, largestIndex);
            T temp = array[index];
            array[index] = array[largestIndex];
            array[largestIndex] = temp;
            index = largestIndex;
        }
    }
}