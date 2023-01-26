namespace grok_algo;

internal class ArrayHelper
{
    public void PrintArr(int[] arr) => Console.WriteLine(string.Join(';', arr));
    public void PrintArr(int[,] arr)
    {
        for (var row = 0; row < arr.GetLength(0); row++)
        {
            for (var col = 0; col < arr.GetLength(1); col++)
            {
                Console.Write($"{arr[row, col]}\t");
            }
            Console.WriteLine("");
        }
    }


    public int[] GenerateSorted(int n)
    {
        var arr = GenerateRandom(n);
        Array.Sort(arr);

        return arr;
    }

    public int[] GenerateRandom(int n)
    {
        var rand = new Random(DateTime.Now.Microsecond);

        var arr = new int[n];
        for (var i = 0; i < n; i++)
        {
            arr[i] = rand.Next(100);
        }

        return arr;
    }
}