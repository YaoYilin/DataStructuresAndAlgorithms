using System.Collections.Generic;

public class KeyedPriorityQueue<K, V, P> where V : class
{
    List<HeapNode<K, V, P>> heap = new List<HeapNode<K, V, P>>();
    private int size;
    private HeapNode<K, V, P> placeHolder = default(HeapNode<K, V, P>);
    private Comparer<P> priorityComparer = Comparer<P>.Default;
    public KeyedPriorityQueue()
    {
        // dummy zeroth element, heap is 1-based
        heap.Add(default(HeapNode<K, V, P>));
    }

    public void Enqueue(K key, V value, P priority)
    {
        int i = ++size;
        if(i == heap.Count)
        {
            heap.Add(placeHolder);
        }

        int parent = i / 2;
        while (i > 1 && IsHigher(priority, heap[parent].Priority))
        {
            heap[i] = heap[parent];
            i = parent;
            parent = i / 2;
        }
        heap[i] = new HeapNode<K, V, P>(key, value, priority);
    }

    public V Dequeue()
    {
        if (size <= 0)
        {
            return null;
        }
        V ret = heap[1].Value;
        heap[1] = heap[size];
        heap[size--] = placeHolder;
        Heapify(1);
        return ret; 
    }

    public void Clear()
    {
        heap.Clear();
        size = 0;
    }

    public V Peek()
    {
        return size > 1 ? null : heap[1].Value;
    }

    public int Count
    {
        get { return size; }
    }

    protected virtual bool IsHigher(P p1, P p2)
    {
        return priorityComparer.Compare(p1, p2) >= 1;
    }

    private void Heapify(int i)
    {
        int left = 2 * i;
        int right = left + 1;
        int highest = i;

        // 取出来三个中最大的顶点
        if(left <= size && IsHigher(heap[left].Priority, heap[i].Priority))
        {
            highest = left;
        }
        if(right <= size && IsHigher(heap[right].Priority, heap[highest].Priority))
        {
            highest = right;
        }

        // 最大的顶点不是输入的顶点，然后交换
        if(highest != i)
        {
            Swap(i, highest);
            Heapify(highest);
        }
    }

    private void Swap(int i, int j)
    {
        HeapNode<K, V, P> temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }

    private struct HeapNode<_K, _V, _P>
    {
        public _K Key;
        public _V Value;
        public _P Priority;
        public HeapNode(_K key, _V value, _P priority)
        {
            Key = key;
            Value = value;
            Priority = priority;
        }
    }
}

