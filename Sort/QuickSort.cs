public static class QuickSort
{
    public static void Sort(int[] arr)
    {
        Sort(arr, 0, arr.Length - 1);
    }

    private static void Sort(int[] arr, int left, int right)
    {
        if (left >= right)
        {
            return;
        }
        int pivot = Partition(arr, left, right);
        Sort(arr, left, pivot - 1);
        Sort(arr, pivot + 1, right);
    }

    private static int Partition(int[] arr, int left, int right)
    {
        int pivot = arr[left];
        int start = left, end = right;
        while (start < end)
        {
            while (start < end && arr[start] < pivot)
            {
                start++;
            }

            while (start < end && arr[end] > pivot)
            {
                end--;
            }

            Swap(arr, start, end);
        }

        arr[start] = pivot;
        return start;
    }

    private static void Swap(int[] arr, int i, int j)
    {
        int temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }
}