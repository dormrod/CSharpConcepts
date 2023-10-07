using System;

namespace CSharpConcepts.DataStructures;

public class MyQueue<T>
{
    private int _head;
    private int _tail;
    private int Count => _tail - _head;
    private readonly T[] _queue;
    
    public MyQueue(int capacity)
    {
        _queue = new T[capacity];
    }

    public void Enqueue(T item)
    {
        if (Count >= _queue.Length)
            throw new InvalidOperationException("Cannot enqueue item as queue is full.");

        _queue[_tail++ % _queue.Length] = item;
    }

    public T Dequeue()
    {
        if (Count == 0)
            throw new InvalidOperationException("Cannot dequeue item as stack is empty.");

        var item  = _queue[_head++ % _queue.Length];
        
        // Keep head and tail within bounds
        if (_head == _queue.Length)
        {
            _head -= _queue.Length;
            _tail -= _queue.Length;
        }

        return item;
    }

    public T Peek()
    {
        if (_head == _tail)
            throw new InvalidOperationException("Cannot peek item as queue is empty.");

        return _queue[_head];
    }

    public void Clear()
    {
        _head = 0;
        _tail = 0;
    }
}