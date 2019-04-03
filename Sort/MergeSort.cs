public static class MergeSort
{
    public static void Sort(int[] arr)
    {
        int[] temp = new int[arr.Length];
        Sort(arr, 0, arr.Length - 1, temp);
    }

    private static void Sort(int[] arr, int left, int right, int[] temp)
    {
        if (left < right)
        {
            int middle = (left + right) / 2;
            Sort(arr, left, middle, temp);
            Sort(arr, middle + 1, right, temp);
            Merge(arr, left, middle, right, temp);
        }
    }

    private static void Merge(int[] arr, int left, int middle, int right, int[] temp)
    {
        int i = left;
        int j = middle + 1;
        int t = 0;
        while (i <= middle && j <= right)
        {
            if(arr[i] < arr[j])
            {
                temp[t++] = arr[i++];
            }
            else
            {
                temp[t++] = arr[j++];
            }
        }

        while (i <= middle)
        {
            temp[t++] = arr[i++];
        }
        while (j <= right)
        {
            temp[t++] = arr[j++];
        }
        t = 0;
        while (left <= right)
        {
            arr[left++] = temp[t++];
        }
    }
}

