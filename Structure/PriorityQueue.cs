using System.Collections.Generic;
using System.Text;

public interface IPriority
{
    int Priority { get; }
}

public class PriorityQueue<T> where T : IPriority
{
    private List<T> m_Heap;

    public PriorityQueue()
    {
        m_Heap = new List<T>();
    }

    public int Count
    {
        get
        {
            return m_Heap.Count;
        }
    }

    public T Peek()
    {
        if (m_Heap.Count <= 0)
        {
            return default(T);
        }
        return m_Heap[0];
    }

    public bool Contains(T value, out int index)
    {
        for (int i = 0; i < m_Heap.Count; i++)
        {
            if(m_Heap[i].Equals(value))
            {
                index = i;
                return true;
            }
        }
        index = -1;
        return false;
    }

    public T Dequeue()
    {
        if (m_Heap.Count <= 0)
        {
            return default(T);
        }
        T value = m_Heap[0];
        m_Heap.RemoveAt(0);
        Heapify(0);
        return value;
    }

    public void Enqueue(T value)
    {
        m_Heap.Add(value);
        ShiftUp(m_Heap.Count - 1);
    }

    public void ShiftUp(int i)
    {
        while (i > 0)
        {
            int parent = (i - 1) / 2;
            if(m_Heap[parent].Priority > m_Heap[i].Priority)
            {
                Swap(i, parent);
                i = parent;
            }
            else
            {
                break;
            }
        }
    }

    private void Heapify(int i)
    {
        int left = 2 * i + 1;
        int right = left + 1;
        int lowest = i;
        if (left < m_Heap.Count && m_Heap[left].Priority < m_Heap[i].Priority)
        {
            lowest = left;
        }
        if (right < m_Heap.Count && m_Heap[right].Priority < m_Heap[lowest].Priority)
        {
            lowest = right;
        }
        if (lowest != i)
        {
            Swap(lowest, i);
            Heapify(lowest);
        }
    }

    private void Swap(int i, int j)
    {
        T t = m_Heap[i];
        m_Heap[i] = m_Heap[j];
        m_Heap[j] = t;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var i in m_Heap)
        {
            sb.Append(i);
            sb.Append(" ");
        }
        return sb.ToString();
    }
}
