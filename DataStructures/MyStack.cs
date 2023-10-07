using System;

namespace CSharpConcepts.DataStructures;

public class MyStack<T>
{
    private int _count;
    private readonly T[] _stack;
    
    public MyStack(int capacity)
    {
        _stack = new T[capacity];
    }

    public void Push(T item)
    {
        if (_count >= _stack.Length)
            throw new InvalidOperationException("Cannot push item as stack is full.");

        _stack[_count++] = item;
    }

    public T Pop()
    {
        if (_count == 0)
            throw new InvalidOperationException("Cannot pop item as stack is empty.");

        return _stack[--_count];
    }

    public T Peek()
    {
        if (_count == 0)
            throw new InvalidOperationException("Cannot peek item as stack is empty.");

        return _stack[_count - 1];
    }

    public void Clear()
    {
        _count = 0;
    }
}