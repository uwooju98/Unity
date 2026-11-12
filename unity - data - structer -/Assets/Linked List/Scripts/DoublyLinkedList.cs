using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static UnityEngine.Rendering.DebugUI;

public class DoublyLinkedListNode<T>
{
    public T Value { get; set; }

    public DoublyLinkedListNode<T> Previous { get; set; }
    public DoublyLinkedListNode<T> Next { get; set; }
    public DoublyLinkedList<T> List { get; set; }

    public DoublyLinkedListNode(T value)
    {
        Value = value;
    }
}

public class DoublyLinkedList<T> : IEnumerable<T>
{
    private DoublyLinkedListNode<T> head;
    private DoublyLinkedListNode<T> tail;

    public int Count { get; private set; }

    public DoublyLinkedListNode<T> First => head;
    public DoublyLinkedListNode<T> Last => tail;

    public IEnumerator<T> GetEnumerator()
    {
        DoublyLinkedListNode<T> current = head;
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

    public DoublyLinkedListNode<T> AddFirst(T value)
    {
        var node = new DoublyLinkedListNode<T>(value);
        node.List = this;

        if (head == null)
        {
            head = node;
        }
        else
        {
            var current = head;
            head = node;
            node.Next = current;
            current.Previous = head;
        }

        if (tail == null)
        {
            tail = node;
        }
        Count++;
        return node;
    }

    public DoublyLinkedListNode<T> AddLast(T value)
    {
        var node = new DoublyLinkedListNode<T>(value);
        node.List = this;


        if (tail == null)
        {
            head = node;
            tail = node;
        }
        else
        {
            tail.Next = node;
            node.Previous = tail;
            tail = node;
        }
        Count++;
        return node;
    }

    public DoublyLinkedListNode<T> AddAfter(DoublyLinkedListNode<T> node, T value)
    {
        ValidataNode(node);

        if (node == tail)
        {
            return AddLast(value);
        }

        var newNode = new DoublyLinkedListNode<T>(value);
        newNode.List = this;

        var current = node.Next;
        node.Next = newNode;
        newNode.Previous = node;
        newNode.Next = current;
        current.Previous = newNode;

        Count++;
        return newNode;
    }

    public DoublyLinkedListNode<T> AddBefore(DoublyLinkedListNode<T> node, T value)
    {
        ValidataNode(node);

        if (node == head)
        {
            return AddFirst(value);
        }

        var newNode = new DoublyLinkedListNode<T>(value);
        newNode.List = this;

        var current = node.Previous;
        node.Previous = newNode;
        newNode.Next = node;
        newNode.Previous = current;
        current.Next = newNode;

        Count++;
        return newNode;
    }

    public void RemoveFirst()
    {
        if (head == null)
        {
            throw new InvalidOperationException("리스트가 비어있습니다.");
        }
        Remove(head);
    }

    public void RemoveLast()
    {
        if (tail == null)
        {
            throw new InvalidOperationException("리스트가 비어있습니다.");
        }
        Remove(tail);
    }

    public bool Remove(T value)
    {
        var node = Find(value);
        if (node == null)
        {
            return false;
        }
        Remove(node);
        return true;
    }

    public void Remove(DoublyLinkedListNode<T> node)
    {
        ValidataNode(node);

        if (head == node)
        {
            if (node == tail)
            {
                head = null;
                tail = null;
            }
            else
            {
                head = node.Next;
                node.Next.Previous = null;
            }       
        }
        else if (node == tail)
        {
            tail = node.Previous;
            node.Previous.Next = null;
        }
        else
        {
            var currentPrevious = node.Previous;
            var currentNext = node.Next;

            currentPrevious.Next = currentNext;
            currentNext.Previous = currentPrevious;
        }
        

        node.Previous = null;
        node.Next = null;
        node.List = null;

        Count--;
    }

    public DoublyLinkedListNode<T> Find(T value)
    {
        var comparer = EqualityComparer<T>.Default;
        DoublyLinkedListNode<T> current = head;
        while (current != null)
        {
            if (comparer.Equals(current.Value, value))
            {
                return current;
            }
            current = current.Next;
        }
        return null;
    }

    public IEnumerable<T> Backward()
    {
        var current = tail;

        while (current != null)
        {
            yield return current.Value;
            current = current.Previous;
        }
    }

    public bool Contains(T value)
    {
        return Find(value) != null;
    }

    public void Clear()
    {
        var current = head;
        while (current != null)
        {
            var next = current.Next;
            current.Previous = null;
            current.Next = null;
            current.List = null;
            current = next;
        }

        head = tail = null;
        Count = 0;
    }

    private void ValidataNode(DoublyLinkedListNode<T> node)
    {
        if (node == null)
        {
            throw new ArgumentNullException(nameof(node));
        }
        if (node.List != this)
        {
            throw new InvalidOperationException("이 리스트에 속한 노드가 아닙니다.");
        }
    }
}