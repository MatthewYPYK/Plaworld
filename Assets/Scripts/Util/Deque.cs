using System;
using System.Collections.Generic;

public class Deque<T>
{
    private List<T> list;

    public Deque()
    {
        list = new List<T>();
    }

    public int Count
    {
        get { return list.Count; }
    }

    public void AddFront(T item)
    {
        list.Insert(0, item);
    }

    public void AddRear(T item)
    {
        list.Add(item);
    }

    public T RemoveFront()
    {
        if (list.Count == 0)
        {
            throw new InvalidOperationException("Deque is empty");
        }

        T removedItem = list[0];
        list.RemoveAt(0);
        return removedItem;
    }

    public T RemoveRear()
    {
        if (list.Count == 0)
        {
            throw new InvalidOperationException("Deque is empty");
        }

        T removedItem = list[list.Count - 1];
        list.RemoveAt(list.Count - 1);
        return removedItem;
    }

    public bool Remove(T item)
    {
        return list.Remove(item);
    }

    public T PeekFront()
    {
        if (list.Count == 0)
        {
            throw new InvalidOperationException("Deque is empty");
        }

        return list[0];
    }

    public T PeekRear()
    {
        if (list.Count == 0)
        {
            throw new InvalidOperationException("Deque is empty");
        }

        return list[list.Count - 1];
    }
}
