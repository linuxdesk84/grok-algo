namespace grok_algo;

internal class Chapter02
{
    void SelectionSort(int[] arr)
    {
        for (var i = 0; i < arr.Length; i++)
        {
            var max = arr[i];
            var idx = i;
            for (var j = i; j < arr.Length; j++)
            {
                if (max < arr[j])
                {
                    max = arr[j];
                    idx = j;
                }
            }

            (arr[i], arr[idx]) = (arr[idx], arr[i]);
        }
    }

    public void Test01()
    {
        var n = 20;

        var ah = new ArrayHelper();
        var arr = ah.GenerateRandom(n);
        ah.PrintArr(arr);

        SelectionSort(arr);
        ah.PrintArr(arr);
    }
}