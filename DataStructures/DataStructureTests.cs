using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CSharpConcepts.DataStructures;

public class DataStructureTests
{
    [Test]
    public void Stack_CanPushPeekAndPopValues()
    {
        var capacity = 5;
        var myStack = new MyStack<int>(capacity);
        var sysStack = new Stack<int>(capacity);

        // Push items on the stack up to capacity and check peek gives the last item 
        AddItemsToStack(capacity);

        Assert.That(myStack.Peek(), Is.EqualTo(capacity - 1));
        Assert.That(sysStack.Peek(), Is.EqualTo(capacity - 1));

        // Check the capacity can't be exceeded for myStack (the system implementation auto resizes)
        Assert.Throws<InvalidOperationException>(() => myStack.Push(capacity + 1));

        // Check pop returns the successive last items
        for (int i = capacity - 1; i >= 0; --i)
        {
            RemoveItemFromStackAndAssertValue(i);
        }

        // Check cannot perform a pop on an empty stack
        Assert.Throws<InvalidOperationException>(() => myStack.Pop());
        Assert.Throws<InvalidOperationException>(() => sysStack.Pop());

        // Check that clear resets the stack
        myStack.Push(42);
        sysStack.Push(42);
        myStack.Clear();
        sysStack.Clear();
        AddItemsToStack(capacity);

        Assert.That(myStack.Peek(), Is.EqualTo(capacity - 1));
        Assert.That(sysStack.Peek(), Is.EqualTo(capacity - 1));

        void AddItemsToStack(int n)
        {
            for (int i = 0; i < n; ++i)
            {
                myStack.Push(i);
                sysStack.Push(i);
            }
        }

        void RemoveItemFromStackAndAssertValue(int expected)
        {
            Assert.That(myStack.Pop(), Is.EqualTo(expected));
            Assert.That(sysStack.Pop(), Is.EqualTo(expected));
        }
    }

    [Test]
    public void Queue_CanEnqueuePeekAndDequeueValues()
    {
        var capacity = 5;
        var myQueue = new MyQueue<int>(capacity);
        var sysQueue = new Queue<int>(capacity);

        // Enqueue items on the queue up to capacity and check peek gives the first item
        AddItemsToQueue(capacity);
        Assert.That(myQueue.Peek(), Is.EqualTo(0));
        Assert.That(sysQueue.Peek(), Is.EqualTo(0));

        // Check the capacity can't be exceeded for myQueue (the system implementation auto resizes)
        Assert.Throws<InvalidOperationException>(() => myQueue.Enqueue(capacity + 1));
        
        // Check dequeue returns the successive first items
        for (int i = 0; i < capacity; ++i)
        {
            RemoveItemFromQueueAndAssertValue(i);
        }
        
        // Check cannot perform a dequeue on an empty queue
        Assert.Throws<InvalidOperationException>(() => myQueue.Dequeue());
        Assert.Throws<InvalidOperationException>(() => sysQueue.Dequeue());
        
        // Check that partially filling and emptying the queue is always first in first out
        AddItemsToQueue(3);
        RemoveItemFromQueueAndAssertValue(0);
        RemoveItemFromQueueAndAssertValue(1);
        
        AddItemsToQueue(4);
        RemoveItemFromQueueAndAssertValue(2);
        RemoveItemFromQueueAndAssertValue(0);
        RemoveItemFromQueueAndAssertValue(1);
        
        AddItemsToQueue(2);
        RemoveItemFromQueueAndAssertValue(2);
        RemoveItemFromQueueAndAssertValue(3);
        RemoveItemFromQueueAndAssertValue(0);

        // Check that clear resets the queue
        myQueue.Clear();
        sysQueue.Clear();
        AddItemsToQueue(capacity);
        Assert.That(myQueue.Peek(), Is.EqualTo(0));
        Assert.That(sysQueue.Peek(), Is.EqualTo(0));

        void AddItemsToQueue(int n)
        {
            for (int i = 0; i < n; ++i)
            {
                myQueue.Enqueue(i);
                sysQueue.Enqueue(i);
            }
        }
        
        void RemoveItemFromQueueAndAssertValue(int expected)
        {
            Assert.That(myQueue.Dequeue(), Is.EqualTo(expected));
            Assert.That(sysQueue.Dequeue(), Is.EqualTo(expected));
        }
    }

