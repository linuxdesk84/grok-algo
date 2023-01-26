namespace grok_algo;

internal class Chapter09
{
    private SetOfItems KnapsackProblem(IReadOnlyList<Item> items, int knapsackVolume)
    {
        var table = new SetOfItems[items.Count, knapsackVolume];

        for (var i = 0; i < items.Count; i++)
        {
            var item = items[i];

            for (var j = 0; j < knapsackVolume; j++)
            {
                var previousSet = i > 0 ? table[i - 1, j] : new SetOfItems();

                var volume = j + 1;
                if (volume < item.Volume)
                {
                    table[i, j] = previousSet;
                    continue;
                }

                var currentSet = new SetOfItems(item);

                var remainingVolume = volume - item.Volume;
                if (i > 0 && remainingVolume > 0)
                {
                    currentSet.AddItems(table[i - 1, remainingVolume - 1].Items);
                }

                table[i, j] = previousSet.TotalValue > currentSet.TotalValue ? previousSet : currentSet;
            }
        }


        return table[items.Count - 1, knapsackVolume - 1];
    }

    public void Test01()
    {
        var items = new List<Item>()
        {
            new Item("guitar", 1, 1500),
            new Item("stereo", 4, 3000),
            new Item("laptop", 3, 2000),
            new Item("iphone", 1, 2000),
        };

        var knapsackVolume = 4;

        var set = KnapsackProblem(items, knapsackVolume);
        Console.WriteLine(set);
    }

    public void Test02()
    {
        var items = new List<Item>()
        {
            new Item("вода", 3, 10),
            new Item("книга", 1, 3),
            new Item("еда", 2, 9),
            new Item("куртка", 2, 5),
            new Item("камера", 1, 6),
        };

        var knapsackVolume = 6;

        var set = KnapsackProblem(items, knapsackVolume);
        Console.WriteLine(set);
    }

    public void Test03()
    {
        var items = new List<Item>()
        {
            new Item("Вестминстерское аббатство", 1, 7),
            new Item("театр \"Глобус\"", 1, 6),
            new Item("национальная галерея", 2, 9),
            new Item("Британский музей", 4, 5),
            new Item("собор св. Павла", 1, 8),
        };

        var knapsackVolume = 4;

        var set = KnapsackProblem(items, knapsackVolume);
        Console.WriteLine(set);
    }

    private class Item
    {
        public string Name { get; }
        public int Volume { get; }
        public int Value { get; }

        public Item(string name, int volume, int value)
        {
            Name = name;
            Volume = volume;
            Value = value;
        }

        protected Item()
        {
        }
    }

    private class SetOfItems
    {
        public HashSet<Item> Items { get; }
        public int TotalValue { get; private set; }
        public int TotalVolume { get; private set; }

        public SetOfItems()
        {
            Items = new HashSet<Item>();
        }

        public SetOfItems(Item item)
        {
            Items = new HashSet<Item> { item };
            RecalculateSet();
        }

        private void RecalculateSet()
        {
            TotalValue = Items.Sum(x => x.Value);
            TotalVolume = Items.Sum(x => x.Volume);
        }

        public void AddItems(HashSet<Item> items)
        {
            foreach (var item in items)
            {
                Items.Add(item);
            }
            RecalculateSet();
        }

        public override string ToString() =>
            $"volume={TotalVolume}, value={TotalValue}, items={string.Join(',', Items.Select(x => $"{x.Name}({x.Value})"))}";
    }
}