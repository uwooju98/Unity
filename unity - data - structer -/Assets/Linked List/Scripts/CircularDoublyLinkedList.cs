using System;
using System.Collections;
using System.Collections.Generic;



public class CircularDoublyLinkedListNode<T>
{
    public T Value { get; set; }
    public CircularDoublyLinkedList<T> List { get; set; }

    // 리스트 내부 전용 링크 = 마지막 노드의 next가 처음 노드인 원형 연결
    public CircularDoublyLinkedListNode<T> next;
    public CircularDoublyLinkedListNode<T> prev;

    // 사용자가 보는 링크 = 이음새(마지막 → 처음)에서 null을 돌려줘 원형을 숨긴다
    public CircularDoublyLinkedListNode<T> Next => next == null || next == List.First ? null : next;
    public CircularDoublyLinkedListNode<T> Previous => prev == null || this == List.First ? null : prev;

    public CircularDoublyLinkedListNode(T value)
    {
        Value = value;
    }
}

public class CircularDoublyLinkedList<T> : IEnumerable<T>
{
    private CircularDoublyLinkedListNode<T> head;

    public int Count { get; private set; }

    public CircularDoublyLinkedListNode<T> First => head;
    public CircularDoublyLinkedListNode<T> Last => head?.prev;

    public IEnumerator<T> GetEnumerator()
    {
        CircularDoublyLinkedListNode<T> current = head;

        if (head == null)
        {
            yield break;
        }

        do
        {
            yield return current.Value;
            current = current.next;
        } while (current != head);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }


    public CircularDoublyLinkedListNode<T> AddFirst(T value)
    {
        var node = new CircularDoublyLinkedListNode<T>(value);
        node.List = this;

        if (head == null)
        {
            head = node;
            node.next = node;
            node.prev = node;
        }
        else
        {
            InsertBefore(head, node);
            head = node;
        }

        Count++;
        return node;
    }

    public CircularDoublyLinkedListNode<T> AddLast(T value)
    {
        var node = new CircularDoublyLinkedListNode<T>(value);
        node.List = this;

        if (head == null)
        {
            head = node;
            node.next = node;
            node.prev = node;
        }
        else
        {
            InsertBefore(head, node);
        }

        Count++;
        return node;
    }

    public CircularDoublyLinkedListNode<T> AddAfter(CircularDoublyLinkedListNode<T> node, T value)
    {
        ValidateNode(node);

        var newNode = new CircularDoublyLinkedListNode<T>(value);
        newNode.List = this;

        InsertBefore(node.next, newNode);

        Count++;
        return newNode;
    }

    public CircularDoublyLinkedListNode<T> AddBefore(CircularDoublyLinkedListNode<T> node, T value)
    {
        ValidateNode(node);

        var newNode = new CircularDoublyLinkedListNode<T>(value);
        newNode.List = this;

        InsertBefore(node, newNode);

        if (node == head)
        {
            head = newNode;
        }

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
        if (head == null)
        {
            throw new InvalidOperationException("리스트가 비어있습니다.");
        }
        Remove(head.prev);
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

    public void Remove(CircularDoublyLinkedListNode<T> node)
    {
        ValidateNode(node);


        if (head == node && node == node.prev)
        {
            head = null;
        }
        else
        {
            node.next.prev = node.prev;
            node.prev.next = node.next;

            if (node == head)
            {
                head = node.next;
            }
        }

        node.prev = null;
        node.next = null;
        node.List = null;

        Count--;
    }

    public CircularDoublyLinkedListNode<T> Find(T value)
    {
        var comparer = EqualityComparer<T>.Default;
        CircularDoublyLinkedListNode<T> current = head;
        while (current != null)
        {
            if (comparer.Equals(current.Value, value))
            {
                return current;
            }

            current = current.next;

            if (current == head)
            {
                break;
            }
        }
        return null;
    }

    public IEnumerable<T> Backward()
    {
        if (head == null)
        {
            yield break;
        }

        var current = head.prev;

        do
        {
            yield return current.Value;
            current = current.prev;
        } while (current != Last);
    }

    public bool Contains(T value)
    {
        return Find(value) != null;
    }

    public void Clear()
    {
        var current = head;

        if (head != null)
        {
            do
            {
                var next = current.next;
                current.prev = null;
                current.next = null;
                current.List = null;
                current = next;
            } while (current != head);
        }

        head = null;
        Count = 0;
    }

    private void InsertBefore(CircularDoublyLinkedListNode<T> target, CircularDoublyLinkedListNode<T> newNode)
    {
        var current = target.prev;
        target.prev = newNode;
        newNode.next = target;
        newNode.prev = current;
        current.next = newNode;
    }


    private void ValidateNode(CircularDoublyLinkedListNode<T> node)
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