public static class HeapSort
{
    public static void Sort(int[] arr)
    {
        for (int i = arr.Length / 2 - 1; i >= 0 ; i--)
        {
            Heapfiy(arr, i, arr.Length);
        }
        for (int i = arr.Length - 1; i > 0; i--)
        {
            Swap(arr, 0, i);
            Heapfiy(arr, 0, i);
        }
    }

    private static void Heapfiy(int[] arr, int i, int length)
    {
        int left = 2 * i + 1;
        int right = left + 1;
        int highest = i;
        if(left < length && arr[left] > arr[i])
        {
            highest = left;
        }
        if(right < length && arr[right] > arr[highest])
        {
            highest = right;
        }
        if(highest != i)
        {
            Swap(arr, highest, i);
            Heapfiy(arr, highest, length);
        }
    }

    private static void Swap(int[] arr, int i, int j)
    {
        int t = arr[i];
        arr[i] = arr[j];
        arr[j] = t;
    }
}

