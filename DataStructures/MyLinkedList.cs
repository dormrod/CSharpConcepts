using System.Collections;
using System.Collections.Generic;

namespace CSharpConcepts.DataStructures;

public class MyNode<T>
{
    public MyNode<T>? Next { get; internal set; }
    public MyNode<T>? Previous { get; internal set; }
    public T Value { get; }

    public MyNode(T value, MyNode<T>? next, MyNode<T>? previous)
    {
        Value = value;
        Next = next;
        Previous = previous;
    }
}

public class MyLinkedList<T> : IEnumerable<T>
{
    private MyNode<T>? _head;
    private MyNode<T>? _tail;
    public MyNode<T>? First => _head ?? _tail;
    public MyNode<T>? Last => _tail ?? _head;

    public MyLinkedList()
    {
    }

    public MyNode<T> AddFirst(T value)
    {
        var node = new MyNode<T>(value, _head, null);
        
        if (_head != null)
            _head.Previous = node;

        _tail ??= _head;
        
        _head = node;

        return node;
    }

    public MyNode<T> AddLast(T value)
    {
        var node = new MyNode<T>(value, null, _tail);

        if (_tail != null)
            _tail.Next = node;

        _head ??= _tail;

        _tail = node;

        return node;
    }

    public void RemoveFirst()
    {
        _head = _head?.Next;

        if (_head != null)
            _head.Previous = null;
    }
    
    public void RemoveLast()
    {
        _tail = _tail?.Previous;

        if (_tail != null)
            _tail.Next = null;
    }

    public MyNode<T>? Find(T value)
    {
        var current = First;

        if (current == null || current.Value != null && current.Value.Equals(value))
            return current;

        do
        {
            current = current.Next;
        } while (current != null && current.Value != null && !current.Value.Equals(value));

        return current;
    }

    public MyNode<T> AddAfter(MyNode<T> item, T value)
    {
        var node = new MyNode<T>(value, item.Next, item);

        if (item.Next != null)
            item.Next.Previous = node;
        
        item.Next = node;

        return node;
    }
    
    public MyNode<T> AddBefore(MyNode<T> item, T value)
    {
        var node = new MyNode<T>(value, item, item.Previous);

        if (item.Previous != null)
            item.Previous.Next = node;
        
        item.Previous = node;

        return node;
    }

    public void Remove(MyNode<T> item)
    {
        if (item.Previous != null)
            item.Previous.Next = item.Next;

        if (item.Next != null)
            item.Next.Previous = item.Previous;
    }

    public void Clear()
    {
        _head = null;
        _tail = null;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = First;
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
}