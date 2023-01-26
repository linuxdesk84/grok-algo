namespace grok_algo;

internal class Chapter04
{
    int RecursionSum(int[] arr, int index, int sum)
    {
        if (index >= arr.Length)
            return 0;
        return arr[index] + RecursionSum(arr, index + 1, sum);
    }

    public void Test01()
    {
        int n = 3;
        var ah = new ArrayHelper();
        var arr = ah.GenerateRandom(n);
        ah.PrintArr(arr);

        var sum  = RecursionSum(arr, 0, 0);
        Console.WriteLine(sum);
    }

    int[] QuickSort(int[] arr)
    {
        if (arr.Length <= 1)
            return arr;

        var pivot = arr[0];

        var listLeft = arr[1..].Where(x => x < pivot).Select(x => x).ToArray();
        var listRight = arr[1..].Where(x => x >= pivot).Select(x => x).ToArray();

        var left = QuickSort(listLeft.ToArray());
        var right = QuickSort(listRight.ToArray());

        var arr2 = new int[arr.Length];
        var idx = 0;

        foreach (var numLeft in left) arr2[idx++] = numLeft;
        arr2[idx++] = pivot;
        foreach (var numRight in right) arr2[idx++] = numRight;

        return arr2;
    }

    public void Test02()
    {
        var n = 35;
        var ah = new ArrayHelper();
        var arr = ah.GenerateRandom(n);
        ah.PrintArr(arr);

        var arr2 = QuickSort(arr);
        ah.PrintArr(arr2);
    }
}