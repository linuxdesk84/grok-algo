namespace grok_algo;

internal class Chapter01
{
    int BinarySearch(int[] arr, int y0, int lower, int upper)
    {
        if (lower > upper) return -1; // not correct index

        var mid = (lower + upper) / 2;

        if (arr[mid] == y0)
            return mid; // found!

        return y0 < arr[mid]
            ? BinarySearch(arr, y0, lower, mid - 1)
            : BinarySearch(arr, y0, mid + 1, upper);
    }

        

    public void Test01()
    {
        var n = 35;
        var ah = new ArrayHelper();
        var arr = ah.GenerateSorted(n);
        ah.PrintArr(arr);

        var rand = new Random(DateTime.Now.Microsecond);

        for (var i = 0; i < 20; i++)
        {
            var guest = rand.Next(100);
            var idx = BinarySearch(arr, guest, 0, arr.Length - 1);

            var res = idx < 0 ? $"not found!" : $"found! idx={idx}";
            Console.WriteLine($"guest={guest} -> {res}");
        }
    }
}