    [Test]
    public void LinkedList_CanAddAndRemoveValues()
    {
        var myLinkedList = new MyLinkedList<string>();
        var sysLinkedList = new LinkedList<string>();
        
        // Add first and last values
        var myCurrent = myLinkedList.AddFirst("the");
        var sysCurrent = sysLinkedList.AddFirst("the");
        myLinkedList.AddFirst("Then");
        sysLinkedList.AddFirst("Then");

        myLinkedList.AddLast("dog's");
        sysLinkedList.AddLast("dog's");
        myLinkedList.AddLast("bowl");
        sysLinkedList.AddLast("bowl");
        
        // Add intermediate values after current value
        AddAfter(new [] {"quick", "fox", "jumped", "over", "the", "lazy"});

        // Find a value and add before current value
        myCurrent = myLinkedList.Find("fox");
        sysCurrent = sysLinkedList.Find("fox");
        myCurrent = myLinkedList.AddBefore(myCurrent, "brown");
        sysCurrent = sysLinkedList.AddBefore(sysCurrent, "brown");
        
        myCurrent = myLinkedList.Find("lazy");
        sysCurrent = sysLinkedList.Find("lazy");
        myCurrent = myLinkedList.AddBefore(myCurrent, "very");
        sysCurrent = sysLinkedList.AddBefore(sysCurrent, "very");
        
        // Iterate and build the sentence, verifying correct
        Assert.That(BuildString(myLinkedList), Is.EqualTo("Then the quick brown fox jumped over the very lazy dog's bowl"));
        Assert.That(BuildString(sysLinkedList), Is.EqualTo("Then the quick brown fox jumped over the very lazy dog's bowl"));

        // Remove values from the start, end and middle
        RemoveFirstAndLast(2);
        FindAndRemove(new[] {"fox", "the"});
        
        // Iterate and build the sentence, verifying correct
        Assert.That(BuildString(myLinkedList), Is.EqualTo("quick brown jumped over very lazy"));
        Assert.That(BuildString(sysLinkedList), Is.EqualTo("quick brown jumped over very lazy"));
        
        // Verify clear works
        myLinkedList.Clear();
        sysLinkedList.Clear();
        Assert.That(myLinkedList.First, Is.Null);
        Assert.That(myLinkedList.Last, Is.Null);
        Assert.That(sysLinkedList.First, Is.Null);
        Assert.That(sysLinkedList.Last, Is.Null);
        
        void AddAfter(string[] values)
        {
            foreach (var value in values)
            {
                myCurrent = myLinkedList.AddAfter(myCurrent, value);
                sysCurrent = sysLinkedList.AddAfter(sysCurrent, value);
            }
        }

        string BuildString(IEnumerable<string> linkedList)
            => string.Join(" ", linkedList);

        void RemoveFirstAndLast(int n)
        {
            for (int i = 0; i < n; ++i)
            {
                myLinkedList.RemoveFirst();
                myLinkedList.RemoveLast();
                
                sysLinkedList.RemoveFirst();
                sysLinkedList.RemoveLast();
            }
        }

        void FindAndRemove(string[] values)
        {
            foreach (var value in values)
            {
                myCurrent = myLinkedList.Find(value);
                sysCurrent = sysLinkedList.Find(value);
                myLinkedList.Remove(myCurrent);
                sysLinkedList.Remove(sysCurrent);
            }
        }
    }

    [Test]
    public void HashTable_CanAddGetAndRemoveValues()
    {
        var myHashTable = new MyHashTable<int, int>(10);
        var sysDictionary = new Dictionary<int, int>();

        // Add many values (more than the number of buckets)
        for (int i = 0; i < 100; ++i)
        {
            myHashTable.Add(i, -i);
            sysDictionary.Add(i, -i);
        }
        
        // Verify cannot add duplicate key
        Assert.Throws<InvalidOperationException>(() => myHashTable.Add(50, -50));
        Assert.Throws<ArgumentException>(() => sysDictionary.Add(50, -50));

        // Get many values 
        for (int i = 0; i < 100; ++i)
        {
            Assert.That(myHashTable[i], Is.EqualTo(-i));
            Assert.That(sysDictionary[i], Is.EqualTo(-i));
        }
        
        // Verify cannot get non-existent key
        Assert.Throws<InvalidOperationException>(() =>
        {
            var _ = myHashTable[-50];
        });
        Assert.Throws<KeyNotFoundException>(() =>
        {
            var _ = sysDictionary[-50];
        });
        
        // Verify clear removes entries
        myHashTable.Clear();
        sysDictionary.Clear();
     
        Assert.Throws<InvalidOperationException>(() =>
        {
            var _ = myHashTable[0];
        });
        Assert.Throws<KeyNotFoundException>(() =>
        {
            var _ = sysDictionary[0];
        });
    }
}