using System;
using System.Collections;
using System.Collections.Generic;


public class FowardLinkedList<T> : IEnumerable<T>
{
    private class FowardLinkedListNode
    {
        public T Value;
        public FowardLinkedListNode Next;

        public FowardLinkedListNode(T value)
        {
            Value = value;
        }
    }

    private FowardLinkedListNode head;
    private FowardLinkedListNode tail;

    public int Count { get; private set; }
    public T First
    {
        get
        {
            // 비어 있으면 InvalidOperationException
            if (head == null)
            {
                throw new InvalidOperationException("리스트가 비어있습니다.");
            }
            return head.Value;
        }
    }
    public T Last
    {
        get
        {
            if (tail == null)
            {
                throw new InvalidOperationException("리스트가 비어있습니다.");
            }
            return tail.Value;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        FowardLinkedListNode current = head;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void AddFirst(T value)
    {
        var node = new FowardLinkedListNode(value);
        node.Next = head;
        head = node;

        if (tail == null)
        {
            tail = node;
        }
        Count++;
    }

    public void AddLast(T value)
    {
        var node = new FowardLinkedListNode(value);
        if (tail == null)
        {
            head = node;
            tail = node;
        }
        else
        {
            tail.Next = node;
            tail = node;
        }
        Count++;
    }

    public T RemoveFirst()
    {
        if (head == null)
        {
            throw new InvalidOperationException("리스트가 비어있습니다.");
        }
        T value = head.Value;
        head = head.Next;

        if (tail == null)
        {
            tail = null;
        }
        Count--;
        return value;
    }

    public bool Remove(T value)
    {
        var comparer = EqualityComparer<T>.Default;

        FowardLinkedListNode previous = null;
        FowardLinkedListNode current = head;

        while (current != null)
        {
            if (comparer.Equals(current.Value, value))
            {
                if (previous == null)
                {
                    head = current.Next;
                }
                else
                {
                    previous.Next = current.Next;
                }

                if (current == tail)
                {
                    tail = previous;
                }
                Count--;
                return true;
            }

            previous = current;
            current = current.Next;
        }

        return false;
    }

    public bool Contains(T value)
    {
        var comparer = EqualityComparer<T>.Default;
        FowardLinkedListNode current = head;
        while (current != null)
        {
            if (comparer.Equals(current.Value, value))
            {
                return true;
            }
            current = current.Next;
        }

        return false;
    }

    public void Clear()
    {
        head = tail = null;
        Count = 0;
    }
}